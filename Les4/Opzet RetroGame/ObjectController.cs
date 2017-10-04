using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using delegate/ events so the UI elements won't ask for updates every frame -> efficiency
public class ObjectController : MonoBehaviour {

	public float minLifeTime = 4.0f;
	public float maxLifeTime = 8.0f;

	//physics variables vector2 here

	public delegate void OnDeath(ObjectController objectController);
	public event OnDeath onDeath;

	void Start(){
		Invoke("Died",Random.Range(minLifeTime,maxLifeTime));
		//rigidbody2d rb = Getcomponent <rigidbody2D>(); here
	}

	void Died(){
		if (onDeath != null) {
			onDeath (this);
			Destroy (gameObject);
		}
	}
}
