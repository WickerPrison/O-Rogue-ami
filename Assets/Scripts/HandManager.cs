using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    DeckManager deckManager;
    List<CardScript> hand = new List<CardScript>();
    Vector3 drawPosition = new Vector3(-7.85f, -4f, 0);
    Vector3 centerPosition = new Vector3(0, -3.7f, 0);
    float spacing = 1.5f;

    private void Start()
    {
        deckManager = GetComponent<DeckManager>();
    }

    public void Draw()
    {
        if(deckManager.drawPile.Count == 0)
        {
            if (deckManager.discardPile.Count == 0) return;
            deckManager.ShuffleDiscardIntoDraw();
        }
        CardObject cardObject = deckManager.drawPile.Dequeue();
        CardScript card = Instantiate(cardPrefab).GetComponent<CardScript>();
        card.cardObject = cardObject;
        hand.Add(card);
        card.transform.position = drawPosition;
        card.handManager = this;
    }

    public void UpdateHandPositions()
    {
        for(int i = 0; i < hand.Count; i++)
        {
            float offset = spacing * i - spacing * ((float)hand.Count / 2 - 0.5f);
            hand[i].destination = centerPosition + new Vector3(offset, 0, 0);
        }
    }

    public void Discard(CardScript cardScript)
    {
        hand.Remove(cardScript);
        UpdateHandPositions();
        deckManager.Discard(cardScript.cardObject);   
    }

    public void RemoveCard(CardScript cardScript)
    {
        hand.Remove(cardScript);
        UpdateHandPositions();
    }
}
