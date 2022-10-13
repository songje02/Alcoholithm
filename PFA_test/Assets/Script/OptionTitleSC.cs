using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTitleSC : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject title1;
    public GameObject title2;
    public GameObject check1, check2;
    public GameManager gameManager;
    private int state; // 설정창 상태 0 : 꺼짐, 1: 사운드 창 2: 게임종료 창

    void Start()
    {
        //title1 = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(0).gameObject;
        //Debug.Log(title1.name);
        //title2 = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(1).gameObject;
        //Debug.Log(title2.name);
        //check1 = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject;
        //Debug.Log(check1.name);
        //check2 = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject;
        //Debug.Log(check2.name);
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        state = 0;
        applyScreen();
    }

    public void change1()
    {
        state = 1;
        applyScreen();
    }

    public void change2()
    {
        state = 2;
        applyScreen();
    }

    public void applyScreen()
    {
        switch (state)
        {
            case 0 : title1.SetActive(false); title2.SetActive(false); break;
            case 1 : title1.SetActive(true); title2.SetActive(false); break;
            case 2 : title1.SetActive(false); title2.SetActive(true); break;
        }
    }

    public void exitGame()
    {
        gameManager.gameExit();
    }

    public void closeScreen()
    {
        state = 0;
        applyScreen();
    }

    public void soundToggle()
    {
        check1.SetActive(!check1.active);
        gameManager.soundToggle();
    }
    public void musicToggle()
    {
        check2.SetActive(!check2.active);
        gameManager.musicToggle();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(state == 0)
            {
                state = 1;
                applyScreen();
            }
            else
            {
                state = 0;
                applyScreen();
            }
        }
    }
}
