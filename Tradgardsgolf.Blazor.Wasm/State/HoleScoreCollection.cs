using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tradgardsgolf.BlazorWasm.State;

public class HoleScoreCollection : IList<HoleScoreModel>
{
    private readonly IList<HoleScoreModel> _holeScores = new List<HoleScoreModel>();

    public HoleScoreModel this[int index]
    {
        get => _holeScores.FirstOrDefault(x => x.Hole == index);
        set => this[index] = value;
    }

    public int Count => _holeScores.Count;
    public bool IsReadOnly => _holeScores.IsReadOnly;

    public void Add(HoleScoreModel item)
    {
        _holeScores.Add(item);
    }

    public void Clear()
    {
        _holeScores.Clear();
    }

    public bool Contains(HoleScoreModel item)
    {
        return _holeScores.Contains(item);
    }

    public void CopyTo(HoleScoreModel[] array, int arrayIndex)
    {
        _holeScores.CopyTo(array, arrayIndex);
    }

    public IEnumerator<HoleScoreModel> GetEnumerator()
    {
        return _holeScores.GetEnumerator();
    }

    public int IndexOf(HoleScoreModel item)
    {
        return _holeScores.IndexOf(item);
    }

    public void Insert(int index, HoleScoreModel item)
    {
        _holeScores.Insert(index, item);
    }

    public bool Remove(HoleScoreModel item)
    {
        return _holeScores.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _holeScores.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}