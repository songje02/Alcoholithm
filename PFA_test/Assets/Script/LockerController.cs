using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerController : MonoBehaviour
{

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
            active();
    }

    public void active()
    {
        if (animator.GetBool("isOpen"))
            animator.SetBool("isOpen", false);
        else
            animator.SetBool("isOpen", true);
    }


}
