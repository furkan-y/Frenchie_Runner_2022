using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public DogForwardMove dogForwardMove;
    public DogMove dogMove;

    public bool isGameStarted;

    public int countdown;
    public TextMeshProUGUI countdownTMP;
    public GameObject countdownTMPGO;

    public GameObject pausedPnl;
    public GameObject lostPnl;

    void Start()
    {
        Application.targetFrameRate = 50;

        StartCoroutine(startCountdown());

        if(pausedPnl.activeSelf == true)  { pausedPnl.SetActive(false);  }
        if (lostPnl.activeSelf == true) { pausedPnl.SetActive(false); }
    }


    IEnumerator startCountdown()
    {
        StopGame();
        countdownTMP.text = countdown.ToString();
        yield return new WaitForSecondsRealtime(1f);
        countdown--;
        countdownTMP.text = countdown.ToString();
        yield return new WaitForSecondsRealtime(1f);
        countdown--;
        countdownTMP.text = countdown.ToString();
        yield return new WaitForSecondsRealtime(1f);
        countdown--;
        countdownTMP.text = countdown.ToString();
        countdownTMPGO.SetActive(false);
        StartGame();
    }

    private void StopGame()
    {
        isGameStarted = false;
    }

    private void StartGame()
    {
        isGameStarted = true;
        dogForwardMove.isGameStarted = true;
        dogMove.GameStarted();
    }

    public void PauseButtonClicked()
    {
        if (pausedPnl.activeSelf == false)
        {
            pausedPnl.SetActive(true); 
        }
        Time.timeScale = 0.0f;
    }

    public void ContinueButtonClicked()
    {
        Time.timeScale = 1.0f;
        if (pausedPnl.activeSelf == true)
        {
            pausedPnl.SetActive(false);
        }
        // put 3 sec countdown
    }
    public void RestartBtnClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void PlayAgainBtnClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenuBtnClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOverFunction()
    {
        if (lostPnl.activeSelf == false)
        {
            lostPnl.SetActive(true);
        }
        Time.timeScale = 0.0f;
    }

}
