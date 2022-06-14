using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4;
    public float jumpPower;
    bool jumping;
    public int jumpCount = 0;

    Vector3 movement;
    float h, v;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        if(jumpCount < 2)
        {
            run();
        }
        else
            jumping = false;
        jump();
    }
    
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount++;
            
            if (jumping)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    void run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 9;
        else
            speed = 4;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            jumping = true;
            jumpCount = 0;
        }
    }
}
