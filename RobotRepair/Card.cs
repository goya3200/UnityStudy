using UnityEngine;
using System.Collections;

public class Card : System.Object {
	public string img;
	public bool isFaceUp = false;
	public bool isMatched = false;
    public int id;
	
	public Card() {
		img = "robot";
	}
	
	public Card(string img, int id) {
		this.img = img;	
        this.id = id;
	}

}
