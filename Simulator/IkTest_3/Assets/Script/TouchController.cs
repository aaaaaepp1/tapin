using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	public OVRInput.Controller controllerType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = OVRInput.GetLocalControllerPosition (this.controllerType);
		this.transform.rotation = OVRInput.GetLocalControllerRotation (this.controllerType);
	}
}
