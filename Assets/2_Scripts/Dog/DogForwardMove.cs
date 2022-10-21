using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogForwardMove : MonoBehaviour
{
    public float dogRunningSpeed = 4.0f;

    [SerializeField] Vector3 startPosition;

    public bool isGameStarted;

    void Start()
    {
        MoveDogToTheStartPlace();
        isGameStarted = false;
    }

    void Update()
    {
        if (isGameStarted)
        {
            gameObject.transform.Translate(0, 0, dogRunningSpeed * Time.deltaTime);
        }
    }


    private void MoveDogToTheStartPlace()
    {
        transform.position = startPosition;
    }
}
