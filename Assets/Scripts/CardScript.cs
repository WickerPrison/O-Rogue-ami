using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public enum CardState
{
    DRAWING, HAND, DRAGGING, DISCARDING, CHOOSESHAPE
}

public class CardScript : MonoBehaviour
{
    [System.NonSerialized] public CardObject cardObject;
    [SerializeField] TextMeshProUGUI cardCost;
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;
    [SerializeField] SpriteRenderer artRenderer;
    [System.NonSerialized] public HandManager handManager;
    [System.NonSerialized] public Vector3 destination;
    Vector3 discardDestination = new Vector3(7.85f, -4f, 0);
    Vector3 chooseShapeDestination = new Vector3(-6, 2, 0);
    float speed = 20;
    CardState cardState = CardState.DRAWING;
    PlayerManager playerManager;
    PlayerConditions playerConditions;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerManager = handManager.GetComponent<PlayerManager>();
        playerConditions = playerManager.GetComponent<PlayerConditions>();

        cardCost.text = cardObject.cost.ToString();
        cardName.text = cardObject.name;
        cardDescription.text = cardObject.description;
        artRenderer.sprite = cardObject.cardArt;
    }

    // Update is called once per frame
    void Update()
    {
        if (cardState == CardState.DRAGGING)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position -= new Vector3(0, 0, transform.position.z);
        }
        else
        {
            ToDestination();
        }
    }

    void ToDestination()
    {
        if (Vector2.Distance(transform.position, destination) > speed * Time.deltaTime)
        {
            Vector3 direction = destination - transform.position;
            transform.position += speed * Time.deltaTime * direction.normalized;
        }
        else
        {
            transform.position = destination;
            switch (cardState)
            {
                case CardState.DISCARDING:
                    Destroy(gameObject); break;
                case CardState.DRAWING:
                    cardState = CardState.HAND;
                    break;
            }
        }
    }

    private void OnMouseUp()
    {
        if (cardState == CardState.DRAGGING)
        {
            if (playerManager.hoverTransform == null)
            {
                cardState = CardState.HAND;
                return;
            }

            switch (playerManager.hoverTransform.tag)
            {
                case "PlayZone":
                    PlayCard();
                    break;
                case "Project":
                    Project project = playerManager.hoverTransform.GetComponent<Project>();
                    if(project.ProjectState == ProjectState.NEW)
                    {
                        cardState = CardState.CHOOSESHAPE;
                        destination = chooseShapeDestination;
                        project.NewProject(this);
                    }
                    else if(project.ProjectState == ProjectState.INPROGRESS)
                    {
                        
                    }
                    break;
                default:
                    cardState = CardState.HAND;
                    break;
            }
        }
    }

    void PlayCard()
    {

    }


    void Discard()
    {
        if (cardState == CardState.DISCARDING) return;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        cardState = CardState.DISCARDING;
        handManager.Discard(this);
        destination = discardDestination;
    }

    public void ReturnToHand()
    {
        cardState = CardState.HAND;
        handManager.UpdateHandPositions();
    }

    public void BecomeOragami()
    {
        handManager.RemoveCard(this);
        Destroy(gameObject);
    }

    private void OnEndPlayerTurn(object sender, System.EventArgs e)
    {
        Discard();
    }

    private void OnMouseDown()
    {
        if (gameManager.gameState != GameState.PLAYERTURN) return;

        if(cardState == CardState.HAND)
        {
            cardState = CardState.DRAGGING;
        }
    }
}
