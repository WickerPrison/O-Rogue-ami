using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public List<CardObject> cardPool;

    public CardObject[] GetCards(int amount)
    {
        List<CardObject> drawPool = new List<CardObject>(cardPool);
        CardObject[] results = new CardObject[amount];
        for(int i = 0; i < amount; i++)
        {
            int randInt = Random.Range(0, drawPool.Count - 1);
            results[i] = drawPool[randInt];
            drawPool.RemoveAt(randInt);
        }
        return results;
    }
}
