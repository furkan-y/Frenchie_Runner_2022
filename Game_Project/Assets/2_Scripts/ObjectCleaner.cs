using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{

    private void OnTriggerEnter(Collider triggeredObject)
    {
        if (triggeredObject.tag == "Gold" || triggeredObject.tag == "Magnet")
        {
            triggeredObject.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collidedObject)
    {
        if (collidedObject.gameObject.tag == "Obstacle")
        {
            collidedObject.gameObject.SetActive(false);
        }
    }

}
