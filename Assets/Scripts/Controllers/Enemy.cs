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
    public Color radarColor;

    //public List<GameObject> bullets;
    //public List<Vector3> directions;
    public GameObject powerUp;

    int state = 0;
    public List<GameObject> bullets;
    public List<Vector3> directions;
    float attackTimer = 0.1f;
    public float  count;

    int patrolState = 0;

    public float rotateSpeed = 50f;
    public float degrees = 180f;

    public float degreeTracker = 0f;

    public float bulletTimer = 0.5f;
    float bulletTimerTracker;
    bool canShoot = true;




    private void Start(){
        playerPosition = playerTransform.position;
        acceleration = maxSpeed/accelerationTime;
        previousPosition = playerTransform.position;
    }
    private void Update()
    {
        //MECHANIC 1

        //face the player
        /**transform.up = (playerTransform.position - transform.position);

        if (state == 0){
            //enemy pauses
            IdleState();
        }else if(state == 1){
            //enemy dashes to player position
            DashState();
        }else if (state == 2){
            //enemy attacks player
            AttackState(1 , 7);
        }
        MoveBullets();
        CheckCollision();**/

        //EnemyMovement();

        //MECHANIC 3
        /**if(patrolState == 0){
            Patrol();
        }else if(patrolState == 1){
            ScanOne();
        }else if (patrolState == 2){
            ScanTwo();
        }else if (patrolState == 3){
            ScanThree();
        }

        VisionCone(30, 5f);**/
        
        //MECHANIC 4
        bulletTimerTracker += Time.deltaTime;
        if (bulletTimerTracker >= bulletTimer){
            //reset timer
            bulletTimerTracker = 0;
            //reset so enemy can shoot
            canShoot = true;
        }

        Radar(5f, 10);
        CheckCollision();
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

    //idle state, the enemy doesn't move
    public void IdleState(){
        //the time accumulates and keeps track of how much time has passed
        t += Time.deltaTime;
        //if the timer has been reached, log it to the console
        if (t >= timer){
            t = 0;
            state++;
            previousPosition = playerTransform.position;
            return;
        }
    }

    //enemy dashes to the player
    public void DashState(){
        //calculate the direction to the player
        Vector3 direction = previousPosition - transform.position;
        
        //calculate how much to move
        Vector3 offset = direction.normalized * speed * Time.deltaTime;

        if(direction.magnitude <= 0.0001){
            direction = Vector3.zero;
        }
        
        //check if the enemy is close enough to the player past position
        if (direction.magnitude <= offset.magnitude){
            //if it is, simply move the enemy to the position
            transform.position = previousPosition;
            //update the previous position to a new position
            previousPosition = playerTransform.position;
            state++;
        }else{
            //move the enemy to the player
            transform.position += offset;
        }

        
    }

    public void AttackState(float radius, int numOfBullets){
        //iterates through all the points
        for(int i = 1; i <= numOfBullets; i ++){
            //calculate the angle
            angle = Mathf.Deg2Rad*((360f/numOfBullets)*i);
            //calculate the point
            Vector3 Point = new Vector3(radius*Mathf.Cos(angle) + transform.position.x, radius*Mathf.Sin(angle) + transform.position.y, 0f);
            //spawn the power up
            GameObject bullet = Instantiate(powerUp, Point, Quaternion.identity);
            bullets.Add(bullet);
            directions.Add(Point - transform.position);
        }
        state = 0;

    }

    public void MoveBullets(){
        //iterated through all the bullets
        for(int i = 0; i < bullets.Count; i++){
            //move each bullet in the list by its direction
            bullets[i].transform.position += directions[i] * 20f * Time.deltaTime;
        }
    }

    public void CheckCollision(){
        for(int i = 0; i < bullets.Count; i++){
            //find the distance between the player and each bullet
            Vector3 distance = bullets[i].transform.position - playerTransform.position;
            if(distance.magnitude <= 0.75f){
                Debug.Log("Player Hit!");
            }
        }
    }


    public void Patrol(){
        //set the destination point
        Vector3 point = points[onPoint];
        //find the direction vector from the enemy to the point
        Vector3 direction = (point - transform.position);
        //find the offset that the enemy has to travel
        Vector3 offset = direction.normalized * speed * Time.deltaTime;
        //if the enemy is close to the point, snap to it
        if(direction.magnitude <= offset.magnitude){
            transform.position = point;
            patrolState++;
            //go to the next point
            if(onPoint == points.Count - 1){
                onPoint = 0;
            }else{
                onPoint ++;
            }
        }else{
            //other wise, move the enemy
            transform.position += offset;
        }

    }

    public void ScanOne(){
        //keeps track of how much the enemy already rotated
        float offset = rotateSpeed*Time.deltaTime;
        //if the rotation is close enough to the target rotation
        if ((degreeTracker + offset) >= degrees/2){
            transform.Rotate(0f, 0f, -(degrees/2-degreeTracker));//snap to the target rotation
            degreeTracker = 0;//reset the tracker
            patrolState++;//go to the next state
        }else{
            transform.Rotate(0f, 0f, -rotateSpeed*Time.deltaTime);//else, keep rotating normally
            degreeTracker += offset;//keeping track of how much the enemy already rotated
        }

    }
    public void ScanTwo(){
        //keeps track of how much the enemy already rotated
        float offset = rotateSpeed*Time.deltaTime;
        //if the rotation is close enough to the target rotation
        if ((degreeTracker + offset) >= degrees){
            transform.Rotate(0f, 0f, (degrees-degreeTracker));//snap to the target rotation
            degreeTracker = 0;//reset the tracker
            patrolState++;//go to the next state
        }else{
            transform.Rotate(0f, 0f, rotateSpeed*Time.deltaTime);//else, keep rotating normally
            degreeTracker += offset;//keeping track of how much the enemy already rotated
        }
    }
    public void ScanThree(){
        //keeps track of how much the enemy already rotated
        float offset = rotateSpeed*Time.deltaTime;
        //if the rotation is close enough to the target rotation
        if ((degreeTracker + offset) >= degrees/2){
            transform.Rotate(0f, 0f, -(degrees/2-degreeTracker));//snap to the target rotation
            degreeTracker = 0;//reset the tracker
            patrolState = 0;//go to the next state
        }else{
            transform.Rotate(0f, 0f, -rotateSpeed*Time.deltaTime);//else, keep rotating normally
            degreeTracker += offset;//keeping track of how much the enemy already rotated
        }
    }

    public void VisionCone(float degrees, float range){
        Vector3 coneDirection = transform.up; 
        degrees *= Mathf.Deg2Rad;//converts the degrees into radians
        float xPoint = coneDirection.x*Mathf.Cos(degrees/2) + coneDirection.y*Mathf.Sin(degrees/2);//right vector x position
        float yPoint = -coneDirection.x*Mathf.Sin(degrees/2) + coneDirection.y*Mathf.Cos(degrees/2);//right vector y position
        Debug.DrawLine(transform.position, new Vector3(xPoint, yPoint, 0f)*range + transform.position,Color.red);//draws right vector
        xPoint = coneDirection.x*Mathf.Cos(-degrees/2) + coneDirection.y*Mathf.Sin(-degrees/2);//left vector x position
        yPoint = -coneDirection.x*Mathf.Sin(-degrees/2) + coneDirection.y*Mathf.Cos(-degrees/2);//left vector y position
        Debug.DrawLine(transform.position, new Vector3(xPoint, yPoint, 0f)*range + transform.position,Color.red);//draws left vector
    
        Vector3 facingDirection = transform.up.normalized; //enemy's up facing direction
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;//direction from the enemy to the player
        float distance = (playerTransform.position-transform.position).magnitude;//distance from the enemy to the player
        float boundary = Mathf.Cos(degrees/2); //boundary of the cone
        if(boundary <= Vector3.Dot(facingDirection, directionToPlayer) && distance <= range){
            //checks if the player is in the enemy's cone and within distance
            Debug.Log("Player Detected!");
        }

    }

    public void Radar(float radius, int circlePoints){
        if ((transform.position - playerTransform.position).magnitude <= radius){
            radarColor = Color.red;
            Vector3 direction = playerTransform.position - transform.position;
            transform.up = direction.normalized;

            if(canShoot){
                GameObject bullet = Instantiate(powerUp, transform.up + transform.position, Quaternion.identity);
                bullets.Add(bullet);
                directions.Add((transform.position - bullet.transform.position).normalized);
                canShoot = false;
                bulletTimerTracker = 0;

            }
            


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

    
