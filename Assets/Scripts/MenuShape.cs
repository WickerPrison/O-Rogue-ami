using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuShape : MonoBehaviour
{
    [System.NonSerialized] public ShapeObject shapeObject;
    [System.NonSerialized] public ShapeSelect shapeSelect;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = shapeObject.sprite;
    }

    public void SetSelected()
    {
        shapeSelect.SetSelected(shapeObject);
    }
}
