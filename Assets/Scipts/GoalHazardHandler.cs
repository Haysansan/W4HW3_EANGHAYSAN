using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GoalHazardHandler : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    float elapsed = 0f;
    bool finished = false;

    void Update()
    {
        if (finished) return;
        elapsed += Time.deltaTime;
        timerText.text = $"Time: {elapsed:F2}s";
    }

    public void OnGoalReached()
    {
        finished = true;
        messageText.text = "Goal reached!";
    }

    public void OnHazardHit()
    {
        finished = true;
        messageText.text = "Hit hazard!";
    }
}

