using UnityEngine;
using System.Collections;
using UnityEngineInternal;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Door : MonoBehaviour {
	public Transform player;
	public Vector3 moveDist;
	private Vector3 initialLocation;
	private bool canMove = true;
	//public Color newColor; <- Old version
	private Color newColor; 
	private Vector3 oldPosition; // door position tracking
	private Vector3 newPosition; // door position tracking
	public Treasure treasure; 
	public bool isLocked = false;
	public bool lockTextShowing = false;

	void Start () {
		initialLocation = transform.position;
	}
		
	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();

		//Check position of door to play sound only
		//if the door has moved in the last frame
		oldPosition  = transform.position;
		move ();
		newPosition = transform.position;

		//Play Sound
		if (newPosition != oldPosition) {
			playSound (audio);
		}
			
		//Locked or Unlocked Color
		if (treasure.isOpen  || !isLocked) {
			//door is green if open 
			newColor = new Color (0,1,0,1);
		}else{
			//door is red if closed
			newColor = new Color (1,0,0,1);	
		}

		//change door color to red or green
		changeColor (newColor);
	}

	//change door color
	private void changeColor(Color col){
		SpriteRenderer wall = GetComponent<SpriteRenderer> ();
		wall.color = col;
	}

	//Move dooor
	private void move() {
		//Check if door was never locked or has been unlocked
		if (treasure.isOpen  || !isLocked) {
			//Open on player proximity
			if ((player.transform.position - transform.position).magnitude < 5.00f && canMove) {
				transform.position = transform.position + moveDist;
				canMove = false;
				//Close on player proximity
			} else if ((player.transform.position - transform.position).magnitude >= 5.00f) {
				transform.position = initialLocation;
				canMove = true;
				//Too far for lock text to show
				lockTextShowing = false;
			}
			//The player is close and the door is locked (this is awful logic)
		} else if (((player.transform.position - transform.position).magnitude < 5.00f) && isLocked && !treasure.isOpen){
			treasure.dirText.text = "The door is locked. We'll need Felicity for this!";
			lockTextShowing = true;
		}
	}

	//Play door open sound
	private void playSound(AudioSource audio){
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
