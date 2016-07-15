using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour {

	//Treasure located at (100,-100)
	bool didPlayerWin = false; //Win State
	public Text dirText; //Direction Text
	public Transform player; //Player Location
	public Transform felicity; //Felicity Location
	public Transform diggle; //Diggle Location
	public Transform barry;  //Barry Location
	public Transform thea;  //Thea Location

	void Update () {
		if (player.position.x < -100f) {
			//Move Right
			dirText.text = "You are very far away. Go back to the where you started.";
		} else if (player.position.y > 100f) {
			//Move Down	
			dirText.text = "You are very far away. Go back to the where you started.";
		} else if (player.position.y < -200f) {
			//Move Up
			dirText.text = "You are very far away. Go back to the where you started.";
		} else if (player.position.x > 200f) {
			//Move Left
			dirText.text = "You are very far away. Go back to the where you started.";
		}
		//Player close to Felicity
		else if ((player.position - felicity.transform.position).magnitude < 10f) {
			dirText.text = "Hey oliver its Felicty.";
		}
		//Player close to Diggle
		else if ((player.position - diggle.transform.position).magnitude < 10f) {
			dirText.text = "Hey oliver its Diggle.";
		}
		//Player close to Thea
		else if ((player.position - thea.transform.position).magnitude < 10f) {
			dirText.text = "Hey oliver its Thea.";
		}
		//Player close to Barry
		else if ((player.position - barry.transform.position).magnitude < 10f) {
			dirText.text = "Hey oliver its Barry.";
		}
		//Player close 
		else if ((player.position - transform.position).magnitude < 20f) {
			dirText.text = "You are very close. The treasure is right around here.";
			//Player win zone
			if ((player.position - transform.position).magnitude < 5f) {
				dirText.text = "Press [Space] to catch Damien Darhk! ";
				if (Input.GetKeyDown (KeyCode.Space)) {
					AudioSource audio = GetComponent<AudioSource> ();
					audio.Play ();
					didPlayerWin = true;
				} else if (didPlayerWin) {
					SceneManager.LoadScene ("Win Screen");
				}
			}
		} else {
				dirText.text = "Find Damien Darhk!";
		}
		if (Input.GetKey (KeyCode.Space)) {
			dirText.text += " " + (player.position - transform.position).ToString ();
		}
	}
}
