using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float maxSpeed;
    public Vector2 moveVector;

    Rigidbody2D rb;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector2.zero;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        Animate();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * maxSpeed * Time.fixedDeltaTime);
    }

    void Animate()
    {
        if (moveVector != Vector2.zero)
        {
            anim.SetFloat("Horizontal", moveVector.x);
            anim.SetFloat("Vertical", moveVector.y);
        }
    }
}
