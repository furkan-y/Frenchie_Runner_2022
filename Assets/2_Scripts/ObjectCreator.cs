using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public DogCollisionDetection dogCollisionDetection;
    public Transform dogParentPos;

    // collectibles
    public GameObject gold;
    public GameObject magnet;
    public Vector3 goldPosition;
    public Vector3 magnetPosition;
    // Sidestep
    public GameObject car;
    public GameObject tires;
    // Jump
    public GameObject cones;
    public GameObject fenceConcrete;
    public GameObject log;
    // BendOver
    public GameObject arch;
    public GameObject longBarrier;

    List<GameObject> collectibles;
    List<GameObject> obstacles;
    public GameObject CreatedCollectibles;
    public GameObject CreatedObstacles;

    public int numberOfGolds;
    public int numberOfMagnets;
    public int numberOfCars;
    public int numberOfTires;
    public int numberOfCones;
    public int numberOfFenceConcrete;
    public int numberOfLog;
    public int numberOfArch;
    public int numberOfLongBarrier;

    public float distanceOfCreatedObjects;
    public float timeBetweenACollectibleSpawn;
    public float timeBetweenAObstacleSpawn;

    public int numberOfCollectibleSpawnsAtTheBegining;
    public int numberOfObstacleSpawnsAtTheBegining;

    public List<int> leftObjectsOldSpawnPointsIntList;
    public List<int> middleObjectsOldSpawnPointsIntList;
    public List<int> rightObjectsOldSpawnPointsIntList;

    public int endDistanceOfTheBeginningSpawner;

    public int expiredMinutes;
    public int secondsForLvlUp;
    public int level;

    public float decreaseObsSpawnTimeEvrLvl;
    public float decreaseColleSpawnTimeEvrLvl;

    public bool isDogAlive;
    public int countDownSeconds; // There is same variable in UIController.cs too

    public int carSpawnLevel;

    void Start()
    {
        isDogAlive = true;
        expiredMinutes = 0;
        level = 1;
        countDownSeconds = 3;

        collectibles = new List<GameObject>();
        obstacles = new List<GameObject>();

        SetActiveFalseAllInteractiveObjects();
        FirstCreationOfInteractiveObjects();
        FirstSpawnOfInteractiveObjects();

        InvokeRepeating("IncreaseTheExpiredMinutes", 63f, 60f);
        InvokeRepeating("LevelUp", secondsForLvlUp, secondsForLvlUp);

        StartCoroutine(CreateCollectibleCoroutine(countDownSeconds + 1));
        StartCoroutine(CreateObstacleCoroutine(countDownSeconds + 1));

    }

    void SetActiveFalseAllInteractiveObjects()
    {
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(false);
        }
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }

    void FirstCreationOfInteractiveObjects()
    {
        FirstCreationOfCollectibles(gold, numberOfGolds, collectibles);
        FirstCreationOfCollectibles(magnet, numberOfMagnets, collectibles);
        FirstCreationOfObstacles(car, numberOfCars, obstacles);
        FirstCreationOfObstacles(tires, numberOfTires, obstacles);
        FirstCreationOfObstacles(cones, numberOfCones, obstacles);    // Jump
        FirstCreationOfObstacles(fenceConcrete, numberOfFenceConcrete, obstacles);
        FirstCreationOfObstacles(log, numberOfLog, obstacles);
        FirstCreationOfObstacles(arch, numberOfArch, obstacles);   // BendOver
        FirstCreationOfObstacles(longBarrier, numberOfLongBarrier, obstacles);
    }


    void FirstSpawnOfInteractiveObjects()
    {
        //  15 metreden ilk 60, metreye kadar rastgele objeler aktif edilecek dağıtılacak
        // COLLECTİBLES
        for (int i = 0; i <= numberOfCollectibleSpawnsAtTheBegining; i++)
        {
            int randCollectible = Random.Range(0, collectibles.Count);
            if (collectibles[randCollectible].activeSelf == false)
            {

                collectibles[randCollectible].SetActive(true);
            }
            else
            {
                numberOfCollectibleSpawnsAtTheBegining++;
            }

            int spawnPointAtZCollectible = Random.Range(15, endDistanceOfTheBeginningSpawner);
            int randomLaneCollectible = Random.Range(0, 3);

            if (randomLaneCollectible == 0)
            {
                if (!leftObjectsOldSpawnPointsIntList.Contains(spawnPointAtZCollectible))
                {
                    leftObjectsOldSpawnPointsIntList.Add(spawnPointAtZCollectible);
                    if (collectibles[randCollectible].tag == "Gold")
                    {
                        collectibles[randCollectible].transform.position = new Vector3(-1.5f, goldPosition.y, spawnPointAtZCollectible);
                    }
                    else if (collectibles[randCollectible].tag == "Magnet")
                    {
                        collectibles[randCollectible].transform.position = new Vector3(-1.5f - 0.288f, magnetPosition.y, spawnPointAtZCollectible);    // -0.288 magnets prefabs position
                    }
                }
                else
                {
                    numberOfCollectibleSpawnsAtTheBegining++;  // SpawnPointAlreadyUsing
                }
            }
            else if (randomLaneCollectible == 1)
            {
                if (!middleObjectsOldSpawnPointsIntList.Contains(spawnPointAtZCollectible))
                {
                    middleObjectsOldSpawnPointsIntList.Add(spawnPointAtZCollectible);
                    if (collectibles[randCollectible].tag == "Gold")
                    {
                        collectibles[randCollectible].transform.position = new Vector3(0f, goldPosition.y, spawnPointAtZCollectible);
                    }
                    else if (collectibles[randCollectible].tag == "Magnet")
                    {
                        collectibles[randCollectible].transform.position = new Vector3(0f - 0.288f, magnetPosition.y, spawnPointAtZCollectible);    // -0.288 magnets prefabs position
                    }
                }
                else
                {
                    numberOfCollectibleSpawnsAtTheBegining++;   // SpawnPointAlreadyUsing
                }
            }
            else if (randomLaneCollectible == 2)
            {
                if (!rightObjectsOldSpawnPointsIntList.Contains(spawnPointAtZCollectible))
                {
                    rightObjectsOldSpawnPointsIntList.Add(spawnPointAtZCollectible);
                    if (collectibles[randCollectible].tag == "Gold")
                    {
                        collectibles[randCollectible].transform.position = new Vector3(1.5f, goldPosition.y, spawnPointAtZCollectible);
                    }
                    else if (collectibles[randCollectible].tag == "Magnet")
                    {
                        collectibles[randCollectible].transform.position = new Vector3(1.5f - 0.288f, magnetPosition.y, spawnPointAtZCollectible);    // -0.288 magnets prefabs position
                    }
                }
                else
                {
                    numberOfCollectibleSpawnsAtTheBegining++;   // SpawnPointAlreadyUsing
                }
            }
        }



        // OBSTACLES
        for (int i = 0; i < numberOfObstacleSpawnsAtTheBegining; i++)
        {
            int randObstacle = Random.Range(0, obstacles.Count);
            if (obstacles[randObstacle].activeSelf == false && (obstacles[randObstacle].gameObject.GetComponent<Car>() == null))
            {
                obstacles[randObstacle].SetActive(true);
            }
            else
            {
                numberOfObstacleSpawnsAtTheBegining++;
            }

            int spawnPointAtZ = Random.Range(15, endDistanceOfTheBeginningSpawner);
            int randomLane = Random.Range(0, 3);

            if (randomLane == 0)
            {
                if (!leftObjectsOldSpawnPointsIntList.Contains(spawnPointAtZ))
                {
                    leftObjectsOldSpawnPointsIntList.Add(spawnPointAtZ);
                    obstacles[randObstacle].transform.position = new Vector3(-1.5f, 0f, spawnPointAtZ);
                }
                else
                {
                    numberOfObstacleSpawnsAtTheBegining++;  // SpawnPointAlreadyUsing
                }
            }
            else if (randomLane == 1)
            {
                if (!middleObjectsOldSpawnPointsIntList.Contains(spawnPointAtZ))
                {
                    middleObjectsOldSpawnPointsIntList.Add(spawnPointAtZ);
                    obstacles[randObstacle].transform.position = new Vector3(0f, 0f, spawnPointAtZ);
                }
                else
                {
                    numberOfObstacleSpawnsAtTheBegining++;   // SpawnPointAlreadyUsing
                }
            }
            else if (randomLane == 2)
            {
                if (!rightObjectsOldSpawnPointsIntList.Contains(spawnPointAtZ))
                {
                    rightObjectsOldSpawnPointsIntList.Add(spawnPointAtZ);
                    obstacles[randObstacle].transform.position = new Vector3(1.5f, 0f, spawnPointAtZ);
                }
                else
                {
                    numberOfObstacleSpawnsAtTheBegining++;   // SpawnPointAlreadyUsing
                }
            }
        }
    }



    // Magnetin spawnı x ekseninde -0.288 kayacak
    void SpawnCollectible()
    {
        foreach (GameObject collectible in collectibles)
        {
            if (collectible.activeSelf == false)
            {
                int randLane = Random.Range(0, 3);  // for random lanes
                int randSpawnDistance = Random.Range(30, 35);

                if (randLane == 0)
                {
                    if (collectible.tag == "Gold")
                    {
                        collectible.transform.position = new Vector3(-1.5f, goldPosition.y, dogParentPos.position.z + randSpawnDistance);
                    }
                    else if (collectible.tag == "Magnet")
                    {
                        collectible.transform.position = new Vector3(-1.5f - 0.288f, magnetPosition.y, dogParentPos.position.z + randSpawnDistance);    // -0.288 magnets prefabs position
                    }
                }
                else if (randLane == 1)
                {
                    if (collectible.tag == "Gold")
                    {
                        collectible.transform.position = new Vector3(0f, goldPosition.y, dogParentPos.position.z + randSpawnDistance);
                    }
                    else if (collectible.tag == "Magnet")
                    {
                        collectible.transform.position = new Vector3(0f - 0.288f, magnetPosition.y, dogParentPos.position.z + randSpawnDistance);
                    }
                }
                else if (randLane == 2)
                {
                    if (collectible.tag == "Gold")
                    {
                        collectible.transform.position = new Vector3(1.5f, goldPosition.y, dogParentPos.position.z + randSpawnDistance);
                    }
                    else if (collectible.tag == "Magnet")
                    {
                        collectible.transform.position = new Vector3(1.5f - 0.288f, magnetPosition.y, dogParentPos.position.z + randSpawnDistance);
                    }
                }

                if (collectible.gameObject.tag == "Gold")
                {
                    collectible.SetActive(true);
                }
                else if (collectible.gameObject.tag == "Magnet" && !dogCollisionDetection.isMagnetActive)
                {
                    collectible.SetActive(true);
                }

                return;
            }
        }
    }

    void SpawnObstacle()
    {

        int randObst = Random.Range(0, obstacles.Count);

        while(obstacles[randObst].gameObject.GetComponent<Car>() != null && level < carSpawnLevel)
        {
            randObst = Random.Range(0, obstacles.Count);
        }


        if (obstacles[randObst].activeSelf == false)
        {

            int randomLane = Random.Range(0, 3);
            if (randomLane == 0)
            {
                obstacles[randObst].transform.position = new Vector3(-1.5f, 0f, dogParentPos.position.z + distanceOfCreatedObjects);
            }
            else if (randomLane == 1)
            {
                obstacles[randObst].transform.position = new Vector3(0f, 0f, dogParentPos.position.z + distanceOfCreatedObjects);
            }
            else if (randomLane == 2)
            {
                obstacles[randObst].transform.position = new Vector3(1.5f, 0f, dogParentPos.position.z + distanceOfCreatedObjects);
            }
            obstacles[randObst].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {
                int randObst2 = Random.Range(0, obstacles.Count);
                while (obstacles[randObst2].gameObject.GetComponent<Car>() != null && level < carSpawnLevel)
                {
                    randObst2 = Random.Range(0, obstacles.Count);
                }
                if (obstacles[randObst2].activeSelf == false)
                {
                    int randomLane = Random.Range(0, 3);
                    if (randomLane == 0)
                    {
                        obstacles[randObst2].transform.position = new Vector3(-1.5f, 0f, dogParentPos.position.z + distanceOfCreatedObjects);
                    }
                    else if (randomLane == 1)
                    {
                        obstacles[randObst2].transform.position = new Vector3(0f, 0f, dogParentPos.position.z + distanceOfCreatedObjects);
                    }
                    else if (randomLane == 2)
                    {
                        obstacles[randObst2].transform.position = new Vector3(1.5f, 0f, dogParentPos.position.z + distanceOfCreatedObjects);
                    }
                    obstacles[randObst2].SetActive(true);
                    return;
                }
            }
        }

    }

    void FirstCreationOfCollectibles(GameObject newGameObject, int quantity, List<GameObject> list)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject newObject = Instantiate(newGameObject, CreatedCollectibles.transform);
            newObject.SetActive(false);
            list.Add(newObject);
        }
    }
    void FirstCreationOfObstacles(GameObject newGameObject, int quantity, List<GameObject> list)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject newObject = Instantiate(newGameObject, CreatedObstacles.transform);
            newObject.SetActive(false);
            list.Add(newObject);
        }
    }

    private void IncreaseTheExpiredMinutes()
    {
        expiredMinutes++;
        Debug.Log("expiredMinutes: " + expiredMinutes);
    }

    private void LevelUp()
    {
        level++;
        Debug.Log("LevelUP: " + level);
        if (timeBetweenACollectibleSpawn > 0.32f)
        {
            timeBetweenACollectibleSpawn -= decreaseColleSpawnTimeEvrLvl;
        }

        if (timeBetweenAObstacleSpawn > 0.32f)
        {
            timeBetweenAObstacleSpawn -= decreaseObsSpawnTimeEvrLvl;
        }

    }

    IEnumerator CreateCollectibleCoroutine(int countDownSeconds)
    {
        yield return new WaitForSeconds(countDownSeconds);
        while (isDogAlive)
        {
            SpawnCollectible();
            yield return new WaitForSeconds(timeBetweenACollectibleSpawn); 
        }
    }

    IEnumerator CreateObstacleCoroutine(int countDownSeconds)
    {
        yield return new WaitForSeconds(countDownSeconds);
        while (isDogAlive)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(timeBetweenAObstacleSpawn);
        }
    }

}
