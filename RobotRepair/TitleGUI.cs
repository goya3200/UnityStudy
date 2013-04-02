using UnityEngine;
using System.Collections;

public class TitleGUI : MonoBehaviour {
	
	public GUISkin customSkin;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
//		GUI.skin = customSkin;
//		if (GUI.Button(new Rect(0, 0, 100, 50), "Play Game")) {
//			prApplint ("You Clicked me!");
//		}

	 	int buttonW = 100;
		int buttonH = 50;

		float halfScreenW = Screen.width/2;

		float halfButtonW = buttonW/2;
		GUI.skin = customSkin;
		if (GUI.Button(new Rect(halfScreenW - halfButtonW, 160, buttonW, buttonH), "Play Game")) {
			//print ("You Clicked me!");
			Application.LoadLevel("game");
		}
	}
}
