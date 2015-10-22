using UnityEngine;
using System.Collections;

public class LifeTracker : MonoBehaviour {

	public UnityEngine.UI.Image life1, life2, life3, life4, life5;
	public UnityEngine.UI.Text gameOver;
	public UnityEngine.UI.Button restart, quit;

	public bool isGameOver = false;

	public void SetGameOver(bool val) {
		isGameOver = val;
		gameOver.enabled = val;
		restart.enabled = val;
		restart.image.enabled = val;
		restart.GetComponentInChildren<UnityEngine.UI.Text>().enabled = val;
		quit.enabled = val;
		quit.image.enabled = val;
		quit.GetComponentInChildren<UnityEngine.UI.Text>().enabled = val;
	}

	// Use this for initialization
	void Start () {
		SetGameOver (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GainLife() {
		if (!life2.enabled) {
			life2.enabled = true;
		} else if (!life3.enabled) {
			life3.enabled = true;
		} else if (!life4.enabled) {
			life4.enabled = true;
		} else if (!life5.enabled) {
			life5.enabled = true;
		}
	}
	
	public void LoseLife () {
		if (life5.enabled) {
			life5.enabled = false;
		} else if (life4.enabled) {
			life4.enabled = false;
		} else if (life3.enabled) {
			life3.enabled = false;
		} else if (life2.enabled) {
			life2.enabled = false;
		} else if (life1.enabled) {
			life1.enabled = false;
			SetGameOver (true);
		}
	}
}
