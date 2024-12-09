using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayZone : MonoBehaviour
{
    public List<OragamiDisplay> displayObjects;
    [SerializeField] GameObject displayElementPrefab;
    List<Transform> transforms;
    Vector3 down = new Vector3(0, -1, 0);
    Vector3 right = new Vector3(1, 0, 0);

    private void Start()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        for(int i = 0; i < displayObjects.Count; i++)
        {
            Vector3 position = new Vector3(-4f, 1.5f, 0);
            int j = i + 1;
            while(j > 9)
            {
                j -= 9;
                position += down; 
            }
            position += right * (j - 1);
            displayObjects[i].destination = position;
        }
    }

    public void AddDisplayObject(ShapeObject shapeObject, CardObject cardObject, Vector3 startPosition)
    {
        OragamiDisplay oragamiDisplay = Instantiate(displayElementPrefab).GetComponent<OragamiDisplay>();
        oragamiDisplay.Setup(shapeObject, cardObject);
        oragamiDisplay.transform.position = startPosition;
        oragamiDisplay.transform.parent = transform;
        displayObjects.Add(oragamiDisplay);
        UpdateDisplay();
    }
}
