using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    //variable to store position of the mouse
    Vector2 mousepos;
    //variable to store the sizes of the squares
    //variable to store the white square sprite
    public GameObject whiteSquare;
    //variable to store the semi-transparent square sprite
    void Start()
    {
        mousepos = new Vector2();
    }

    void Update()
    {
        //TASK A

        //continuously store the current position of the mouse
        mousepos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //check if the screen is clicked
        if (Mouse.current.leftButton.isPressed){
            //if the screen is clicked, draw a white square at the position of the mouse with the size of the size variable
            Debug.DrawLine(mousepos, mousepos, Color.white);
        }
        
        //TASK B

        //draw a semi-transparent square at the position of the mouse at all times with the size of the size variable

        //TASK C

        //check if the mouse wheel is scrolled up
        //if the mouse wheel is scrolled up, increase the size stored in the size variable
        //if the mouse wheel is scrolled down, decrease the size stored in the size variable

        
    }
}
