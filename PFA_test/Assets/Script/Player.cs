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

    public bool SavePoint_1 = false;
    public bool SavePoint_2 = false;
    public bool SavePoint_3 = false;
    public bool SavePoint_4 = false;


    bool isBorder;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("School"))  // 놀이터 -> 학교
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

        SavePoint_1 = true;
        SavePoint_2 = false;
        SavePoint_3 = false;
        SavePoint_4 = false;
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
            Debug.Log(SavePoint_1);
            if (SavePoint_1)
            {
                this.gameObject.transform.position = new Vector3(0,0,0);
            }
            else if (SavePoint_2)
            {
                this.gameObject.transform.position = new Vector3();
            }
            else if (SavePoint_3)
            {
                this.gameObject.transform.position = new Vector3();
            }
            else if (SavePoint_4)
            {
                this.gameObject.transform.position = new Vector3();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            anim.SetBool("is_Jumping", false);
            jumping = true;
            jumpCount = 0;
        }

        if(collision.gameObject.CompareTag("savepoint1"))
        {
            anim.SetBool("is_Jumping", false);
            jumping = true;
            jumpCount = 0;

            //조명 켜지기
            GameObject obj1 = GameObject.Find("savepoint1");
            obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("savepoint2"))
        {
            anim.SetBool("is_Jumping", false);
            jumping = true;
            jumpCount = 0;

            //조명 켜지기
            GameObject obj2 = GameObject.Find("savepoint2");
            obj2.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        Check_Savepoint(collision);

    }

    void Check_Savepoint(Collision collision)
    {
        //if (collision.gameObject.CompareTag("save_one"))
        //{
        //    if (SavePoint_2 != true)
        //    {
        //        SavePoint_1 = true;
        //    }
        //}
        //else if (collision.gameObject.CompareTag("save_two"))
        //{
        //    if (SavePoint_3 != true)
        //    {
        //        SavePoint_1 = false;
        //        SavePoint_2 = true;
        //    }
        //}
        //else if (collision.gameObject.CompareTag("save_three"))
        //{
        //    if (SavePoint_4 != true)
        //    {
        //        SavePoint_2 = false;
        //        SavePoint_3 = true;
        //    }
        //}
        //else if (collision.gameObject.CompareTag("save_four"))
        //{
        //    SavePoint_3 = false;
        //    SavePoint_4 = true;
        //}
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

}
