using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Image cafeBar;
    private float currentScore;  // Puntaje actual
    public float maxScore;
    public float maxFoods=37;
    public GameObject[] listFood;
    private int countFood;
    private int _lifes = 7;

    [SerializeField]
    private TextMeshProUGUI txtMesh;

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
        countFood++;
        currentScore += score;
        cafeBar.fillAmount = currentScore / maxScore;
    }
    public void ReactivateFoods()
    {
        if (countFood >= listFood.Length)
        {
            foreach (var item in listFood)
            {
                item.SetActive(true);
            }
        }
    }

    public void ModifyLife(int value)
    {
        _lifes += value;
        txtMesh.text = _lifes.ToString();
        if (_lifes >= 7)
        {
            _lifes = 7;
            txtMesh.text = _lifes.ToString();
        }

    }


}
