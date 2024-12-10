using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoundButton : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        gameManager.EndRound();

        if(gameManager.gameState == GameState.PLAYERTURN)
        {
            
        }
    }
}
