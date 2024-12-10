using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySlot : MonoBehaviour
{
    [SerializeField] GameObject cardDisplay;
    [SerializeField] SpriteRenderer cardArt;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;
    CardObject cardObject;

    public bool PlayCard(CardObject playedCard)
    {
        if(cardObject == null)
        {
            CreateOragami(playedCard);
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateOragami(CardObject card)
    {
        cardDisplay.SetActive(true);
        cardObject = card;
        cardArt.sprite = card.cardArt;
        cardName.text = card.cardName;
        cardDescription.text = card.description;
        score.text = GetScore().ToString();
    }

    public int GetScore()
    {
        if (cardObject == null) return 0; 
        return cardObject.score;
    }
}
