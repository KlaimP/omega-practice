using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordManagerScript : MonoBehaviour
{

    public TMP_Text Score;
    public TMP_Text HighScore;


    // Update is called once per frame
    void Update()
    {
        Score.text = "Your Score " + PlayerPrefs.GetInt("Score");
        HighScore.text = "High Score " + PlayerPrefs.GetInt("HighScore");

        if (PlayerPrefs.GetInt("HighScore") < PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }

    }
}
