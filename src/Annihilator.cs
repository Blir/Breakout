﻿using UnityEngine;
using System.Collections;

public class Annihilator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("AnnihilatorScript.OnTriggerEnter2D");
		if(coll.gameObject.tag.Equals("Brick")){
			coll.gameObject.GetComponent<BrickController>().DestroyBrick ();
		}
	}
}