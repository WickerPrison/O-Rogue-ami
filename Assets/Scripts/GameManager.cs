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

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        StartMatch();
    }

    public void StartMatch()
    {
        gameState = GameState.PLAYERTURN;
        EventManager.Instance.StartMatch();
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
