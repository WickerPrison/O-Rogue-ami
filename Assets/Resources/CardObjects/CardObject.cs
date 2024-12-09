using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu]
public class CardObject : ScriptableObject
{
    public string cardName;
    public int score;
    [TextArea(5, 10)]
    public string description;
    public Sprite cardArt;
}
