using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class Door : MonoBehaviour {
	public Transform player;
	public Vector3 moveDist;
	private Vector3 initialLocation;
	private bool canMove = true;
	public Color newColor; 
	private Vector3 oldPosition;
	private Vector3 newPosition;


	void Start () {
		initialLocation = transform.position;
	}
		
	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();


		oldPosition  = transform.position;
		move ();
		newPosition = transform.position;

		if (newPosition != oldPosition) {
			playSound (audio);
			//StartCoroutine (Coroutine ());	
		}

		changeColor (newColor);
	}

	//WaitForSeconds Tutorial
	//private IEnumerator Coroutine(){
	//	playSound (audio);
	//	yield return WaitForSeconds (1);	
	//}


	private void changeColor(Color col){
		SpriteRenderer wall = GetComponent<SpriteRenderer> ();
		wall.color = col;
	}

	private void move() {
			if((player.transform.position - transform.position).magnitude < 4 && canMove){
				transform.position = transform.position + moveDist;
				canMove = false;
			}
			else if ((player.transform.position - transform.position).magnitude > 5){
				transform.position = initialLocation;
				canMove = true;
			}
	}

	private void playSound(AudioSource audio){
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
