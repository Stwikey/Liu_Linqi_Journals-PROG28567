using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    public Vector3 direction;
    public Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        //finds a random direction
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        //from that direction, finds the point the player wants to go to
        point = transform.position + direction.normalized * maxFloatDistance;
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement(){
        //move the asteroid towards the point based on moveSpeed
        transform.position += moveSpeed * (point - transform.position).normalized * Time.deltaTime;
        //check if it is in within distance of the point
        if((transform.position - point).magnitude <= arrivalDistance){
            Debug.Log("hu huh");
            //if it is, choose a new point
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            //offset a point in the stored direction by the max distance
            point = transform.position + direction.normalized * maxFloatDistance;

        }

    }
}
