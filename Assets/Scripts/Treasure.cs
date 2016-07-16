using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;
using System;

public class Treasure : MonoBehaviour {

	//Treasure located at (playerSpeed,-playerSpeed)
	bool didPlayerWin = false; //Win State
	public Text dirText; //Direction Text
	public Transform player; //Player Location
	public Transform felicity; //Felicity Location
	public Transform diggle; //Diggle Location
	public Transform barry;  //Barry Location
	public Transform thea;  //Thea Location
	public bool isOpen = false;
	public Door door1;
	public Door door2;
	public PlayerPhysicsMove playerMove;
	public float increasedPlayerSpeed;
	public TrailRenderer trailRendererGreen;
	public TrailRenderer trailRendererRed;

	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();

	if ((player.position - felicity.transform.position).magnitude < 3f) {
			dirText.text = "Felicity: I hacked the locked (red) doors. Back track and find Dahrk!";
			isOpen = true;
		}
		//Player close to Diggle
		else if ((player.position - diggle.transform.position).magnitude < 3f) {

			dirText.text = "Diggle: Come on Oliver, man, you almost have him.";
		}
		//Player close to Thea
		else if ((player.position - thea.transform.position).magnitude < 3f) {
			dirText.text = "Thea: Oliver, turn around! This area is clear.";

		}
		//Player close to Barry
		else if ((player.position - barry.transform.position).magnitude < 3f) {
			dirText.text = "Barry: The speed force just gave you a boost!";
			//Double player speed on proximity to Barry;
			playerMove.playerSpeed = increasedPlayerSpeed;
			trailRendererGreen.enabled = false;
			trailRendererRed.enabled = true;
			//attach red trail renderer to player
			//why is unity terrible there should be a class for this
			trailRendererRed.transform.parent = player.transform;
			trailRendererRed.transform.position = player.transform.position;
		}
		//Player close 
		else if ((player.position - transform.position).magnitude < 9f) {
			dirText.text = "You're so close!";
			//Player win zone
			if ((player.position - transform.position).magnitude < 2f) {
				dirText.text = "Press [Enter] to catch Damien Darhk! ";
				if (Input.GetKeyDown (KeyCode.Return)) {
					audio.Play ();
					didPlayerWin = true;
				} else if (didPlayerWin) {
					SceneManager.LoadScene ("Win Screen");
				}
			}
		} else {
			//Is lock text showing
			if (!door1.lockTextShowing && !door2.lockTextShowing) {
				dirText.text = "Find Damien Darhk!";
			}
		}
		if (Input.GetKey (KeyCode.Space)) {  //Hold space to cheat
			dirText.text += " " + (player.position - transform.position).ToString ();
		}
	}
}
