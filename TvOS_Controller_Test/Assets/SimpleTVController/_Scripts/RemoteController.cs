using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RemoteController : MonoBehaviour {

	public Text LogText;
	public Text LogText2;
	public Text LogText3;
	public Text LogText4;
	public Text LogText5;

	public Button CenterBtn;
	public Button LeftBtn;
	public Button RightBtn;
	public Button UpBtn;
	public Button DownBtn;

	private float fingerStartTime  = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
  
    private bool isSwipe = false;
    private float minSwipeDist  = 50.0f;
    private float maxSwipeTime = 0.5f;
    private bool isSelectorScreen = false;
	
	void Awake()
	{
        #if !UNITY_EDITOR && UNITY_TVOS
        	UnityEngine.Apple.TV.Remote.touchesEnabled = true;
			UnityEngine.Apple.TV.Remote.allowExitToHome = false;
		#endif
	}

	void Start () {
		LogText.text = "Starting Test";
		LogText2.text = "";
		LogText3.text = "";
		LogText4.text = "";
		LogText5.text = "";
	}
	
	void Update () {
		onTouchEvent();
		onSwipeEvent();
		onButtonEvent();
	}

	void onButtonEvent(){
		if(Input.GetButton("joystick button 0")){
			LogText5.text = "MENU Button";
		}

		if(Input.GetButton("joystick button 15")){
			LogText5.text = "Play/Pause Button";
		}

		if(Input.GetButtonDown("Submit")){
			LogText5.text = "TouchPad Clicked";
		}
	}


	void onTouchEvent(){


		string fingerStatus ="";
		string direction ="";

		if (Input.touchCount > 0){
        
			foreach (UnityEngine.Touch touch in Input.touches) {

						switch(touch.phase)	{
						case TouchPhase.Began:
							fingerStatus = "TouchPhase.Began";
						break;

						case TouchPhase.Ended:
							fingerStatus = "TouchPhase.Ended";
						break;

						case TouchPhase.Canceled:
							fingerStatus = "TouchPhase.Canceled";
						break;

						case TouchPhase.Stationary:
							fingerStatus = "TouchPhase.Stationary";
						break;

						case TouchPhase.Moved:
							fingerStatus = "TouchPhase.Moved";
						break;

					}
				}

				LogText.text= "Touch Status : " + fingerStatus;   


				float vertical = Input.GetAxis("Vertical");
				float horizontal = Input.GetAxis("Horizontal");

				if (Input.GetAxis("Vertical") >= 0.5) {
						direction = "up";
				}else if (Input.GetAxis("Vertical") <= -0.5) {
						direction = "down";
				}

				if (Input.GetAxis("Horizontal") >= 0.5) {
						direction = "right";
				}else if (Input.GetAxis("Horizontal") <= -0.5) {
						direction = "left";
				}
						
				LogText2.text= "Vertical : " + vertical + "\nHorizontal : " + horizontal +"\nDirection : " +direction;	

				LogText3.text= "Touch X : " + Input.touches[0].position.x + "\nTouch Y : " + Input.touches[0].position.y;

	        }		
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
                        LogText4.text = ("SWIPE = MOVE RIGHT");

                    }else{
                        // MOVE LEFT
                        LogText4.text =  ("SWIPE = MOVE LEFT");
                        
                    }
                    }

                    if(swipeType.y != 0.0f ){
                    if(swipeType.y > 0.0f){
                        // MOVE UP
                        LogText4.text =  ("SWIPE = MOVE UP");
                        
                    }else{
                        // MOVE DOWN
                        LogText4.text = ("SWIPE = MOVE DOWN");
                        
                    }
                    }

                }
                    
                break;
                }
            }
    	}

	}
}
