using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    public TextMeshProUGUI ProgressTMP;
    public Slider progressSlider;
    public GameObject loadingScenePnl;

    private void Start()
    {
        if(loadingScenePnl.activeSelf == true)  {   loadingScenePnl.SetActive(false);   }
    }

    public void Load_Level()
    {
        StartCoroutine(Load_Progress());
    }

    IEnumerator Load_Progress()
    {
        AsyncOperation Operation = SceneManager.LoadSceneAsync(1);

        loadingScenePnl.SetActive(true);

        while (!Operation.isDone)
        {
            float progress = Mathf.Clamp01(Operation.progress / 0.9f);
            progressSlider.value = progress;
            ProgressTMP.text = "%" + progress * 100;

            Debug.Log(Operation.progress);
            yield return null;
        }


    }

}
