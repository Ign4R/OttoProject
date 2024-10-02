using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Image cafeBar;
    private float currentScore;  // Puntaje actual
    public float maxScore;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        cafeBar.fillAmount = currentScore / maxScore;
    }

}
