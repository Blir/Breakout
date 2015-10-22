using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	static System.Random rand = new System.Random();

	string[] powerTypes = {"Lengthen", "Lengthen", "Lengthen", "Extra Life", "Extra Life", "Clone Ball", "Clone Ball", "Clone Ball", "Annihilator"};
	string powerName;
	public int seconds;
	BallController ball;

	// Use this for initialization
	void Start () {
		powerName = powerTypes[rand.Next(powerTypes.Length)];
		Animator anim = GetComponent<Animator>();
		switch (powerName) {
			case "Lengthen":
				anim.SetInteger ("PowerUpNum", 0);
				break;
			case "Clone Ball":
				anim.SetInteger ("PowerUpNum", 1);
				break;
			case "Annihilator":
				anim.SetInteger ("PowerUpNum", 2);
				break;
			case "Extra Life":
				anim.SetInteger ("PowerUpNum", 3);
				break;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -1.5f);
		// GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -1.5f), ForceMode2D.Force);
		Debug.Log ("powerup started");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name.Equals ("BottomBorder")) {
			Destroy (gameObject);	
			return;
		}
		if (coll.gameObject.tag.Equals ("Player")) {
			switch(powerName){
				case "Lengthen": 
					PaddleController paddle = coll.gameObject.GetComponent<PaddleController>();
					paddle.StartCoroutine (paddle.LengthPowerUp (seconds));
					break;
				case "Clone Ball": 
					ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallController>();
					if(ball != null){
						ball.CloneBall ();
					}
					break;
				case "Annihilator": 
					ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallController>();
					if(ball != null){
						ball.StartCoroutine (ball.Annihilator (seconds));
					}
					break;
				case "Extra Life":
					GameObject.FindGameObjectWithTag ("Lives").GetComponent<LifeTracker>().GainLife ();
					break;
			}
			Destroy(gameObject);
		}
	}
}
