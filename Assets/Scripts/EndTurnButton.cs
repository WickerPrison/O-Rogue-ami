using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if(gameManager.gameState == GameState.PLAYERTURN)
        {
            gameManager.EndPlayerTurn();
        }
    }
}
