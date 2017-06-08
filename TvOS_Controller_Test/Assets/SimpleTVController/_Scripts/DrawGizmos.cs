using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour {

	public Color gizmocolor;
	public float Radius;

	void  Update (){
		transform.rotation = Quaternion.identity;
	}

	void  OnDrawGizmos (){
		Gizmos.color = gizmocolor;
		Gizmos.DrawSphere (transform.position, Radius);
	}
	
}
