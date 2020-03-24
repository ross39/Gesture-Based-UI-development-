using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text myText;
	private static int highScore = 0;

	void Start(){
		myText = GetComponent<Text>();
		Reset();
	}
			
	public void Score(int points){
		Debug.Log ("Scored points");
		score += points;
		if (score > highScore)
		{
			myText.text = highScore.ToString();
			PlayerPrefs.SetInt("highscore", highScore);
		}
		myText.text = score.ToString();
	}
	
	public static void Reset(){
		score = 0;
	}

    //need to save score in playerprefs 

}
