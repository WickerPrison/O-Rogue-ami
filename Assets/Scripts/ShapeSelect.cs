using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSelect : MonoBehaviour
{
    [SerializeField] GameObject menuShapePrefab;
    [SerializeField] Transform grid;
    [System.NonSerialized] public GameManager gameManager;
    PlayerManager playerManager;
    CardScript choosingCard;
    Project choosingProject;
    ShapeObject selectedShape;
    [SerializeField] Image selectedImage;
    [SerializeField] TextMeshProUGUI shapeName;
    [SerializeField] TextMeshProUGUI folds;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] PlayerData playerData;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        foreach (ShapeObject shapeObject in playerData.knownShapes)
        {
            MenuShape menuShape = Instantiate(menuShapePrefab).GetComponent<MenuShape>();
            menuShape.transform.SetParent(grid.transform);
            menuShape.shapeObject = shapeObject;
            menuShape.shapeSelect = this;
        }
        SetSelected(playerData.knownShapes[0]);
        gameObject.SetActive(false);
    }

    public void OpenMenu(CardScript cardScript, Project project)
    {
        choosingCard = cardScript;
        choosingProject = project;
    }

    public void SetSelected(ShapeObject shapeObject)
    {
        selectedShape = shapeObject;
        selectedImage.sprite = shapeObject.sprite;
        shapeName.text = shapeObject.name;
        folds.text = "Folds: " + shapeObject.folds.ToString();
        description.text = shapeObject.description;
    }

    public void StartProject()
    {
        choosingProject.StartProject(choosingCard, selectedShape);
        gameManager.gameState = GameState.PLAYERTURN;
        choosingProject = null;
        choosingCard = null;
        gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        gameManager.gameState = GameState.PLAYERTURN;
        choosingCard.ReturnToHand();
        choosingProject = null;
        choosingCard = null;
        gameObject.SetActive(false);
    }
}
