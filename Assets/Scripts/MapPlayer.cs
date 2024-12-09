using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayer : MonoBehaviour
{
    MapManager mapManager;
    public MapIcon destinationIcon;
    float speed = 5;

    private void Start()
    {
        mapManager = GetComponentInParent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationIcon == null) return;

        if(Vector3.Distance(transform.position, destinationIcon.transform.position) > speed * Time.deltaTime)
        {
            Vector3 direction = destinationIcon.transform.position - transform.position;
            transform.position += speed * Time.deltaTime * direction.normalized;
            if(Vector3.Distance(transform.position, destinationIcon.transform.position) < speed * Time.deltaTime)
            {
                transform.position = destinationIcon.transform.position;
                if (destinationIcon.IconState == MapIcon.IconStates.ENTERING)
                {
                    destinationIcon.EnterEncounter();
                }
                else
                {
                    mapManager.Arrived(destinationIcon);
                    destinationIcon = null;
                }
            }
        }
    }
}
