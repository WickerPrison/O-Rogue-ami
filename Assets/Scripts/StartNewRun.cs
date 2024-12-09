using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewRun : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] MapData mapData;
    [SerializeField] List<CardObject> startingCards;
    [SerializeField] List<ShapeObject> startingShapes;

    public void StartRun()
    {
        playerData.maxEnergy = 3;
        playerData.deck.Clear();
        foreach(CardObject card in startingCards)
        {
            playerData.deck.Add(card);
        }
        playerData.knownShapes.Clear();
        foreach(ShapeObject shape in startingShapes)
        {
            playerData.knownShapes.Add(shape);
        }
        playerData.maxProjects = 2;
        playerData.mostRecentOragamiFavor = 0;

        mapData.Reset();

        SceneManager.LoadScene("Island1");
    }
}
