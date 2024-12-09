using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public enum MapState
    {
        FIRSTLANDING, MOVING, CHOOSING, BOSS
    }
    
    public MapState mapState = MapState.FIRSTLANDING;
    public int currentPosition;
    public List<int> visited;

    public void AddVisited(int index)
    {
        if(visited.Contains(index)) return;
        visited.Add(index);
    }

    public void Reset()
    {
        mapState = MapState.FIRSTLANDING;
        visited.Clear();
    }
}
