using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public void OnRestart () {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnQuit () {
		Application.Quit ();
	}
}
