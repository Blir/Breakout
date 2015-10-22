using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {

	public Sprite blue, green, red, yellow, purple, white;
	public int durability;
	public GameObject powerUp; // power up prefab for white bricks
	int numHits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D (Collision2D coll) {
		//Debug.Log ("BrickScript.OnCollisionEnter2D");
		numHits++;
		switch (durability - numHits) {
		case 4: 
			GetComponent<SpriteRenderer>().sprite = yellow; 
			break;
	 	case 3: 
	 		GetComponent<SpriteRenderer>().sprite = red; 
	 		break;
     	case 2: 
     		GetComponent<SpriteRenderer>().sprite = green; 
     		break;
	 	case 1: 
	 		GetComponent<SpriteRenderer>().sprite = blue; 
	 		break;
	 	case 0: 
			DestroyBrick ();
	 		break;
		}
	}

	bool IsPowerUpBrick () {
		return GetComponent<SpriteRenderer>().sprite == white;
	}

	public void DestroyBrick () {
		//Debug.Log ("BrickScript.DestroyBrick");
		GameObject.FindGameObjectWithTag ("Score").GetComponent<ScoreTracker>().incrementScore (durability * 10);
		Destroy (gameObject);
		if (IsPowerUpBrick ()) {
			Instantiate (powerUp, transform.position, Quaternion.identity);
		}
	}
}
