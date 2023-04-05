using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class portal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
