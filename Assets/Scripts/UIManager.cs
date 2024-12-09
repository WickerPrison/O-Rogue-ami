using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ShapeSelect shapeSelect;
    [SerializeField] GameObject youLose;
    [SerializeField] GameObject youWin;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void OpenShapeSelectMenu(CardScript cardScript, Project project)
    {
        shapeSelect.gameObject.SetActive(true);
        shapeSelect.gameManager = gameManager;
        shapeSelect.OpenMenu(cardScript, project);
        gameManager.gameState = GameState.MENU;
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
