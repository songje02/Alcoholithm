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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("School"))  // 놀이터 -> 학교
        {
            GM.PlayerGround_Pos = new Vector3(this.transform.position.x -5, this.transform.position.y + 1, this.transform.position.z); //놀이터에서 플레이어 위치값 저장
            SceneManager.LoadScene("School");
        }
        else if (other.CompareTag("PlayGround")) // 학교 -> 놀이터
        {
            GM.School_Pos = new Vector3(this.transform.position.x - 3, this.transform.position.y + 1, this.transform.position.z); //학교에서 플레이어 위치값 저장
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
        

        movement = new Vector3(h, 0, v).normalized;
        transform.position += movement * speed * Time.deltaTime;  

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
    }
}
