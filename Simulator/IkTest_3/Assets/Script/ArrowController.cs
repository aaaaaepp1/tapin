using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	public bool inTheArea;
	public GameObject defaultPositionDefiner;
	public GameObject arrow;
	public GameObject basePointDefiner;
	public GameObject environment;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (this.inTheArea && OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
			this.arrow.transform.position = this.transform.position;
			this.arrow.transform.rotation = this.transform.rotation;
			this.arrow.transform.Rotate (-90f, 0f, 0f);
		}

		this.basePointDefiner.transform.eulerAngles = Vector3.zero;

		if (OVRInput.GetDown (OVRInput.RawButton.A)) {
			this.UpdateEnvironmentPos (basePointDefiner.transform.position);
		}

		basePointDefiner.SetActive ((OVRInput.Get (OVRInput.RawButton.B)));
	}

	void OnTriggerStay (Collider other) {
		if (other.tag.Equals ("ARROW_AREA")) {
			this.inTheArea = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag.Equals ("ARROW_AREA") && OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
			this.inTheArea = false;
			arrow.transform.position = Vector3.Lerp (this.transform.position, this.defaultPositionDefiner.transform.position, 0.2f);
		}
	}

	private void UpdateEnvironmentPos (Vector3 pos) {
		this.environment.transform.position = pos;
		this.arrow.transform.position = this.defaultPositionDefiner.transform.position;
	}
}
