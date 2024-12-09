using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    PACIFY, HARDHEARTED
}

public enum Buffs
{
    FOCUS
}

public enum FoldBonusType
{
    ADDITION, PERSISTANT
}

public enum CardTags
{
    CRUMPLE
}

public enum SpecialAbilities
{
    MULTIFAVOR
}

[CreateAssetMenu]
public class CardObject : ScriptableObject
{
    public List<CardTags> tags;
    public int cost;
    public string cardName;
    public string description;
    public Sprite cardArt;
    public int addFolds;
    public int addFavor;
    public List<Conditions> conditions;
    public List<int> conditionAmounts;

    public List<Buffs> buffs;
    public List<int> buffAmounts;

    public FoldBonusType foldBonusType;
    public float foldBonusAmount;

    public List<SpecialAbilities> specialAbilities;
    public List<float> specialAbilitiesAmounts;
}
