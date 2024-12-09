using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int maxEnergy;
    public List<CardObject> deck;
    public List<ShapeObject> knownShapes;
    public int maxProjects;

    public int mostRecentOragamiFavor;
}
