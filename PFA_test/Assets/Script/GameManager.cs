using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public bool isSoundOn;


    public Vector3 PlayerGround_Pos; //포탈 이동시 플레이어 위치값 변경
    public Vector3 School_Pos;

    public bool is_School;
    public bool is_PlayGround;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬전환시 이벤트 호출
    {
        if (scene.name == "PlayGround")
        {
            is_PlayGround = true;
            Instantiate(player, PlayerGround_Pos, transform.rotation);
        }
        else if (scene.name == "School")
        {
            is_School = true;
            Instantiate(player, School_Pos, transform.rotation);
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()  //싱글톤 패턴 사용
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
        Instantiate(player);
        is_School = false;
        is_PlayGround = false;

        isSoundOn = true;
    }

    public void soundToggle()
    {
        isSoundOn = !isSoundOn;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
