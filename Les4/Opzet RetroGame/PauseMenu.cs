using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	void Update(){
		if (Input.GetButtonDown ("Cancel")) {
			GameManager.instance.UnPause ();
		}
	}
}
