using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {
	int cols = 4;
	int rows = 4;
	int totalCards = 16;
	int matchesNeededToWin = 8;
	int matchesMade = 0;
	int cardW = 100;
	List<Card> aCard;
	Card[,] aGrid;
	ArrayList aCardsFlipped;
   	bool playerCanClick;
   	bool playerHasWon = false;
	
	// Use this for initialization
	void Start () {
        playerCanClick = true;

		aCard = new List<Card>();
		aGrid = new Card[4, 4];
        aCardsFlipped = new ArrayList();
		
		BuildDeck();
		
        for (int i=0; i<rows; i++)
        {
            for(int j=0; j<cols; j++)
            {

				int someNum = Random.Range(0, aCard.Count);
				aGrid[i, j] = aCard[someNum];
				aCard.RemoveAt(someNum);
            }
        }
	}
	
	// Update is called once per frame
	void OnGUI ()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        BuildGrid();

        if ( playerHasWon )
            BuildWinPrompt();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
	}
	
	IEnumerator FlipCardFaceUp(Card card)
    {
        card.isFaceUp = true;

        if ( aCardsFlipped.IndexOf(card) < 0 )
        {
            aCardsFlipped.Add(card);

            if ( aCardsFlipped.Count == 2 )
            {
                playerCanClick = false;
                yield return new WaitForSeconds(1);
				
				Card firstCard = (Card)aCardsFlipped[0];
				Card secondCard = (Card)aCardsFlipped[1];
                if (firstCard.id == secondCard.id)
                {
                    // Matched!
                    firstCard.isMatched = true;
                    secondCard.isMatched = true;

                    matchesMade ++;

                    if ( matchesMade >= matchesNeededToWin )
                    {
                        playerHasWon = true;
                    }
                }
                else
                {
                    firstCard.isFaceUp = false;
                    secondCard.isFaceUp = false;
                }
                aCardsFlipped = new ArrayList();
                playerCanClick = true;
            }
        }
    }

    void BuildWinPrompt()
    {
        int winPromptW = 100;
        int winPromptH = 90;

        float halfScreenW = Screen.width/2;
        float halfScreenH = Screen.height/2;

        int halfPromptW = winPromptW/2;
        int halfPromptH = winPromptH/2;
		
		GUI.BeginGroup(new Rect(halfScreenW-halfPromptW, halfScreenH-halfPromptH, winPromptW, winPromptH));
			
        GUI.Box(new Rect(0, 0, winPromptW, winPromptH), "A Winnner is You!");

        if(GUI.Button(new Rect(10, 40, 80, 20), "Player Again" ))
        {
        	Application.LoadLevel("title");
        }
        GUI.EndGroup();
    }

    void BuildGrid()
    {
        GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

        for ( int i=0; i<rows; i++ )
        {
            GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
			Card card = new Card();

            for ( int j=0; j<cols; j++ )
            {
                card = aGrid[i,j];
                string img;

                if ( card.isMatched)
                {
                    img = "blank";
                }
                else
                {
                    if (card.isFaceUp)
                    {
                        img = card.img;
                    }
                    else
                    {
                        img = "wrench";
                    }

                }

                GUI.enabled = !card.isMatched;

                if(GUILayout.Button((Texture)Resources.Load(img), 
                                    GUILayout.Width(cardW)))
                {
                    if (playerCanClick)
                    {
                        StartCoroutine(FlipCardFaceUp(card));
                    }
                    //Debug.Log(card.img);
                    GUI.enabled = true;
                }
            }

			GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
		GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }
	
	void BuildDeck()
	{
		int totalRobots = 4;
		Card card;
        int id = 0;
		
		for ( int i=0; i<totalRobots; i++ )
		{
			List<string> aRobotParts = new List<string>{"Head","Arm","Leg"};

			for ( int j=0; j<2; j++ )
			{
				int someNum = Random.Range (0, aRobotParts.Count);
				string theMissingPart = aRobotParts[someNum];
				
				aRobotParts.RemoveAt(someNum);
				
				card = new Card("robot" + (i+1) + "Missing" + theMissingPart, id);
				aCard.Add(card);
				
				card = new Card("robot" + (i+1) + theMissingPart, id);
				aCard.Add(card);
                id++;
			}
		}
	}
}
