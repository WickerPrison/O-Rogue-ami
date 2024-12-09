using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OragamiDisplay : MonoBehaviour
{
    ShapeObject shapeObject;
    CardObject cardObject;
    [System.NonSerialized] public Vector3 destination;
    [SerializeField] SpriteRenderer spriteRenderer;
    float speed = 20;

    Spirit spirit;

    private void Start()
    {
        spirit = GameObject.FindGameObjectWithTag("Spirit").GetComponent<Spirit>();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.localPosition, destination) > speed * Time.deltaTime)
        {
            Vector3 direction = destination - transform.localPosition;
            transform.localPosition += speed * Time.deltaTime * direction.normalized;
        }
        else
        {
            transform.localPosition = destination;
        }
    }

    public void Setup(ShapeObject newShapeObject, CardObject newCardObject)
    {
        shapeObject = newShapeObject;
        cardObject = newCardObject;
        spriteRenderer.sprite = shapeObject.sprite;
    }

    private void OnPlayerTurn(object sender, System.EventArgs e)
    {
        if(shapeObject.passiveFavor > 0)
        {
            spirit.GainFavor(shapeObject.passiveFavor);
        }

       if(cardObject.foldBonusType == FoldBonusType.PERSISTANT)
        {
            spirit.GainFavor((int)cardObject.foldBonusAmount);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnPlayerTurn += OnPlayerTurn;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnPlayerTurn -= OnPlayerTurn;
    }
}
