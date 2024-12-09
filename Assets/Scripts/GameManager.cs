using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    GAMESTART, PLAYERTURN, ENEMYTURN, MENU, GAMEOVER
}

public class GameManager : MonoBehaviour
{
    UIManager uiManager;
    [System.NonSerialized] public GameState gameState = GameState.GAMESTART;
    Spirit spirit;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        spirit = GameObject.FindGameObjectWithTag("Spirit").GetComponent<Spirit>();
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        gameState = GameState.PLAYERTURN;
        EventManager.Instance.PlayerTurn();
    }

    public void EndPlayerTurn()
    {
        gameState = GameState.ENEMYTURN;
        EventManager.Instance.EndPlayerTurn();
        spirit.SpiritTurn();
    }

    public void LoseGame()
    {
        gameState = GameState.GAMEOVER;
        SceneManager.LoadScene("Island1");
    }

    public void WinGame()
    {
        gameState = GameState.GAMEOVER;
        SceneManager.LoadScene("CardReward");
    }
}
