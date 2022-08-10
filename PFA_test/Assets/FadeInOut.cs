using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class FadeInOut : MonoBehaviour
{
    public float aniTime = 2f;         
    private Image fadeImage;           

    private float start = 1f;         
    private float end = 0f;          
    private float time = 0f;         


    public bool stopIn = false; 

    void Awake()
    { 
        fadeImage = GetComponent<Image>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (stopIn == false && time <= 2)
        {
            PlayFadeIn();
        }
        if (time >= 2 && stopIn == false)
        {
            stopIn = true;
            time = 0;
        }
    }

    void PlayFadeIn()
    {
        time += Time.deltaTime / aniTime;
        Color color = fadeImage.color;

        color.a = Mathf.Lerp(start, end, time);
  
        fadeImage.color = color;

    }
}
