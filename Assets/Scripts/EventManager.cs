using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get{return instance;}
    }

    public event EventHandler OnStartMatch;
    public event EventHandler OnDrawCard;
    public event EventHandler OnDiscard;
    public event EventHandler OnShuffleIntoDraw;
    public event EventHandler OnUpdateEnergy;
    public event Action<EventManager, Transform> OnHover;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void StartMatch()
    {
        OnStartMatch?.Invoke(this, EventArgs.Empty);
    }

    public void DrawCard()
    {
        OnDrawCard?.Invoke(this, EventArgs.Empty);
    }

    public void Discard()
    {
        OnDiscard?.Invoke(this, EventArgs.Empty);
    }

    public void ShuffleIntoDraw()
    {
        OnShuffleIntoDraw?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateEnergy()
    {
        OnUpdateEnergy?.Invoke(this, EventArgs.Empty);
    }
}
