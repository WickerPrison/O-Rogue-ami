using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    [SerializeField] Vector3 leftPosition;
    [SerializeField] Vector3 rightPosition;
    [SerializeField] Transform marker;
    [SerializeField] TextMeshProUGUI markerNumber;
    SpiritConditions conditions;
    GameManager gameManager;
    Vector3 markerDestination;
    float maxFavor = 10;
    float minFavor = -50;
    float favor = 0;
    float speed = 5;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        conditions = GetComponent<SpiritConditions>();
    }

    public void SpiritTurn()
    {
        LoseFavor(10);
        StartCoroutine(DummyTurnDelay());
    }

    IEnumerator DummyTurnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if(gameManager.gameState != GameState.GAMEOVER)
        {
            conditions.IncrementConditions();
            gameManager.StartPlayerTurn();
        }
    }

    void UpdateUI()
    {
        float ratio = (favor - minFavor) / (maxFavor - minFavor);
        markerDestination = Vector3.Lerp(leftPosition, rightPosition, ratio);
        markerNumber.text = favor.ToString();
    }

    public void LoseFavor(int amount)
    {
        if (conditions.pacify > 0) return;

        favor -= amount;
        UpdateUI();
        if(favor <= minFavor)
        {
            gameManager.LoseGame();
        }
    }

    public void GainFavor(int amount)
    {
        if (conditions.pacify > 0 || conditions.hardHearted > 0) return;

        favor += amount;
        UpdateUI();
        if(favor >= maxFavor)
        {
            gameManager.WinGame();
        }
    }

    public void GetCondition(Conditions condition, int amount)
    {
        conditions.GetCondition(condition, amount);
    }

    private void Update()
    {
        if (Vector3.Distance(marker.localPosition, markerDestination) > speed * Time.deltaTime)
        {
            Vector3 direction = markerDestination - marker.localPosition;
            marker.localPosition += speed * Time.deltaTime * direction.normalized;
        }
        else
        {
            marker.localPosition = markerDestination;
        }
    }
}
