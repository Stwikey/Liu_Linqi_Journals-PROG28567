using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class Pipepline : MonoBehaviour
{
    //variable to store the mouse position
    Vector2 mousepos;
    //variable to store the second mouse position
    Vector2 mouseend;
    //list to store the coordinates of the points
    public List<Vector2> coords;
    //variable to store the total length
    float magnitude = 0;
    //variable to keep track of the timer
    float t; 
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //Check if the left mouse button is held down
        if (Mouse.current.leftButton.isPressed){
            //store the mouse position inside a variable
            mousepos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //change the timer only when the mouse button is down
            t += Time.deltaTime;
        }

        
        //Check if the left mouse button was released
        if(Mouse.current.leftButton.wasReleasedThisFrame){
            //reset the timer
            t = 0;
            mousepos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //print the total magnitude of the vectors to the console
            Debug.Log(magnitude);
        }

        //if the timer is up
        if (t > 0.1){ 
            mouseend = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());//after 0.1s, store the current mouse position in the new variable
            coords.Add(mousepos);
            coords.Add(mouseend);
            //reset the timer
            t = 0;
        }

        //when a line is drawn, calculate the magnitude of the line and add it to the total length variable
        magnitude = 0;
        for(int i = 0; i < coords.Count - 1; i++){
            //redraws all the lines so they dont disappear from the screen.
            Debug.DrawLine(coords[i], coords[i + 1], Color.white);  
            magnitude += Mathf.Sqrt((coords[i].x - coords[i+1].x)*(coords[i].x - coords[i+1].x) + (coords[i].y - coords[i+1].y)*(coords[i].y - coords[i+1].y));

        }
         

    }
}
