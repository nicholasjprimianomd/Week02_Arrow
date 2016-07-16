using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class Door : MonoBehaviour {
	public Transform player;
	public Vector3 moveDist;
	private Vector3 initialLocation;
	private bool canMove = true;
	//public Color newColor;
	private Color newColor; 
	private Vector3 oldPosition;
	private Vector3 newPosition;
	public Treasure treasure;
	public bool isLocked = false;


	void Start () {
		initialLocation = transform.position;
	}
		
	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();


		oldPosition  = transform.position;
		move ();
		newPosition = transform.position;


		//Play Sound
		if (newPosition != oldPosition) {
			playSound (audio);
		}


		//Locked or Unlocked Color
		if (treasure.isOpen  || !isLocked) {
			newColor = new Color (0,1,0,1);
		}else{
			newColor = new Color (1,0,0,1);	
		}
		changeColor (newColor);
	}

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
			}
			//The player is close and the door is locked (this is awful logic)
		} else if (((player.transform.position - transform.position).magnitude < 5.00f) && isLocked && !treasure.isOpen){
			treasure.dirText.text = "The door is locked. We'll need Felicity for this!";
			Debug.Log ("The door is locked. We'll need Felicity for this!");
		}
	}

	//Play door open sound
	private void playSound(AudioSource audio){
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
