using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject image;
    FadeInOut fade;

    void Start()
    {
        fade = GameObject.Find("Image").GetComponent<FadeInOut>();
        image.SetActive(false);
    }

    void Update()
    {
        if (this.transform.position.y < -40)
        {
            image.SetActive(true);
            if (fade.stopIn == false && fade.time <= 2)
            {
                fade.PlayFadeIn();
            }
            if (fade.time >= 2 && fade.stopIn == false)
            {
                fade.stopIn = true;
                fade.time = 0;
            }
            transform.position = new Vector3(62.5f, 5, -53.7f);
        }
    }
}
