﻿using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
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
        
        [JsonPropertyName("scorecardState")] 
        public ScorecardState ScorecardState { get; private set; }

        [JsonPropertyName("lastAccessed")] 
        public DateTime LastAccessed { get; private set; } = DateTime.Now;

        public void NewScorecard(ComponentBase source, CourseResponse courseResponseModel)
        {
            ScorecardState = ScorecardState.Create(courseResponseModel);
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