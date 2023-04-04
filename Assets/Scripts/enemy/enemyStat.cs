using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStat : MonoBehaviour
{

    public float hp;

    public void Hit(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy killed");
        }
    }
}
