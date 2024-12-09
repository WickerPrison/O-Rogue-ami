using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject youLose;
    [SerializeField] GameObject youWin;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void LoseGame()
    {
        youLose.SetActive(true);
    }

    public void WinGame()
    {
        youWin.SetActive(true);
    }
}
