using UnityEngine;
using System.Collections;
using Thalmic.Myo;


public class LevelManager : MonoBehaviour {

	public GameObject myo = null;
	

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
