using System;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public abstract class BaseState
    {
        public event Action<ComponentBase, string> StateChanged;
        
        protected virtual void NotifyStateChange(ComponentBase source, string property)
        {
            StateChanged?.Invoke(source, property);
        }
    }

    public class AppState : BaseState
    {
        private readonly TimeSpan _stateValidTime = TimeSpan.FromHours(1);
        
        [JsonProperty] 
        public ScorecardState ScorecardState { get; private set; }

        [JsonProperty] 
        public DateTime LastAccessed { get; private set; }
        
        public AppState()
        {
            LastAccessed = DateTime.Now;
        }

        public void NewScorecard(ComponentBase source, Course courseModel)
        {
            ScorecardState = ScorecardState.Create(courseModel);
            ScorecardState.StateChanged += ScorecardStateOnStateChanged;
            NotifyStateChange(source, nameof(ScorecardState));
        }

        private void ScorecardStateOnStateChanged(ComponentBase source, string property)
        {
            NotifyStateChange(source, $"{nameof(ScorecardState)}.{property}");
        }

        protected override void NotifyStateChange(ComponentBase source, string property)
        {
            LastAccessed = DateTime.Now;
            base.NotifyStateChange(source, property);
        }

        public bool IsValid()
        {
            return DateTime.Now <= LastAccessed.Add(_stateValidTime);
        }
    }
}