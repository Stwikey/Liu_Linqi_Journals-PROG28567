using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float angularSpeed = 10f;
    public Transform enemy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 1*angularSpeed*Time.deltaTime);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        float deltaAngle = ComputeShortestAngle(transform.up, (enemy.position - transform.position).normalized);
        
        float rotateDirection = Mathf.Sign(deltaAngle);
        float angleStep = angularSpeed*Time.deltaTime;

        if(angleStep < Mathf.Abs(deltaAngle)){
            transform.Rotate(0f, 0f, rotateDirection*angularSpeed*Time.deltaTime);
        }else{
            transform.Rotate(0f, 0f, deltaAngle);
        }

        if(Vector3.Dot(transform.up, (enemy.position - transform.position).normalized) >= 0){
            Debug.Log("In Front");
        }else{
            Debug.Log("Behind");
        }
    }

    public float ComputeShortestAngle(Vector3 a, Vector3 b){
        float angleA = Mathf.Atan2(a.y, a.x);
        float angleB = Mathf.Atan2(b.y, b.x);

        return Mathf.DeltaAngle(angleA, angleB);
    }

}
