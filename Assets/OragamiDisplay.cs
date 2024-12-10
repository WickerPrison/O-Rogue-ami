using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OragamiDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] DisplaySlot[] displaySlots;
    int score;

    public void CountScore()
    {
        score = 0;
        for(int i = 0; i < displaySlots.Length; i++)
        {
            score += displaySlots[i].GetScore();
        }
        scoreDisplay.text = score.ToString();
    }
}
