using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    DogCollisionDetection dogCollisionDetection;
    public GameObject navyParent;
    public GameObject navy;
    public float goldRotationSpeed;
    public float goldMoveSpeed;

    private void Start()
    {
        dogCollisionDetection = FindObjectOfType<DogCollisionDetection>();
        navyParent = GameObject.FindWithTag("DogParent");
        navy = GameObject.FindWithTag("Dog");
    }

    void Update()
    {
       
        if (dogCollisionDetection.isMagnetActive)
        {
            float distance = Vector3.Distance(transform.position, navyParent.transform.position);

            if(distance <= 6f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(navy.transform.position.x, navyParent.transform.position.y, navyParent.transform.position.z), goldMoveSpeed * Time.deltaTime);
            }         
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            transform.Rotate(0, goldRotationSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider triggeredObject)
    {
        if (triggeredObject.gameObject.tag == "Obstacle" && !dogCollisionDetection.isMagnetActive && (triggeredObject.gameObject.GetComponent<Car>() == null))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1f);
        }

    }

}
