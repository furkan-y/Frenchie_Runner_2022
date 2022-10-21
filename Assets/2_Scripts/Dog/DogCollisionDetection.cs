using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DogCollisionDetection : MonoBehaviour
{
    public DogMove dogMove;
    public UIController uIController;
    public DogForwardMove dogForwardMove;
    public GameMusicManager gameMusicManager;
    public ObjectCreator objectCreator;

    [SerializeField] Transform Road_1_Transf;
    [SerializeField] Transform Road_2_Transf;
    [SerializeField] Transform Road_3_Transf;
    [SerializeField] Transform Road_4_Transf;
    [SerializeField] Transform Road_5_Transf;
    [SerializeField] Transform Road_6_Transf;

    public int totalGold;
    public TMPro.TextMeshProUGUI totalGoldTMP;

    public bool isMagnetActive = false;
    public int expireTimeOfActiveMagnet;

    void Start()
    {
        totalGold = 0;
        totalGoldTMP.text = totalGold.ToString();
        isMagnetActive = false;

    }


    private void OnCollisionEnter(Collision collidedObject)
    {
        if (collidedObject.gameObject.tag == "Obstacle")
        {
            Debug.Log("DEATH: " + collidedObject.gameObject.name);
            StartCoroutine(DogHitTheObstacle());
        }
    }

    private void OnTriggerEnter(Collider triggeredObject)
    {
        if (triggeredObject.gameObject.tag == "Gold")
        {
            totalGold++;
            totalGoldTMP.text = totalGold.ToString();
            triggeredObject.gameObject.SetActive(false);
            gameMusicManager.GoldCollectedSoundFunc();
        }
        else if (triggeredObject.gameObject.tag == "Magnet")
        {            
            StartCoroutine(MagnetActive(triggeredObject.gameObject));
        }

        if (triggeredObject.gameObject.name == "Road_1")
        {
            StartCoroutine(MoveTheRoad_6());
        }
        else if(triggeredObject.gameObject.name == "Road_2")
        {
            StartCoroutine(MoveTheRoad_5());

        }
        else if (triggeredObject.gameObject.name == "Road_3")
        {
            StartCoroutine(MoveTheRoad_4());

        }
        else if (triggeredObject.gameObject.name == "Road_4")
        {
            StartCoroutine(MoveTheRoad_3());
        }
        else if (triggeredObject.gameObject.name == "Road_5")
        {
            StartCoroutine(MoveTheRoad_2());

        }
        else if (triggeredObject.gameObject.name == "Road_6")
        {
            StartCoroutine(MoveTheRoad_1());

        }

    }

    IEnumerator MagnetActive(GameObject triggeredObject)
    {
        triggeredObject.gameObject.SetActive(false);
        isMagnetActive = true;
        yield return new WaitForSeconds(expireTimeOfActiveMagnet);
        isMagnetActive = false;
    }

    IEnumerator MoveTheRoad_6()
    {
        yield return new WaitForSecondsRealtime(2f);
        Road_6_Transf.position = new Vector3(0, 0, Road_5_Transf.position.z + 20.425f);
    }
    IEnumerator MoveTheRoad_5()
    {
        yield return new WaitForSecondsRealtime(2f);
        Road_1_Transf.position = new Vector3(0, 0, Road_6_Transf.position.z + 20.425f);
    }
    IEnumerator MoveTheRoad_4()
    {
        yield return new WaitForSecondsRealtime(2f);
        Road_2_Transf.position = new Vector3(0, 0, Road_1_Transf.position.z + 20.425f);
    }
    IEnumerator MoveTheRoad_3()
    {
        yield return new WaitForSecondsRealtime(2f);
        Road_3_Transf.position = new Vector3(0, 0, Road_2_Transf.position.z + 20.425f);
    }
    IEnumerator MoveTheRoad_2()
    {
        yield return new WaitForSecondsRealtime(2f);
        Road_4_Transf.position = new Vector3(0, 0, Road_3_Transf.position.z + 20.425f);
    }
    IEnumerator MoveTheRoad_1()
    {
        yield return new WaitForSecondsRealtime(2f);
        Road_5_Transf.position = new Vector3(0, 0, Road_4_Transf.position.z + 20.425f);
    }

    IEnumerator DogHitTheObstacle()
    {
        dogForwardMove.dogRunningSpeed = 0f;
        dogMove.DogHitTheObstacle();
        gameMusicManager.DogDeathSoundFunc();
        objectCreator.isDogAlive = false;
        yield return new WaitForSeconds(1f);
        uIController.GameOverFunction(); 
    }

}
