using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FinishAbilities
{
    
}

public class OragamiAbilities : MonoBehaviour
{
    public Dictionary<FinishAbilities, Action> finishDict = new Dictionary<FinishAbilities, Action>();

    private void Start()
    {
        
    }


}
