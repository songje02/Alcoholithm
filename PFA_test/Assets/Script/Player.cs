using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    public int jumpCount = 0;
    private float speed = 0.0f;
    public float WalkSpeed = 5.0f;
    public float RunSpeed = 15.0f;
    public float jumpPower = 15.0f;

    bool jumping;
    Rigidbody rb;
    public GameManager GM;
    Animator anim;
    public Vector3 Save_Pos;
    bool isBorder;
    AudioSource audioSource;
    [SerializeField] AudioClip[] EffectSound;

    [Tooltip ("Camera Roteate Object")]
    private GameObject Follow_Cam;
    private float Target_Rotate;
    private float rotationVelocity;
    private float Rotation_SmoothTime = 0.12f;

    public float gravity = -20;
    float Jump_Timer = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChildRoom"))
        {
            SceneManager.LoadScene("School");
        }
        else if (other.CompareTag("School"))
        {
            SceneManager.LoadScene("PlayGround");
        }
        else if (other.CompareTag("PlayGround"))
        {
            SceneManager.LoadScene("End");
        }
    }

    void Start()
    {
        Follow_Cam = GameObject.Find("Main Camera");
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
        
        if (jumpCount < 2)
        {
            if (jumpCount == 0)
            {
                gravity = -20;
                jump();
            }
            else if (jumpCount == 1)
            {
                gravity = -10;
                Jump_Timer += Time.deltaTime;
                if (Jump_Timer >= 0.0025f)
                {
                    jumping = true;
                    jump();
                    Jump_Timer = 0;
                }
            }

        }
        else jumping = false;


        run();


        StopToWall();
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            if (!(moveInput.x == 0 && moveInput.y == 0))
            {
                Target_Rotate = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + Follow_Cam.transform.eulerAngles.y;
                float rotaton = Mathf.SmoothDampAngle(transform.eulerAngles.y, Target_Rotate, ref rotationVelocity, Rotation_SmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotaton, 0.0f);

                Vector3 targetDirection = Quaternion.Euler(0.0f, Target_Rotate, 0.0f) * Vector3.forward;
                transform.position += targetDirection.normalized * speed * Time.deltaTime;
            }
        }

        anim.SetBool("is_Walking", isMove);
    }


    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount++;

            if (jumping)
            {
                anim.SetBool("is_Jumping", true);
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                rb.AddForce(Vector3.down * gravity);
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
            speed = RunSpeed;
        }
        else
        {
            anim.SetBool("is_Running", false);
            speed = WalkSpeed;
        }
    }

    void Die()
    {
        audioSource.clip = EffectSound[0];
        audioSource.Play();
        this.gameObject.transform.position = Save_Pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Jump_Reset();
        }

        if (collision.gameObject.CompareTag("Die"))
        {
            Die();
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
            if(collision.transform.GetChild(0) != null)
            {
                GameObject obj1 = GameObject.Find("Save_1");
                obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Save_Pos = obj1.transform.position;
                Jump_Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Save_point_2"))
        {
            if (collision.transform.GetChild(0) != null)
            {
                GameObject obj1 = GameObject.Find("Save_2");
                obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Save_Pos = obj1.transform.position;
                Jump_Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Save_point_3"))
        {
            if (collision.transform.GetChild(0) != null)
            {
                GameObject obj1 = GameObject.Find("Save_3");
                obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Save_Pos = obj1.transform.position;
                Jump_Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Save_point_4"))
        {
            if (collision.transform.GetChild(0) != null)
            {
                GameObject obj1 = GameObject.Find("Save_4");
                obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Save_Pos = obj1.transform.position;
                Jump_Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Save_point_5"))
        {
            if (collision.transform.GetChild(0) != null)
            {
                GameObject obj1 = GameObject.Find("Save_5");
                obj1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Save_Pos = obj1.transform.position;
                Jump_Reset();
            }
        }
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        if(isBorder = Physics.Raycast(transform.position,
            transform.forward, 2.0f, LayerMask.GetMask("Wall")))
        {
            speed = 0;
        }
    }

}
