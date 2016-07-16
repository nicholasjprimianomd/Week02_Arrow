using UnityEngine;
using System.Collections;

public class PlayerPhysicsMove : MonoBehaviour {
	public float playerSpeed;

	// Update is called once per physics frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(0f, 0f);

		if(Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)){
			GetComponent<Rigidbody2D> ().velocity += new Vector2(0f,playerSpeed) * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)){
			GetComponent<Rigidbody2D> ().velocity += new Vector2(0f,-playerSpeed) * Time.deltaTime;	
		}
		if(Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)){
			GetComponent<Rigidbody2D> ().velocity += new Vector2(-playerSpeed,0f) * Time.deltaTime;	
		}
		if(Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)){
			GetComponent<Rigidbody2D> ().velocity += new Vector2(playerSpeed,0f) * Time.deltaTime;	
		}
	}
}
