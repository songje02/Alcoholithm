using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    float time = 0;
    bool isTrigger = false;
    bool isOpen = false;
    
    Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }


    private void Update()
    {
        if (isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("ø≠∑¡∂Û ¬¸±˙");
                ani.SetTrigger("doorOpen");
                isOpen=true;
                isTrigger = false;
            }
        }

        if(isOpen)
        {
            time+=Time.deltaTime;
            if (time>3.0f)
            {
                ani.SetTrigger("doorClose");
                time =0;
                isOpen = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="Player_character(Clone)")
        {
            isTrigger= true;
            Debug.Log("¥‚¿Ω");
        }
    }

    
}
