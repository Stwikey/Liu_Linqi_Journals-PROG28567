using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(3, 2, planetTransform);

    }

    public void OrbitalMotion(float radius, float speed, Transform target){
        //calculate the angle
        angle += speed*Time.deltaTime;
        //calculate the point
        Vector3 point = new Vector3(radius*Mathf.Cos(angle) + target.position.x, radius*Mathf.Sin(angle) + target.position.y, 0f);
        //spawn the power up
        transform.position += (point - transform.position);
        
    }

    
}
