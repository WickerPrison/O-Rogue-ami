using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    DeckManager deckManager;
    [SerializeField] TextMeshProUGUI discardCount;

    private void Start()
    {
        deckManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<DeckManager>();
    }

    private void OnDiscard(object sender, System.EventArgs e)
    {
        discardCount.text = deckManager.discardPile.Count.ToString();
    }

    private void OnShuffleIntoDraw(object sender, System.EventArgs e)
    {
        discardCount.text = deckManager.discardPile.Count.ToString();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDiscard += OnDiscard;
        EventManager.Instance.OnShuffleIntoDraw += OnShuffleIntoDraw;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDiscard -= OnDiscard;
        EventManager.Instance.OnShuffleIntoDraw -= OnShuffleIntoDraw;
    }

}
