using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	private int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<UnityEngine.UI.Text>().text = System.String.Format ("Score: {0}", score);
	}

	public void incrementScore(int amt) {
		score += amt;
	}
}
