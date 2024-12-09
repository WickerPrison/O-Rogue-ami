using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapIcon : MonoBehaviour
{
    public bool isDock = false;
    [SerializeField] Image clickableIcon;
    [SerializeField] Image visitedIcon;
    [SerializeField] string type;
    MapManager mapManager;
    IconStates iconState = IconStates.UNREACHABLE;
    public IconStates IconState {
        get { return iconState; }
        set
        {
            iconState = value;
            switch(iconState)
            {
                case IconStates.UNREACHABLE:
                    visitedIcon.enabled = false;
                    clickableIcon.enabled = false;
                    break;
                case IconStates.REACHABLE:
                    visitedIcon.enabled = false;
                    clickableIcon.enabled = true;
                    break;
                case IconStates.ENTERING:
                case IconStates.VISITEDREACHABLE:
                case IconStates.VISITED:
                    visitedIcon.enabled = true;
                    clickableIcon.enabled = false;
                    break;

            }
        } 
    }

    public enum IconStates
    {
        UNREACHABLE, REACHABLE, VISITED, VISITEDREACHABLE, ENTERING
    }

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GetComponentInParent<MapManager>();
    }

    public virtual void EnterEncounter()
    {
        SceneManager.LoadScene("Combat");
    }

    private void OnMouseDown()
    {
        if(IconState == IconStates.REACHABLE)
        {
            IconState = IconStates.ENTERING;
            mapManager.GoHere(this);
        }
        else if(IconState == IconStates.VISITEDREACHABLE)
        {
            IconState = IconStates.VISITED;
            mapManager.GoHere(this);
        }
    }
}
