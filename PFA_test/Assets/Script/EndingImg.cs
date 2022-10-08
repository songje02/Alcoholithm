using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingImg : MonoBehaviour
{
    public Sprite sprite02;
    public Image img;

    public float time = 5f;

    void Start()
    {
    }

    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            img.sprite = sprite02;
        }
    }
}
