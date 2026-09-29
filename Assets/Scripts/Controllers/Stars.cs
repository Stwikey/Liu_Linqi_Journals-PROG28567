using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;
    int index;

    float timer;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        //Iterate through the list of stars
        /**for(int i = 0; i < starTransforms.Count-1; i++){
            Debug.DrawLine(starTransforms[i].position, starTransforms[i+1].position, Color.green);
        }**/
        timer += Time.deltaTime;

        //draw the line
        Vector3 endPosition = Vector3.Lerp(starTransforms[index].position, starTransforms[index + 1].position, timer/drawingTime);
        Debug.DrawLine(starTransforms[index].position, endPosition, Color.green);

        
        //reset the timer
        if (timer >= drawingTime){
            timer = 0;
            if(index == starTransforms.Count - 2){
                index = 0;
            }else{
                index++;
            }
        }

    }
}
