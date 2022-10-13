using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public bool isSoundOn, isMusicOn;

    public bool is_School;
    public bool is_PlayGround;
    public bool is_Children_Room;
    public AudioSource bgmPlayer;
    [SerializeField] AudioClip[] BackGround_Clip;


    [Header("Player_Pos")]
    public Vector3 PlayerGround_Pos; //��Ż �̵��� �÷��̾� ��ġ�� ����
    public Vector3 School_Pos;
    public Vector3 Children_Room;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ����ȯ�� �̺�Ʈ ȣ��
    {
        if (scene.name == "ChildRoom")
        {
            Instantiate(player, Children_Room, transform.rotation);
            bgmPlayer = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            bgmPlayer.clip = BackGround_Clip[0];
            bgmPlayer.Play();
            is_Children_Room = true;
            is_School = false;
            is_PlayGround = false;
        }
        else if (scene.name == "School")
        {
            Instantiate(player, School_Pos, transform.rotation);
            bgmPlayer = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            bgmPlayer.clip = BackGround_Clip[1];
            bgmPlayer.Play();
            is_Children_Room = false;
            is_School = true;
            is_PlayGround = false;
        }
        else if (scene.name == "PlayGround")
        {
            Instantiate(player, PlayerGround_Pos, transform.rotation);
            bgmPlayer = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            bgmPlayer.clip = BackGround_Clip[2];
            bgmPlayer.Play();
            is_Children_Room = false;
            is_School = false;
            is_PlayGround = true;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()  //�̱��� ���� ���
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        is_School = false;
        is_PlayGround = false;
        isSoundOn = true;
        isMusicOn = true;
    }

    public void soundToggle()
    {
        isSoundOn = !isSoundOn;
    }

    public void musicToggle()
    {
        isMusicOn = !isMusicOn;
    }

    public void gameExit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
