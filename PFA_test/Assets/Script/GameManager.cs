using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;


    public Vector3 PlayerGround_Pos; //포탈 이동전 플레이어 위치값 저장
    public Vector3 School_Pos;

    public bool is_School;
    public bool is_PlayGround;

    int Scene_Count = 0; // 시작시 적용되는 씬도 이벤트 호출되기 때문에 사용

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬전환시 이벤트 호출
    {
        if(Scene_Count >= 2)
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
        Scene_Count += 1;     
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
