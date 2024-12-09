using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    public int focus;

    public void GetBuff(Buffs buff, int amount)
    {
        switch(buff)
        {
            case Buffs.FOCUS:
                focus += amount;
                break;
        }
    }
}
