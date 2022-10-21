using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    DogCollisionDetection dogCollisionDetection;

    void Start()
    {
        dogCollisionDetection = FindObjectOfType<DogCollisionDetection>();
    }

    private void OnTriggerEnter(Collider triggeredObject)
    {
        if ( (triggeredObject.gameObject.tag == "Obstacle") && (triggeredObject.gameObject.GetComponent<Car>() == null) || (triggeredObject.gameObject.tag == "Gold"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1f);
            Debug.Log("Magnet triggered and moved");
        }

    }


}
