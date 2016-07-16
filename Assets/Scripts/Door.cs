using UnityEngine;
using System.Collections;
using UnityEngineInternal;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Door : MonoBehaviour {
	public Transform player;
	public Vector3 moveDist; //distance door moves
	private Vector3 initialLocation;  //reset location of the door
	private bool canMove = true;
	//public Color newColor; <- Old version
	private Color newColor; 
	private Vector3 oldPosition; // door position tracking
	private Vector3 newPosition; // door position tracking
	public Treasure treasure; 
	public bool isLocked = false; //is the door locked?
	public bool lockTextShowing = false; //is the door is locked text showing?

	void Start () {
		initialLocation = transform.position;
	}
		
	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();

		//Check position of door to play sound only
		//if the door has moved in the last frame
		oldPosition  = transform.position;
		moveDoor();
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

	//Move door
	private void moveDoor() {
		//Check if door was never locked or has been unlocked
		if (treasure.isOpen  || !isLocked) {
			lockTextShowing = false;
			//Open on player proximity
			if ((player.transform.position - transform.position).magnitude < 5.00f && canMove) {
				transform.position = transform.position + moveDist;
				Vector3 targetPosition = initialLocation + moveDist;
				//transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 2);

				canMove = false;
				//Close on player proximity
			} else if ((player.transform.position - transform.position).magnitude >= 10.00f) {
				//transform.position = Vector3.Lerp (transform.position, initialLocation, Time.deltaTime * 3);
				transform.position = initialLocation;
	
				canMove = true;
			}
			//The player is close and the door is locked (this is awful logic)
		} else if (((player.transform.position - transform.position).magnitude < 5.00f) && isLocked && !treasure.isOpen){
			treasure.dirText.text = "The door is locked. We'll need Felicity for this!";
			lockTextShowing = true;
		}else{
			lockTextShowing = false;
		}
	}


	//WaitForSecondsTutorial - Look into this
	IEnumerator move (){
		yield return new WaitForSeconds (1f);
		transform.position = transform.position + moveDist;	
		//yield return new WaitForSeconds (.5f);
	}

	IEnumerator moveBack (){
		yield return new WaitForSeconds (1f);
		transform.position = initialLocation;	
		//yield return new WaitForSeconds (.5f);
	}


	//Play door open sound
	private void playSound(AudioSource audio){
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
