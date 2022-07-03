using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int jumpCount = 0;
    public float dieCoordinate = -7.0f; 

    public float speed = 10.0f;
    public float jumpPower;

    public float rotateSpeed = 10.0f;

    bool jumping;
    
    Vector3 movement;
    float h, v;
    float rh, rv;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rh = Input.GetAxis("Horizontal");
        rv = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(rh, 0, rv);

        if(!(h==0 && v==0))
        {
            transform.position+=dir*speed*Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime*rotateSpeed);
        }
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
        else jumping = false;

        jump();

        Die();
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

    void Die()
    {
        if(gameObject.transform.position.y<=dieCoordinate)
        {
            Debug.Log("플레이어 죽음");
        }
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
