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
    Spirit spirit;

    // Start is called before the first frame update
    void Start()
    {
        spirit = GameObject.FindGameObjectWithTag("Spirit").GetComponent<Spirit>();
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
                        PlayOnProject(project);
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
        if (!SpendEnergy()) return;

        if (cardObject.addFavor > 0)
        {
            spirit.GainFavor(cardObject.addFavor);
            if (cardObject.specialAbilities.Contains(SpecialAbilities.MULTIFAVOR))
            {
                int index = cardObject.specialAbilities.IndexOf(SpecialAbilities.MULTIFAVOR);
                int counter = (int)cardObject.specialAbilitiesAmounts[index];
                counter--;
                while (counter > 0)
                {
                    spirit.GainFavor(cardObject.addFavor);
                    counter--;
                }
            }
        }

        if (cardObject.conditions.Count > 0)
        {
            for(int i = 0; i <  cardObject.conditions.Count; i++)
            {
                spirit.GetCondition(cardObject.conditions[i], cardObject.conditionAmounts[i]);
            }
        }

        if(cardObject.buffs.Count > 0)
        {
            for(int i = 0; i < cardObject.buffs.Count; i++)
            {
                playerConditions.GetBuff(cardObject.buffs[i], cardObject.buffAmounts[i]);
            }
        }

        if (cardObject.tags.Contains(CardTags.CRUMPLE))
        {
            Crumple();
        }
        else
        {
            Discard();
        }
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

    public void Crumple()
    {
        handManager.RemoveCard(this);
        Destroy(gameObject);
    }

    void PlayOnProject(Project project)
    {
        if (!SpendEnergy()) return;

        if(cardObject.addFolds > 0)
        {
            project.AddFolds(cardObject.addFolds + playerConditions.focus);
        }
        Discard();
    }

    bool SpendEnergy()
    {
        if (playerManager.Energy < cardObject.cost)
        {
            cardState = CardState.HAND;
            return false;
        }
        playerManager.Energy -= cardObject.cost;
        return true;
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

    private void OnEnable()
    {
        EventManager.Instance.OnEndPlayerTurn += OnEndPlayerTurn;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnEndPlayerTurn -= OnEndPlayerTurn;       
    }
}
