using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class RowGeneration : MonoBehaviour
{
    public TMP_InputField input;
    //array that keeps track of all possible digits
    char[] digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
    int num;
    float size;
    Vector2 startpoint;
    public List<Vector2> coords;

    bool isInt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        size = 1f;
        Vector2 startpoint = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (coords.Count > 0){
            for (int i = 0; i <= coords.Count - 4; i += 4){
                Debug.DrawLine(coords[i], coords[i+2], Color.white);//right side
                Debug.DrawLine(coords[i+1], coords[i+3], Color.white);//left side
            }
            //top and bottom lines
            Debug.DrawLine(coords[coords.Count-1], coords[coords.Count-2]);
            Debug.DrawLine(coords[coords.Count-3], coords[coords.Count-4]);

        }

    }

    public void UpdateNumber(){
        Debug.Log(input.text);
    }

    //function to generate squares
    public void GenerateSquares(){
        //check if the input is an integer or not
        isInt = true;
        foreach(char i in input.text){
            for(int j = 0; j < digits.Length; j++){
                if (i == digits[j]){
                    break;
                }else{
                    if (j == 9){
                        isInt = false;
                    }
                }
            }
        }
        //if it is valid, generate squares side by side
        if (isInt){
            num = int.Parse(input.text);
            while (coords.Count > 0){
                coords.RemoveAt(0);
            }
            if (num % 2 == 0 && num != 0){
                GenerateEven();
            }else if (num != 0) {
                GenerateOdd();
            }
        }

    }

    //generates squares for an odd number
    public void GenerateOdd(){
        for(int i = 1; i <= num; i += 2){
            coords.Add(new Vector2(startpoint.x + i*size/2, startpoint.y + size/2));//top right
            coords.Add(new Vector2(startpoint.x - i*size/2, startpoint.y + size/2));//top left
            coords.Add(new Vector2(startpoint.x + i*size/2, startpoint.y - size/2));//bottom right
            coords.Add(new Vector2(startpoint.x - i*size/2, startpoint.y - size/2));//bottom left
        }
    }
    
    //generates squares for an even number
    public void GenerateEven(){
        for(int i = 0; i <= num; i += 2){
            coords.Add(new Vector2(startpoint.x + i*size/2, startpoint.y + size/2));//top right
            coords.Add(new Vector2(startpoint.x - i*size/2, startpoint.y + size/2));//top left
            coords.Add(new Vector2(startpoint.x + i*size/2, startpoint.y - size/2));//bottom right
            coords.Add(new Vector2(startpoint.x - i*size/2, startpoint.y - size/2));//bottom left
        }
    }
}
