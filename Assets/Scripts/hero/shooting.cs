using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    Vector3 mouseVector;
    [Space]
    [SerializeField]
    bool enableCast = true;

    [Space]
    [Header("Input fireball data:")]
    public GameObject firePoint;
    public GameObject fireballPrefab;
    public float fireballForce;

    [Space]
    [Header("Camera:")]
    [SerializeField]
    Camera cm;

    [Space]
    [Header("Animator")]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseVector = cm.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1") && enableCast)
        {
            StartCoroutine(Shoot());
        }
    }



    void FixedUpdate()
    {
        Vector2 lookDir = mouseVector - firePoint.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.rotation = Quaternion.Euler(0,0,angle);
    }

    IEnumerator Shoot()
    {
        enableCast = false;
        anim.SetBool("cast", true);
        GameObject fireball = Instantiate(fireballPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        Vector3 fp = firePoint.transform.up;
        rb.AddForce(fp * fireballForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("cast", false);
        enableCast = true;
    }

}
