    using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    
    [SerializeField] public Vector3 offset = new Vector3(0f, 1f, 0f);

    public int bombTrailSpacing = 1;
    public int numberOfTrailBombs = 3;
    
    public int dist = 2;

    public float ratio = 2f;
    // Update is called once per frame
    void Update()
    {
        //checks if the b key is pressed
        if(Keyboard.current.bKey.wasPressedThisFrame){
            //if the b key is pressed, spawn a bomb at an offset
            SpawnBombAtOffset(offset);
        }

        if(Keyboard.current.tKey.wasPressedThisFrame){
            //if the t key is pressed, spawn a bomb at an offset
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }   

        if(Keyboard.current.oKey.wasPressedThisFrame){
            //if the oKey is pressed, spawn a corner bomb
            SpawnBombOnRandomCorner(dist);
        }

        if(Keyboard.current.wKey.wasPressedThisFrame){
            //if the w key is pressed, warp to the player
            WarpPlayer(enemyTransform, ratio);
        }

        //draw a green line to the asteroids within range
        DetectAsteroids(100f, asteroidTransforms);


    }

    //function to spawn a bomb at an offset
    public void SpawnBombAtOffset(Vector3 inOffset){
        Instantiate(bombPrefab, transform.position + offset, Quaternion.identity);

    }

    public Vector2 Normalize(Vector2 vector){
        float magnitude = Mathf.Sqrt(vector.x*vector.x + vector.y*vector.y);
        Vector2 normalized = new Vector2(vector.x/magnitude, vector.y/magnitude);
        return normalized;

    }

    //function to spawn a bomb trail
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs){
        //for loop to spawn bombs 
        for(int i = 1; i <= inNumberOfBombs; i++){
            //inside the for loop, offset the bomb positions by the spacing variable
            Instantiate(bombPrefab, transform.position - transform.up*i*inBombSpacing, Quaternion.identity);
        }

    }

    public void SpawnBombOnRandomCorner(float inDistance){
        //pick a random number from 0-4 (0, 1, 2, 3)
        int corner = Random.Range(0, 4);
        //if the number is 0
        if(corner == 0){
        //spawn the bomb in the top left corner
            Instantiate(bombPrefab, transform.position + (transform.up + transform.right*-1).normalized*inDistance , Quaternion.identity);
        }
        //if the number is 1
        if(corner == 1){
        //spawn the bomb in the top right corner
            Instantiate(bombPrefab, transform.position + (transform.up + transform.right).normalized*inDistance , Quaternion.identity);
        }

        //if the number is 2
        if(corner == 2){
        //spawn the bomb in the bottom right corner
            Instantiate(bombPrefab, transform.position + (transform.up*-1 + transform.right).normalized*inDistance , Quaternion.identity);
        }

        //if the number is 3
        if(corner == 3){
        //spawn the bomb in the bottom left corner
            Instantiate(bombPrefab, transform.position + (transform.up*-1 + transform.right*-1).normalized*inDistance , Quaternion.identity);
        }

    }

    public void WarpPlayer(Transform target, float ratio){
        //check if ratio is less than or equal to one
        if (ratio > 1){
            ratio = 1;
        }
        //lerp towards the player 
        transform.position = Vector3.Lerp(transform.position, target.position, ratio);
    }

    
    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids){
        for(int i = 0; i < inAsteroids.Count; i++){
            //check if the asteroid is in range
            if(Vector3.Distance(inAsteroids[i].position, transform.position) <= inMaxRange){
                //draw a line from the player position to the enemy position
                Debug.DrawLine(transform.position, transform.position + (inAsteroids[i].position-transform.position).normalized*2.5f, Color.green);
            }
        }
    }

}
