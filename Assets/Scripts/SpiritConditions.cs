using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritConditions : MonoBehaviour
{
    [System.NonSerialized] public int pacify;
    [System.NonSerialized] public int hardHearted;

    public void GetCondition(Conditions condition, int amount)
    {
        switch (condition)
        {
            case Conditions.PACIFY:
                pacify += amount;
                break;
            case Conditions.HARDHEARTED:
                hardHearted += amount;
                break;
        }
    }

    public void IncrementConditions()
    {
        if (pacify > 0) pacify--;
        if(hardHearted > 0) hardHearted--;
    }
}
