using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Moves : MonoBehaviour
{
    [SerializeField] Transform dogs_position;
    Vector3 distance;
    [SerializeField] float cameraDistance;    

    void LateUpdate()
    {
        distance = new Vector3(dogs_position.position.x, transform.position.y, dogs_position.position.z - cameraDistance);
        transform.position = Vector3.Lerp(transform.position, distance, 7.5f * Time.deltaTime);
    }
}
