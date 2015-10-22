using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GameObject leftBorder, rightBorder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float motion = Input.GetAxis ("Horizontal") * speed;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (motion, 0);
		float leftX = leftBorder.transform.position.x + 1;
		float rightX = rightBorder.transform.position.x - 1;
		if (GetComponentInChildren<PaddleController>().isLengthened) {
			leftX++;
			rightX--;
		}
		float x3 = gameObject.transform.position.x;
		if (x3 < leftX) {
			x3 = leftX;
		} else if (x3 > rightX) {
			x3 = rightX;
		}
		gameObject.transform.position = new Vector3 (x3, gameObject.transform.position.y, 0);
	}
}
