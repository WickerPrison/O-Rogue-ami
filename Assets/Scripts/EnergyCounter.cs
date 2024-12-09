using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyCounter;
    [SerializeField] PlayerData playerData;
    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    private void OnUpdateEnergy(object sender, System.EventArgs e)
    {
        energyCounter.text = playerManager.Energy.ToString() + "/" + playerData.maxEnergy.ToString();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnUpdateEnergy += OnUpdateEnergy;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnUpdateEnergy -= OnUpdateEnergy;
    }

}
