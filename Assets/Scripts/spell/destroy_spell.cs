using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_spell : MonoBehaviour
{

    public float time_destroy = 5f;

    public Animator anim;

    bool stay = true;
    GameObject fp;
    // Start is called before the first frame update
    void Start()
    {
        fp = GameObject.Find("fire_point");
        Destroy(gameObject, time_destroy);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(stay_time());
        if (stay)
            gameObject.transform.position = fp.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            anim.SetBool("destroy", true);
            Destroy(gameObject, 0.4f);
        }
    }

    IEnumerator stay_time()
    {
        yield return new WaitForSeconds(0.2f);
        stay = false;
    }
}

