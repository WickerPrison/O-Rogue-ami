using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectZone : MonoBehaviour
{
    [SerializeField] List<Project> projects;
    [SerializeField] PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < projects.Count; i++)
        {
            if(i < playerData.maxProjects)
            {
                projects[i].ProjectState = ProjectState.NEW;
            }
            else
            {
                projects[i].ProjectState = ProjectState.LOCKED;
            }
        }
    }
}
