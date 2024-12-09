using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tags
{
    LEAPFROG
}

[CreateAssetMenu]
public class ShapeObject : ScriptableObject
{
    public string shapeName;
    public int folds;
    public Sprite sprite;
    public string description;

    public int favorOnFinish;
    public int passiveFavor;

    public List<Tags> tags;

    public List<FinishAbilities> finishAbilities;
    public List<int> finishAbilitiesAmount;
}
