using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Vector2 mouseVector;

    Rigidbody2D rb;

    public Camera cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        mouseVector = cm.ScreenToWorldPoint(Input.mousePosition);
    }
}
