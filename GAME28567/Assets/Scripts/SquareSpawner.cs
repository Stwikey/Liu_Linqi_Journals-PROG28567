using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class SquareSpawner : MonoBehaviour
{
    //variable to store the transparent colour
    Color transparent;
    
    //variables to store position of the mouse
    Vector2 mousepos;

    float mousex;
    float mousey;

    //variables to store the coordinates of squares
    Vector2 topleft;
    Vector2 botleft;
    Vector2 topright;
    Vector2 botright;

    //variable to store the sizes of the squares
    float length;
    float dist;

    //list to store coordinates
    public List<Vector2> coords;

    void Start()
    {
        mousepos = new Vector2();
        transparent = new Color(1f, 1f, 1f, 0.2f);
        length = 1f;
    }

    void Update()
    {
        //TASK A

        //sets the size of the square
        dist = length/2;

        //continuously store the current position of the mouse
        mousepos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousex = mousepos.x;
        mousey = mousepos.y;

        //continuously stores the coordinates of squares to be placed
        botleft = new Vector2(mousex - dist, mousey - dist); //bottom left point
        topleft = new Vector2(mousex - dist, mousey + dist); //top left point
        topright = new Vector2(mousex + dist, mousey + dist); //top right point
        botright = new Vector2(mousex + dist, mousey - dist); //bottom right point

        //check if the screen is clicked
        if (Mouse.current.leftButton.wasPressedThisFrame){
            //if the screen is clicked, store the coordinates of the position in a list
            coords.Add(botleft);
            coords.Add(topleft);
            coords.Add(topright);
            coords.Add(botright);
        }
        
        //draws all the squares in the list
        for (int i = 0; i <= coords.Count - 4; i += 4){
            Debug.DrawLine(coords[i], coords[i+1], Color.white);
            Debug.DrawLine(coords[i+2], coords[i+3], Color.white); 
            Debug.DrawLine(coords[i], coords[i+3], Color.white); 
            Debug.DrawLine(coords[i+1], coords[i+2], Color.white);
        }

        //TASK B

        //draw a semi-transparent square at the position of the mouse at all times with the size of the size variable
        Debug.DrawLine(botleft, topleft, transparent); //Draws left side
        Debug.DrawLine(botleft, botright, transparent); //Draws bottom side
        Debug.DrawLine(botright, topright, transparent); //Draws right side
        Debug.DrawLine(topleft, topright, transparent); //Draws top side

        //TASK C

        //check if the mouse wheel is scrolled up
        if (Mouse.current.scroll.ReadValue().y > 0){ //if the mouse wheel is scrolled up, increase the size stored in the size variable
            length += 0.5f;
        }
        //if the mouse wheel is scrolled down, decrease the size stored in the size variable
        if (Mouse.current.scroll.ReadValue().y < 0){
            length -= 0.5f;
        }

        
    }
}
