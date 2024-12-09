using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewRun : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] MapData mapData;
    [SerializeField] List<CardObject> startingCards;

    public void StartRun()
    {
        playerData.deck.Clear();
        foreach(CardObject card in startingCards)
        {
            playerData.deck.Add(card);
        }

        mapData.Reset();

        SceneManager.LoadScene("Island1");
    }
}
