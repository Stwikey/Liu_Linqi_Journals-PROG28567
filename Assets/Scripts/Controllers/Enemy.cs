using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    
    public Transform playerTransform;
    public Vector3 playerPosition;
    public float maxSpeed = 3f;
    public float accelerationTime = 2f;
    float acceleration;
    public Vector3 velocity;

    private void Start(){
        playerPosition = playerTransform.position;
        acceleration = maxSpeed/accelerationTime;
    }
    private void Update()
    {
        EnemyMovement();
        
    }

    public void EnemyMovement(){
        //find the direction from the enemy position to the player position
        Vector3 direction = playerPosition - transform.position;
        //check if the distance between the enemy and the player is very near
        //velocity to move towards the player
        velocity += acceleration * direction.normalized * Time.deltaTime;
        if(direction.magnitude < velocity.magnitude){
            //reset the player position to the new position
            playerPosition = playerTransform.position;
        }
        
        transform.position += velocity * Time.deltaTime;


        if(velocity.magnitude >= maxSpeed){
            velocity = maxSpeed * velocity.normalized;

        }


    }

}
