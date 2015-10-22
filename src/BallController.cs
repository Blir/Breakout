using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	
	static System.Random rand = new System.Random();

	public float MAX_SPEED, MIN_SPEED;
	public float nudgeDelay;
	float[] possibleSpeeds = {-3f, -2.5f, -2f, -1.5f, 1.5f, 2f, 2.5f, 3f};
	bool canNudge = true;

	public Sprite annihilatorBall, normalBall; // to switch sprites for the annihilator power up
	public GameObject childAnnihilator; // annihilator child object for annihilator power up

	public GameObject ball; // ball prefab for clone power up
	public AudioClip brickTone, paddleTone;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector3(possibleSpeeds[rand.Next(possibleSpeeds.Length)], rand.Next (2, 4), 0);
		if (IsAnnihilator ()) {
			BecomeRegularBall ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckVelocity ();
		CheckNudge ();
	}

	void CheckVelocity () {
		
		Vector2 currVel = GetComponent<Rigidbody2D>().velocity;
		float x = currVel.x;
		float y = currVel.y;
		
		if (x > MAX_SPEED) {
			x = MAX_SPEED;
		} else if (x < -MAX_SPEED) {
			x = -MAX_SPEED;
		}

		if (x > 0 && x < MIN_SPEED) {
			x = MIN_SPEED;
		} else if (x <= 0 && x > -MIN_SPEED) {
			x = -MIN_SPEED;
		}
		
		if (y > MAX_SPEED) {
			y = MAX_SPEED;
		} else if (y < -MAX_SPEED) {
			y = -MAX_SPEED;
		}
		
		if (y > 0 && y < MIN_SPEED) {
			y = MIN_SPEED;
		} else if (y <= 0 && y > -MIN_SPEED) {
			y = -MIN_SPEED;
		}
		
		GetComponent<Rigidbody2D>().velocity = new Vector2 (x, y);
	}

	void CheckNudge () {
		if (canNudge && Input.GetKeyDown ("space")) {
			float paddleY = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
			if (paddleY < transform.position.y) {
				StartCoroutine (Nudge ());
			}
		}
	}

	IEnumerator Nudge () {
		canNudge = false;
		int xNudge = rand.Next (-2, 3);
		int yNudge = rand.Next (-2, 3);
		Vector2 currVel = GetComponent<Rigidbody2D>().velocity;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (currVel.x + xNudge, currVel.y + yNudge);
		yield return new WaitForSeconds (nudgeDelay);
		canNudge = true;
	}
	
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.name.Equals("BottomBorder")){
			Destroy (gameObject);	
		}
		if (coll.gameObject.tag.Equals ("Brick")) {
			GetComponent<AudioSource>().PlayOneShot (brickTone);
		} else {
			GetComponent<AudioSource>().PlayOneShot (paddleTone);
		}
	}
	
	public void CloneBall () {
		Instantiate (ball, transform.position, Quaternion.identity);
	}
	
	public IEnumerator Annihilator (int seconds) {
		GetComponent<SpriteRenderer>().sprite = annihilatorBall;
		childAnnihilator.GetComponent<CircleCollider2D>().enabled = true;
		//GetComponentInChildren<CircleCollider2D>().enabled = true;
		gameObject.layer = 9;
		yield return new WaitForSeconds (seconds);
		BecomeRegularBall ();
	}

	void BecomeRegularBall () {
		GetComponent<SpriteRenderer>().sprite = normalBall;
		//GetComponentInChildren<CircleCollider2D>().enabled = false;
		childAnnihilator.GetComponent<CircleCollider2D>().enabled = false;
		gameObject.layer = 10;
	}

	bool IsAnnihilator () {
		return GetComponent<SpriteRenderer>().sprite == annihilatorBall;
	}
}
