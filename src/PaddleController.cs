using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	public GameObject ball;
	public GameObject ballSpawn;
	public UnityEngine.UI.Text lives;
	public bool isLengthened = false;
	bool isSpawning;
	bool g, a, b, e, n;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnBall());
	}
	
	// Update is called once per frame
	void Update () {
		GabeN ();
		if (NoBricks () && !lives.GetComponent<LifeTracker>().isGameOver) {
			if (Application.loadedLevel + 1 == Application.levelCount) {
				Application.LoadLevel (0);
			} else {
				Application.LoadLevel (Application.loadedLevel + 1);
			}
		}
		if (NoBalls () && !isSpawning){
			lastBallCleanup ();
		}
	}

	// developer only - do not enter!
	void GabeN() {
		if (!g) {
			if (Input.GetKeyDown ("g")) {
				g = true;
				Debug.Log ("G");
			}
		} else if (!a) {
			if (Input.GetKeyDown ("a")) {
				a = true;
				Debug.Log ("a");
			}
		} else if (!b) {
			if (Input.GetKeyDown ("b")) {
				b = true;
				Debug.Log ("b");
			}
		} else if (!e) {
			if (Input.GetKeyDown ("e")) {
				e = true;
				Debug.Log ("e");
			}
		} else if (!n) {
			if (Input.GetKeyDown ("n")) {
				n = true;
				Debug.Log ("N");
			}
		} else {
			Object[] balls = GameObject.FindGameObjectsWithTag ("Ball");
			if (Input.GetKeyDown ("c")) {
				foreach (Object ball in balls) {
					((GameObject) ball).GetComponent<BallController>().CloneBall ();
				}
			}
			if (Input.GetKeyDown ("a")) {
				foreach (Object ball in balls) {
					BallController script = ((GameObject) ball).GetComponent<BallController>();
					script.StartCoroutine (script.Annihilator(10));
				}
			}
			if (Input.GetKeyDown ("l")) {
				StartCoroutine (LengthPowerUp (10));
			}
			if (Input.GetKeyDown ("h")) {
				lives.GetComponent<LifeTracker>().GainLife ();
			}
		}
	}

	public IEnumerator LengthPowerUp(int seconds) {
		transform.localScale = new Vector3(3, 1, 1);
		isLengthened = true;
		yield return new WaitForSeconds(seconds);
		transform.localScale = new Vector3(1.5f,1,1);
		isLengthened = false;
	}
	
	public IEnumerator SpawnBall() {
		ballSpawn.GetComponent<Renderer>().enabled = true;
		isSpawning = true;
		while(!Input.GetKeyDown("space")){
				yield return null;
		}
		Instantiate(ball, new Vector3 (transform.position.x, transform.position.y + 0.25f, 0), Quaternion.identity);
		ballSpawn.GetComponent<Renderer>().enabled = false;
		isSpawning = false;
	}
	
	void lastBallCleanup () {
		StartCoroutine(SpawnBall());
		GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
		foreach (GameObject powerUp in powerUps){
			Destroy (powerUp);
		}
		GameObject.FindGameObjectWithTag ("Lives").GetComponent<LifeTracker>().LoseLife();
	}
	
	bool NoBalls () {
		return GameObject.FindGameObjectsWithTag ("Ball").Length == 0;
	}
	
	bool NoBricks(){
		return GameObject.FindGameObjectsWithTag ("Brick").Length == 0;
	}

}
