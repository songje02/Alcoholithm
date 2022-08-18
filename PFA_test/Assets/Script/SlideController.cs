using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        if (animator.GetBool("isOpen"))
            animator.SetBool("isOpen", false);
        else
            animator.SetBool("isOpen", true);
    }

    public void musicToggle()
    {
        gameManager.soundToggle();
    }

    public void gameExit()
    {
        gameManager.gameExit();
    }
}
