using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [System.NonSerialized] public Transform hoverTransform;
    LayerMask layerMask;
    private int energy = 0;
    public int Energy
    {
        get { return energy; }
        set
        {
            energy = value;
            EventManager.Instance.UpdateEnergy();
        }
    }

    private void Start()
    {
        layerMask = LayerMask.GetMask("Interactable");
    }

    private void OnPlayerTurn(object sender, System.EventArgs e)
    {
        Energy = playerData.maxEnergy;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 15, layerMask);
        Debug.DrawRay(mousePos, new Vector3(0, 0, -1), Color.red, 1);

        if(hit.transform != null)
        {
            hoverTransform = hit.transform;
        }
        else
        {
            hoverTransform = null;
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
