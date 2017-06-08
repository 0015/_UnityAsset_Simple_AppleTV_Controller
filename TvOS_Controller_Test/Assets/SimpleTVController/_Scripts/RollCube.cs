using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollCube : MonoBehaviour {

	private bool  ismoving = false;  
	private float startY = 0;  
	public float cubeSpeed;  
	public float cubeSize;


	private float fingerStartTime  = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
  
    private bool isSwipe = false;
    private float minSwipeDist  = 50.0f;
    private float maxSwipeTime = 0.5f;
    
	// Update is called once per frame
	void Update () {
	    onSwipeEvent();
	}
  
	 void onSwipeEvent () {
       
        if (Input.touchCount > 0){

            foreach (UnityEngine.Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                case TouchPhase.Began :
                /* this is a new touch */
                isSwipe = true;
                fingerStartTime = Time.time;
                fingerStartPos = touch.position;
                break;
                
                case TouchPhase.Canceled :
                /* The touch is being canceled */
                isSwipe = false;
                break;
                
                case TouchPhase.Ended :

                float gestureTime = Time.time - fingerStartTime;
                float gestureDist = (touch.position - fingerStartPos).magnitude;
                    
                if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
                    Vector2 direction = touch.position - fingerStartPos;
                    Vector2 swipeType = Vector2.zero;
                    
                    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
                    // the swipe is horizontal:
                    swipeType = Vector2.right * Mathf.Sign(direction.x);
                    }else{
                    // the swipe is vertical:
                    swipeType = Vector2.up * Mathf.Sign(direction.y);
                    }

                    if(swipeType.x != 0.0f){
                    if(swipeType.x > 0.0f){
                        // MOVE RIGHT
						ismoving = true;  
						transform.Find("targetpoint").Translate(cubeSize/2, -cubeSize/2, 0);  
						StartCoroutine( DoRoll(transform.Find("targetpoint").position, -Vector3.forward, 90.0f, cubeSpeed));     
                        

                    }else{
                        // MOVE LEFT
						ismoving = true;  
						transform.Find("targetpoint").Translate(-cubeSize/2, -cubeSize/2, 0);  
						StartCoroutine( DoRoll(transform.Find("targetpoint").position, Vector3.forward, 90.0f, cubeSpeed));  
                    }
                    }

                    if(swipeType.y != 0.0f ){
                    if(swipeType.y > 0.0f){
                        // MOVE UP
                        ismoving = true;  
						transform.Find("targetpoint").Translate(0, -cubeSize/2 , cubeSize/2);  
						StartCoroutine( DoRoll(transform.Find("targetpoint").position, Vector3.right, 90.0f, cubeSpeed));  
                        
                    }else{
                        // MOVE DOWN
                        ismoving = true;  
						transform.Find("targetpoint").Translate(0, -cubeSize/2, -cubeSize/2);  
						StartCoroutine( DoRoll(transform.Find("targetpoint").position, -Vector3.right, 90.0f, cubeSpeed)); 
                        
                    }
                    }

                }
                    
                break;
                }
            }
    	}
	}

	IEnumerator DoRoll (Vector3 aPoint, Vector3 aAxis, float aAngle, float aDuration){    
	
	float tSteps= Mathf.Ceil(aDuration * 30.0f);  
	float tAngle= aAngle / tSteps;  
	Vector3 pos; // declare variable to fix the y position  
	
	// Rotate the cube by the point, axis and angle  
	for (int i= 1; i <= tSteps; i++)   
	{   
		transform.RotateAround (aPoint, aAxis, tAngle);  
		yield return new WaitForSeconds(0.0033333f);  
	}   
	
	// move the targetpoint to the center of the cube   
	transform.Find("targetpoint").position = transform.position;  
	
	// Make sure the y position is correct   
	pos = transform.position;  
	pos.y = startY;  
	transform.position = pos;  
		
	// Make sure the angles are snaping to 90 degrees.       
	Vector3 vec = transform.eulerAngles;  
	vec.x = Mathf.Round(vec.x / 90) * 90;  
	vec.y = Mathf.Round(vec.y / 90) * 90;  
	vec.z = Mathf.Round(vec.z / 90) * 90;  
	transform.eulerAngles = vec;  
		
	// The cube is stoped  
	ismoving = false;       
	}  
}
