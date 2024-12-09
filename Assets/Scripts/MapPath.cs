using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPath : MonoBehaviour
{
    [SerializeField] Image dot;
    MapManager mapManager;
    MapIcon closest;
    MapIcon secondClosest;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        dot.enabled = false;
        mapManager = GetComponentInParent<MapManager>();
        lineRenderer = GetComponent<LineRenderer>();
        findClosest2();
        Vector3 position1 = new Vector3(closest.transform.position.x, closest.transform.position.y, 89);
        Vector3 position2 = new Vector3(secondClosest.transform.position.x, secondClosest.transform.position.y, 89);
        Vector3[] array = { position1, position2 };
        lineRenderer.SetPositions(array);
        mapManager.mapDict[closest].Add(secondClosest);
        mapManager.mapDict[secondClosest].Add(closest);
    }

    void findClosest2()
    {
        float closestDistance = 1000;
        float secondClosestDistance = 1001;
        closest = null;
        secondClosest = null;
        foreach(MapIcon icon in mapManager.mapIcons)
        {
            float distance = Vector3.Distance(transform.position, icon.transform.position);
            if (distance < closestDistance)
            {
                if(closest != null)
                {
                    secondClosest = closest;
                    secondClosestDistance = closestDistance;
                }
                closest = icon;
                closestDistance = distance;
            }
            else if(distance < secondClosestDistance)
            {
                secondClosest = icon;
                secondClosestDistance = distance;
            }
        }
    }
}
