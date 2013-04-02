using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour {
	
	float smooth = 5.0F;
	float tiltAngle = 30.0F;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		transform.position = new Vector3(2, 0, 0);
//		transform.position = new Vector3(transform.position.x + 0.2f, 0, 0);
//		transform.position = new Vector3(Input.mousePosition.x, 0, 0);
//		Debug.Log(Input.mousePosition.x);
//		Debug.Log (Input.mousePosition.x - Screen.width/2);
//		Debug.Log ((Input.mousePosition.x - Screen.width/2) / (Screen.width/2));
		
//		float halfW = Screen.width / 2;
//		float newPositionX = (Input.mousePosition.x - halfW) / halfW;
//		transform.position = new Vector3(newPositionX, 0, 0);
		
		float halfW = Screen.width/2;
		float halfH = Screen.height/2;
		transform.position = new Vector3((Input.mousePosition.x - halfW)/halfW, 0, (Input.mousePosition.y - halfH)/halfH);
		
		float tiltAroundZ = Input.GetAxis("Mouse X") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Mouse Y") * tiltAngle * -2;
		
		Debug.Log (Input.GetAxis("Mouse X"));
		
		Quaternion target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
		
	}
}
