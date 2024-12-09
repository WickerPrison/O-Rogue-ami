using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    [SerializeField] CardReward[] cardRewardDisplays;
    [SerializeField] GameData gameData;

    private void Start()
    {
        CardObject[] cardOptions = gameData.GetCards(3);
        for(int i = 0; i < cardOptions.Length; i++)
        {
            cardRewardDisplays[i].Setup(cardOptions[i]);
        }
    }
}
