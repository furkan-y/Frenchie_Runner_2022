using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    UIController uiController;

    [SerializeField] float carMoveSpeed;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
    }


    void Update()
    {
        if (gameObject.activeSelf && uiController.isGameStarted)
        {
            gameObject.transform.Translate(0, 0, -carMoveSpeed * Time.deltaTime);
        }
    }
}
