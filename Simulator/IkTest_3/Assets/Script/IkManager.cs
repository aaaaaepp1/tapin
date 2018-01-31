using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkManager : MonoBehaviour {

	public IkController ikController;
	public SerialHandler serialHandler;
	public bool updateTapinAngle = true;

	public List<GameObject> arms;
	public List<GameObject> hands;
	public GameObject target;
	public GameObject fingertip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.A))
			this.UpdateIk ();

		if (this.updateTapinAngle) {
			this.serialHandler.SetAngles (
				(int)this.getAngle (arms [2].transform.localEulerAngles [1]),
				(int)this.getAngle (arms [1].transform.localEulerAngles [0]),
				(int)this.getAngle (arms [0].transform.localEulerAngles [0]),
				(int)this.getAngle (hands [1].transform.localEulerAngles [1]),
				(int)this.getAngle (hands [0].transform.localEulerAngles [0])+45);
		}

		Debug.Log ((int)this.getAngle (hands [0].transform.localEulerAngles [0])+45);
	}

	private void UpdateIk () {
		GameObject armTarget = this.GetArmTarget (this.target);
		if (armTarget == null) {
			Debug.LogError ("arm target gameobject is not exist");
			return;
		}

		this.ikController.RunIk (this.arms, this.hands [this.hands.Count - 1], armTarget);
		this.ikController.RunIk (this.hands, this.fingertip, this.target);
	}

	private GameObject GetArmTarget (GameObject target) {

		foreach (Transform eachTransform in target.transform) {
			if (eachTransform.name.Equals ("armTarget")) {
				return eachTransform.gameObject;
			}
		}

		return null;
	}

	private float getAngle (float angle) {
		return (angle > 180 ? angle - 360 : angle)+90;
	}
}
