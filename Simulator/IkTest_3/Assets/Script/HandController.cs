using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

	public bool reach = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag.Equals ("TARGET")) {
			this.reach = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag.Equals ("TARGET")) {
			this.reach = false;
		}
	}
}
