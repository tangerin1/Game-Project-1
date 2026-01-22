using UnityEngine;

public class CameraControls : MonoBehaviour
{
    int currentAngle = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) //move forward
        {
            transform.Rotate(-currentAngle,0,0);
            transform.Translate(0,0,.05f);
            transform.Rotate(currentAngle,0,0);
        }

        if(Input.GetKey(KeyCode.A)) //move left
        {
            transform.Rotate(-currentAngle,0,0);
            transform.Translate(-.05f,0,0);
            transform.Rotate(currentAngle,0,0);
        }

        if(Input.GetKey(KeyCode.S)) //move backward
        {
    
            transform.Rotate(-currentAngle,0,0);
            transform.Translate(0,0,-.05f);
            transform.Rotate(currentAngle,0,0);
        }

        if(Input.GetKey(KeyCode.D)) //move right
        {
            transform.Rotate(-currentAngle,0,0);
            transform.Translate(.05f,0,0);
            transform.Rotate(currentAngle,0,0);
        }

        if(Input.GetKey(KeyCode.LeftArrow)) //rotate left
        {
            transform.Rotate(-currentAngle,0,0);
            transform.Rotate(0,-1,0);
            transform.Rotate(currentAngle,0,0);
        }

        if(Input.GetKey(KeyCode.RightArrow)) //rotate right
        {
            transform.Rotate(-currentAngle,0,0);
            transform.Rotate(0,1,0);
            transform.Rotate(currentAngle,0,0);
        }

        if(Input.GetKey(KeyCode.DownArrow)&&currentAngle<90) //rotate down
        {
            transform.Rotate(1,0,0);
            currentAngle++;
        }

        if(Input.GetKey(KeyCode.UpArrow)&&currentAngle>-90) //rotate up
        {
            transform.Rotate(-1,0,0);
            currentAngle--;
        }
        
    }

}
