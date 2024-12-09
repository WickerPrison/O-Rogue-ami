using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    DeckManager deckManager;
    [SerializeField] TextMeshProUGUI drawCount;

    private void Start()
    {
        deckManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<DeckManager>();
    }

    private void OnDrawCard(object sender, System.EventArgs e)
    {
        drawCount.text = deckManager.drawPile.Count.ToString();
    }

    private void OnShuffleIntoDraw(object sender, System.EventArgs e)
    {
        drawCount.text = deckManager.drawPile.Count.ToString();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDrawCard += OnDrawCard;
        EventManager.Instance.OnShuffleIntoDraw += OnShuffleIntoDraw;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDrawCard -= OnDrawCard;
        EventManager.Instance.OnShuffleIntoDraw -= OnShuffleIntoDraw;
    }
}
