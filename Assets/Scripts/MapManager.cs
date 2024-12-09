using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public MapData mapData;
    MapPlayer mapPlayer;
    public Dictionary<MapIcon, List<MapIcon>> mapDict = new Dictionary<MapIcon, List<MapIcon>>();
    public MapIcon[] mapIcons;

    private void Awake()
    {
        mapIcons = GetComponentsInChildren<MapIcon>();
        foreach(MapIcon icon in mapIcons)
        {
            mapDict.Add(icon, new List<MapIcon>());
        }
    }

    private void Start()
    {
        mapPlayer = GetComponentInChildren<MapPlayer>();
        if (mapData.mapState == MapData.MapState.FIRSTLANDING) InitialSetup();
        else
        {
            mapData.mapState = MapData.MapState.CHOOSING;
            mapPlayer.transform.position = mapIcons[mapData.currentPosition].transform.position;
            Arrived(mapIcons[mapData.currentPosition]);
        }

        foreach(int index in mapData.visited)
        {
            if(mapIcons[index].IconState == MapIcon.IconStates.UNREACHABLE)
            {
                mapIcons[index].IconState = MapIcon.IconStates.VISITED;
            }
            else if(mapIcons[index].IconState == MapIcon.IconStates.REACHABLE)
            {
                mapIcons[index].IconState = MapIcon.IconStates.VISITEDREACHABLE;
            }
        }
    }

    void InitialSetup()
    {
        foreach (MapIcon icon in mapIcons)
        {
            if (icon.isDock)
            {
                icon.IconState = MapIcon.IconStates.REACHABLE;
            }
            else
            {
                icon.IconState = MapIcon.IconStates.UNREACHABLE;
            }
        }
    }

    public void GoHere(MapIcon destinationIcon)
    {
        foreach(MapIcon icon in mapIcons)
        {
            if(icon.IconState == MapIcon.IconStates.REACHABLE)
            {
                icon.IconState = MapIcon.IconStates.UNREACHABLE;
            }
        }
        mapPlayer.destinationIcon = destinationIcon;
        mapData.mapState = MapData.MapState.MOVING;
        int index = Array.IndexOf(mapIcons, destinationIcon);
        mapData.currentPosition = index;
        mapData.AddVisited(index);
    }

    public void Arrived(MapIcon arrivedIcon)
    {
        foreach(MapIcon icon in mapIcons)
        {
            if(icon.IconState == MapIcon.IconStates.REACHABLE)
            {
                icon.IconState = MapIcon.IconStates.UNREACHABLE;
            }
            else if(icon.IconState == MapIcon.IconStates.VISITEDREACHABLE)
            {
                icon.IconState = MapIcon.IconStates.VISITED;
            }
        }

        foreach(MapIcon reachable in mapDict[arrivedIcon])
        {
            if(reachable.IconState == MapIcon.IconStates.UNREACHABLE)
            {
                reachable.IconState = MapIcon.IconStates.REACHABLE;
            }
            else if(reachable.IconState == MapIcon.IconStates.VISITED)
            {
                reachable.IconState = MapIcon.IconStates.VISITEDREACHABLE;
            }
        }
    }
}
