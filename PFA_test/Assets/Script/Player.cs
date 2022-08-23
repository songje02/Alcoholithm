using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int jumpCount = 0;
    public float dieCoordinate = -7.0f; 

    public float speed = 10.0f;
    public float jumpPower = 5.0f;

    public float rotateSpeed = 10.0f;

    bool jumping;
    
    Vector3 movement;
    float h, v;
    float rh, rv;
    Rigidbody rb;

    public GameManager GM;

    Animator anim;

    Vector3 Save_Pos;

    bool isBorder;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChildRoom"))
        {
            SceneManager.LoadScene("PlayGround");
        }
        else if (other.CompareTag("School"))  // 놀이터 -> 학교
        {
            SceneManager.LoadScene("School");
        }
        else if (other.CompareTag("PlayGround")) // 학교 -> 놀이터
        {
            SceneManager.LoadScene("PlayGround");
        }
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
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

        StopToWall();
    }

    private void Update()
    {
        Move();

        if(jumpCount < 2)
        {
           jump();
        }
        else jumping = false;


        run();
       
        Die();

    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (!isBorder)
        {
            movement = new Vector3(h, 0, v).normalized;
            transform.position += movement * speed * Time.deltaTime;
        }

        anim.SetBool("is_Walking", movement != Vector3.zero);
    }


    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount++;


            if (jumping)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetBool("is_Jumping", true);
            }
            else
            {
                anim.SetBool("is_Jumping", false);
            }

        }
    }

    public void run()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("is_Running", true);
            speed = 9;
        }
        else
        {
            anim.SetBool("is_Running", false);
            speed = 4;
        }
    }

    void Die()
    {
        if(gameObject.transform.position.y<=dieCoordinate)
        {
            Debug.Log("플레이어 죽음");
            this.gameObject.transform.position = Save_Pos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Jump_Reset();
        }

        Check_Savepoint(collision);
    }

    void Jump_Reset()
    {
        anim.SetBool("is_Jumping", false);
        jumping = true;
        jumpCount = 0;
    }


    void Check_Savepoint(Collision collision)
    {
        if (GM.is_Children_Room)
        {
            Save_LightON(collision);
        }
        else if (GM.is_School)
        {
            Save_LightON(collision);
        }
        else if (GM.is_PlayGround)
        {
            Save_LightON(collision);
        }
    }

    void Save_LightON(Collision collision)
    {
        if (collision.gameObject.CompareTag("Save_point_1"))
        {
            Debug.Log("test");
            GameObject obj1 = GameObject.Find("Save_1");
            obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Save_Pos = obj1.transform.position;
            Jump_Reset();
        }
        else if (collision.gameObject.CompareTag("Save_point_2"))
        {
            GameObject obj1 = GameObject.Find("Save_2");
            obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Save_Pos = obj1.transform.position;
            Jump_Reset();
        }
        else if (collision.gameObject.CompareTag("Save_point_3"))
        {
            GameObject obj1 = GameObject.Find("Save_3");
            obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Save_Pos = obj1.transform.position;
            Jump_Reset();
        }
        else if (collision.gameObject.CompareTag("Save_point_4"))
        {
            GameObject obj1 = GameObject.Find("Save_4");
            obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Save_Pos = obj1.transform.position;
            Jump_Reset();
        }
        else if (collision.gameObject.CompareTag("Save_point_5"))
        {
            GameObject obj1 = GameObject.Find("Save_5");
            obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Save_Pos = obj1.transform.position;
            Jump_Reset();
        }
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

}
