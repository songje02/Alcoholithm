using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;


    public Vector3 PlayerGround_Pos; //��Ż �̵��� �÷��̾� ��ġ�� ����
    public Vector3 School_Pos;

    public bool is_School;
    public bool is_PlayGround;

    int Scene_Count = 0; // ���۽� ����Ǵ� ���� �̺�Ʈ ȣ��Ǳ� ������ ���

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ����ȯ�� �̺�Ʈ ȣ��
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
        Instantiate(player);
        is_School = false;
        is_PlayGround = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}