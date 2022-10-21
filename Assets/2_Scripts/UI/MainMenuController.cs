using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    LoadingScene loadingScene;

    [SerializeField] GameObject settingsPnl;
    [SerializeField] GameObject creditsPnl;

    void Start()
    {
        Time.timeScale = 1.0f;

        if (settingsPnl.activeSelf ==true)
        {
            settingsPnl.SetActive(false);
        }
        if (creditsPnl.activeSelf == true)
        {
            creditsPnl.SetActive(false);
        }
    }

    public void PlayBtnClicked()
    {
        loadingScene.Load_Level();
        //SceneManager.LoadScene(1);
    }
    public void SettingsBtnClicked()
    {
        settingsPnl.SetActive(true);
    }
    public void FromSettingsToMenuClicked()
    {
        settingsPnl.SetActive(false);
    }

    public void CreditsBtnClicked()
    {
        creditsPnl.SetActive(true);
    }
    public void FromCreditsToMenuBtnClicked()
    {
        creditsPnl.SetActive(false);
    }

    public void QuitBtnClicked()
    {
        Application.Quit();
    }

}
