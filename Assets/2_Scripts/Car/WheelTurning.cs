using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurning : MonoBehaviour
{
    [SerializeField] int wheelTurningSpeed = 7;

    void Start()
    {
        
    }

    // Speed of the turning wheel
    void Update()
    {
        gameObject.transform.Rotate(wheelTurningSpeed, 0, 0);
    }
}
