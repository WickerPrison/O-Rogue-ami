using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardReward : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [System.NonSerialized] public CardObject cardObject;
    [SerializeField] TextMeshProUGUI cardCost;
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;
    [SerializeField] SpriteRenderer artRenderer;

    public void Setup(CardObject card)
    {
        cardObject = card;
        cardName.text = cardObject.name;
        cardDescription.text = cardObject.description;
        artRenderer.sprite = cardObject.cardArt;
    }

    public void GainCard()
    {
        playerData.deck.Add(cardObject);
        SceneManager.LoadScene("Island1");
    }
}
