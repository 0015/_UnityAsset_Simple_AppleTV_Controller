using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

	public Transform target;
	// The distance in the x-z plane to the target
	public float distance= 10.0f;
	// the height we want the camera to be above the target
	public float height= 5.0f;
	// How much we 
	public float heightDamping= 2.0f;
	public float rotationDamping= 3.0f;

	void  LateUpdate (){
		// Early out if we don't have a target
		if (!target)
			return;
		
		 float wantedRotationAngle = target.eulerAngles.y;
             float wantedHeight = target.position.y + height;
                 
             float currentRotationAngle = transform.eulerAngles.y;
             float currentHeight = transform.position.y;
             
             // Damp the rotation around the y-axis
             currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
         
             // Damp the height
             currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
         
             // Convert the angle into a rotation
             Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
             
             // Set the position of the camera on the x-z plane to:
             // distance meters behind the target
             
             Vector3 pos = target.position;
             pos -= currentRotation * Vector3.forward * distance;
             pos.y = currentHeight;
             transform.position = pos;
             
             
             // Always look at the target
             transform.LookAt (target);
	}
}