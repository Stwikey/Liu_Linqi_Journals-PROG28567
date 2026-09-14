using UnityEngine;


public class DrawVectors : MonoBehaviour
{
    void Update()
    {
        // Create two Vector2 variables, "dVector" and "eVector"
        Vector2 dVector;
        Vector2 eVector;
        // Set their values to dVector: (0, 1) and eVector: (3, -2)
        dVector = new Vector2(0f, 1f);
        eVector = new Vector2(3f, -2f);
        // Use Debug.DrawLine to draw a yellow vector starting at the origin and ending at dVector.
        Debug.DrawLine(Vector2.zero, dVector, Color.yellow);
        // Use Debug.DrawLine to draw a grey vector starting at the origin and ending at eVector.
        Debug.DrawLine(Vector2.zero, eVector, Color.grey);

    }
}
