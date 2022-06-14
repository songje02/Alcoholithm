using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogCtr : MonoBehaviour
{
    public Text textBox;
    public GameObject canvas;

    public TextAsset textFile;
    public string[] textLines;
        
    public int currentLine;
    public static int CheckLine;
    public int endAtLine;

    public bool next = false;

    void Start()
    {
        CheckLine = currentLine;
        textBox.text = " ";

        // 텍스트 파일의 텍스트를 스트링 배열에 삽입
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
            // 엔터가 들어간 구간 기준으로 줄 구분 (자름)
        }
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
        StartCoroutine("TypingEffect");
    }

    IEnumerator BlinkingArrow()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("BlinkingArrow");
    }

    IEnumerator TypingEffect()
    {
        StopCoroutine("BlinkingArrow");
        string tempTextLine;
        tempTextLine = textLines[currentLine];
        textBox.text = " ";

        for (int i = 0; i < tempTextLine.Length; i++)
        {
            textBox.text += tempTextLine[i];
            yield return new WaitForSeconds(0.05f);
        }

        if (textBox.text != "")
        {
            StartCoroutine("BlinkingArrow");
        }
    }

    public void NextBtn()
    {              
        next = true;
    }

    private void Update()
    {
        if (next)
        {
            currentLine += 1;
            CheckLine += 1;
            if (currentLine > endAtLine)
            {
                StopCoroutine("TypingEffect");
                StopCoroutine("BlinkingArrow");
                canvas.SetActive(true);
            }
            else
            {
                StopCoroutine("TypingEffect");
                StartCoroutine("TypingEffect");
            }
            next = false;
        }
    }
}