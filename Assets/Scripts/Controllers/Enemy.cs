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
    public float angle;
    Color radarColor;

    public List<GameObject> bullets;
    public List<Vector3> directions;
    public GameObject powerUp;



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
        //Patrol(points, 180);

        Radar(3, 10);
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
    public void Radar(float radius, int circlePoints){
        if ((transform.position - playerTransform.position).magnitude <= radius){
            radarColor = Color.red;
            Vector3 direction = playerTransform.position - transform.position;
            transform.up = direction.normalized;

            GameObject bullet = Instantiate(powerUp, transform.up + transform.position, Quaternion.identity);
            bullets.Add(bullet);
            directions.Add((transform.position - bullet.transform.position).normalized);


            transform.position += speed * direction.normalized * Time.deltaTime;
        }else{
            radarColor = Color.green;
        }

        //iterates through all the points
        for(int i = 1; i <= circlePoints; i ++){
            //calculate the first angle
            angle = Mathf.Deg2Rad*((360f/circlePoints)*i);
            //calculate the first point
            Vector2 firstPoint = new Vector2(radius*Mathf.Cos(angle) + transform.position.x, radius*Mathf.Sin(angle) + transform.position.y);
            //calculate the second angle
            angle = Mathf.Deg2Rad*((360f/circlePoints)*(i+1));
            //calculate the second point
            Vector2 secondPoint = new Vector2(radius*Mathf.Cos(angle) + transform.position.x, radius*Mathf.Sin(angle) + transform.position.y);
            //draws a line from the first to the second point
            Debug.DrawLine(firstPoint, secondPoint, radarColor);
        }

        for(int i = 0; i < bullets.Count; i++){
            bullets[i].transform.position += -directions[i] * 20f * Time.deltaTime;
        }
    }

}

    
