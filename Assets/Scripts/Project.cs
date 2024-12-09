using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum ProjectState
{
    LOCKED, NEW, INPROGRESS, FINISHED
}

public class Project : MonoBehaviour
{
    Collider2D col;
    [SerializeField] GameObject newIcon;
    [SerializeField] GameObject background;
    [SerializeField] GameObject currentProject;
    [SerializeField] SpriteRenderer projectImage;
    [SerializeField] TextMeshProUGUI foldsDisplay;
    [SerializeField] TextMeshProUGUI shapeName;
    [SerializeField] PlayerData playerData;
    [System.NonSerialized] public ProjectZone projectZone;
    OragamiAbilities oragamiAbilities;
    Spirit spirit;
    UIManager uiManager;
    int folds;
    ShapeObject shapeObject;
    CardObject cardObject;

    ProjectState projectState;
    public ProjectState ProjectState
    {
        get { return projectState;}
        set
        {
            projectState = value;
            switch(projectState)
            {
                case ProjectState.LOCKED:
                    col.enabled = false;
                    newIcon.SetActive(false);
                    currentProject.SetActive(false);
                    background.SetActive(false);
                    break;
                case ProjectState.NEW:
                    folds = 0;
                    col.enabled = true;
                    newIcon.SetActive(true);
                    currentProject.SetActive(false);
                    background.SetActive(true);
                    break;
                case ProjectState.INPROGRESS:
                    col.enabled = true;
                    newIcon.SetActive(false);
                    currentProject.SetActive(true);
                    background.SetActive(true);
                    break;
            }
        }
    }

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    private void Start()
    {
        projectZone = GetComponentInParent<ProjectZone>();
        oragamiAbilities = projectZone.GetComponent<OragamiAbilities>();
        spirit = GameObject.FindGameObjectWithTag("Spirit").GetComponent<Spirit>();
    }

    public void NewProject(CardScript cardScript)
    {
        uiManager.OpenShapeSelectMenu(cardScript, this);
    }

    public void StartProject(CardScript cardScript, ShapeObject chosenShape)
    {
        shapeObject = chosenShape;
        ProjectState = ProjectState.INPROGRESS;
        UpdateDisplay();

        cardObject = cardScript.cardObject;
        cardScript.BecomeOragami();
    }

    public void AddFolds(int foldAmount)
    {
        folds += foldAmount;
        if(folds >= shapeObject.folds)
        {
            FinishProject();
        }
        UpdateDisplay();
    }

    void FinishProject()
    {
        ProjectState = ProjectState.NEW;
        int favorGain = 0;
        favorGain += shapeObject.favorOnFinish;

        if (shapeObject.tags.Contains(Tags.LEAPFROG))
        {
            favorGain = playerData.mostRecentOragamiFavor + shapeObject.favorOnFinish;
        }


        if (cardObject.foldBonusAmount > 0)
        {
            switch (cardObject.foldBonusType)
            {
                case FoldBonusType.ADDITION:
                    favorGain += (int)cardObject.foldBonusAmount;
                    break;
            }
        }

        if(shapeObject.finishAbilities.Count > 0)
        {
            foreach(FinishAbilities ability in shapeObject.finishAbilities)
            {
                oragamiAbilities.finishDict[ability]();
            }
        }

        spirit.GainFavor(favorGain);
        playerData.mostRecentOragamiFavor = favorGain;
        shapeObject = null;
    }

    void UpdateDisplay()
    {
        if (shapeObject == null) return; 
        projectImage.sprite = shapeObject.sprite;
        foldsDisplay.text = "Folds: " + folds.ToString() + " / " + shapeObject.folds.ToString();
        shapeName.text = shapeObject.name;
    }
}
