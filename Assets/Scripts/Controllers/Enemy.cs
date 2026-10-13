using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    
    public Transform playerTransform;
    public Vector3 playerPosition;
    public float maxSpeed = 3f;
    public float accelerationTime = 2f;
    float acceleration;
    public Vector3 velocity;

    public float timer = 3f;
    float t = 0f;

    public Vector3 previousPosition; 
    public float speed = 1f;
    int onPoint = 0;

    public List<Vector3> points;

    private void Start(){
        playerPosition = playerTransform.position;
        acceleration = maxSpeed/accelerationTime;
        previousPosition = playerTransform.position;
    }
    private void Update()
    {
        //EnemyMovement();
        //IdleState();
        //DashState();
        Patrol(points, 180);
    }

    /**public void EnemyMovement(){
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


    }**/

    //idle state, the enemy doesn't move
    public void IdleState(){
        //the time accumulates and keeps track of how much time has passed
        t += Time.deltaTime;
        //if the timer has been reached, log it to the console
        if (t >= timer){
            t = 0;
            Debug.Log("Time Reached!");
        }
    }

    //enemy dashes to the player
    /**public void DashState(){
        //calculate the direction to the player
        Vector3 direction = previousPosition - transform.position;
        
        //calculate how much to move
        Vector3 offset = direction.normalized * speed * Time.deltaTime;
        
        //chekc if the enemy is close enough to the player past position

        //Debug.Log(direction.magnitude);
       // Debug.Log(offset.magnitude);
        if (direction.magnitude <= offset.magnitude){
            //if it is, simply move the enemy to the position
            transform.position = previousPosition;
            //update the previous position to a new position
            previousPosition = playerTransform.position;
            Debug.Log("position reached");
        }else{
            //move the enemy to the player
            transform.position += offset;
        }

        
    }**/

    public void Patrol(List<Vector3> points, float degrees){
        Vector3 point = points[onPoint];
        Vector3 direction = (point - transform.position);
        Vector3 offset = direction.normalized * speed * Time.deltaTime;
    
        if(offset.magnitude <= direction.magnitude){
            transform.position = point;
            if(onPoint == points.Count - 1){
                onPoint = 0;
            }else{
                onPoint ++;
            }
        }else{
            transform.position += offset;
        }

    }

}
