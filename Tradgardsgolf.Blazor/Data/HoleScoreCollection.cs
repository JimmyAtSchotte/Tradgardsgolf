using System.Collections;
using System.Collections.Generic;

namespace Tradgardsgolf.Blazor.Data
{
    public class HoleScoreCollection : IList<HoleScore>
    {
        private readonly IList<HoleScore> _holeScores;

        public HoleScoreCollection()
        {
            _holeScores = new List<HoleScore>();
        }

        public HoleScore this[int index] { 
            get =>  _holeScores.FirstOrDefault(x => x.Hole == index);            
            set =>  this[index] = value;            
        }

        public int Count => _holeScores.Count;
        public bool IsReadOnly => _holeScores.IsReadOnly;
        public void Add(HoleScore item) => _holeScores.Add(item);
        public void Clear() => _holeScores.Clear();
        public bool Contains(HoleScore item) => _holeScores.Contains(item);
        public void CopyTo(HoleScore[] array, int arrayIndex) => _holeScores.CopyTo(array, arrayIndex);
        public IEnumerator<HoleScore> GetEnumerator() => _holeScores.GetEnumerator();

        public int IndexOf(HoleScore item) => _holeScores.IndexOf(item);
        public void Insert(int index, HoleScore item) => _holeScores.Insert(index, item);

        public bool Remove(HoleScore item) => _holeScores.Remove(item);
        public void RemoveAt(int index) => _holeScores.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
