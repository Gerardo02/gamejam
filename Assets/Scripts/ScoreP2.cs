using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreP2 : MonoBehaviour
{
    // Start is called before the first frame update
    Text txtScore;
    int score = 0;

    void Awake() => txtScore = GetComponent<Text>();

    public void AddPointsP2(int points)
    {
        score += points;
        txtScore.text = $"Souls: {score}";
    }
}
