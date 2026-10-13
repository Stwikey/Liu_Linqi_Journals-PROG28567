using UnityEngine;
using UnityEngine.InputSystem;


public class DotProductExercise : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float redAngle;
    public float blueAngle;

    Vector2 redVector;
    Vector2 blueVector;
    Vector2 startPoint;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        redVector = new Vector2(Mathf.Cos(redAngle*Mathf.Deg2Rad), Mathf.Sin(redAngle*Mathf.Deg2Rad));
        blueVector = new Vector2(Mathf.Cos(blueAngle*Mathf.Deg2Rad), Mathf.Sin(blueAngle*Mathf.Deg2Rad));
        startPoint = Vector2.zero;

        Debug.DrawLine(startPoint, redVector, Color.red);
        Debug.DrawLine(startPoint, blueVector, Color.blue);

        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            Debug.Log((redVector.x * blueVector.x + redVector.y * blueVector.y));
        }

    }
}
