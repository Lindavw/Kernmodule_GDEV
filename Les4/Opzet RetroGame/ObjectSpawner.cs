using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	public ObjectController prefab;

	public int prefabsCount = 1;

	//notify the UI
	public delegate void OnSpawn();
	public event OnSpawn onSpawn;

	void Start(){
		for (int i = 0; i < prefabsCount; i++) {
			SpawnObject ();
		}
	}

	public void SpawnObject(){
		ObjectController newObject;
		newObject = (ObjectController)Instantiate(prefab, transform.position, transform.rotation);
		newObject.onDeath += onObjectDeath;

		//check to see if anyone is subscribed to the event
		if (onSpawn != null) {
			onSpawn ();
		}
	}

	public void onObjectDeath(ObjectController objectController){
		//unsubscribe to avoid memoryleaks and null references 
		objectController.onDeath -= onObjectDeath;

		SpawnObject ();

	}
}
