using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject challangePopUp;
    public GameObject settingPopUp;

    private void Start()
    {
        settingPopUp.SetActive(false);
        challangePopUp.SetActive(true);
    }

    public void ChallangeBtn()
    {
        challangePopUp.SetActive(true);
    }

    public void RemovePopUpBtn()
    {
        challangePopUp.SetActive(false);
        settingPopUp.SetActive(false);
    }

    public void SettingBtn()
    {
        settingPopUp.SetActive(true);
    }

    public void HomeBtn()
    {
        Debug.Log("홈버튼 - 홈으로 돌아가기");
    }
}
