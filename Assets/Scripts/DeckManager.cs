using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public PlayerData playerData;
    public Queue<CardObject> drawPile;
    public List<CardObject> discardPile;
    HandManager handManager;

    private void Awake()
    {
        handManager = GetComponent<HandManager>();

        playerData.deck.Shuffle();
        drawPile = new Queue<CardObject>(playerData.deck);
    }

    private void OnStartMatch(object sender, System.EventArgs e)
    {
        DrawHand(6);
    }

    void DrawHand(int handSize)
    {
        for(int i = 0; i < handSize; i++)
        {
            handManager.Draw();
        }
        handManager.UpdateHandPositions();
        EventManager.Instance.DrawCard();
    }

    public void Discard(CardObject card)
    {
        discardPile.Add(card);
        EventManager.Instance.Discard();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStartMatch += OnStartMatch;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartMatch -= OnStartMatch;
    }

}
