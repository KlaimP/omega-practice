using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public float HP;

    public float MaxHP;

    public HealthBar hb;

    GameObject player;
    public GameObject died;
    
    void Start()
    {
        GameObject goHb = GameObject.FindWithTag("HealthBar");
        died = GameObject.FindWithTag("Died");
        hb = goHb.GetComponent<HealthBar>();
        hb.MaxHealth(MaxHP);
        hb.Set(MaxHP);
        HP = MaxHP;
        died.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    public void damage(float dmg)
    {
        HP -= dmg;
        hb.Set(HP);
        if (HP <= 0)
        {
            movement mve = player.GetComponent<movement>();
            mve.enabled = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        died.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Menu");
    }
}
