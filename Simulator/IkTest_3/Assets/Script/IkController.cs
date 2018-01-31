using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dimension {
	x = 0,
	y,
	z
}

public class IkController : MonoBehaviour {

	public RunMode runMode;

	public float limitAngle = 90f;
	public bool limitAngleEnable = true;
	public int threshold = 10;

	public HandController handController;

	private int attCounter = 0;
	private bool reach = false;

	public enum RunMode {
		perJoint = 0,
		perArm,
		normal
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RunIk (List<GameObject> att, GameObject hand, GameObject target) {

//		if (this.handController.reach)
//			return;
		
		if (this.runMode.Equals (RunMode.perArm)) {
			foreach (GameObject eachArm in att)
				this.UpdateAngle (eachArm, hand, target, eachArm.GetComponent<ControlDimensionIdentifier> ());
		} else if (this.runMode.Equals (RunMode.perJoint)) {
			if (this.attCounter >= att.Count)
				attCounter = 0;
			this.UpdateAngle (att [this.attCounter], hand, target, att [this.attCounter].GetComponent<ControlDimensionIdentifier> ());
			this.attCounter++;
		} else {
			for (int i = 0; i < threshold; i++) {
//				if (this.handController.reach)
//					break;
				foreach (GameObject eachArm in att)
					this.UpdateAngle (eachArm, hand, target, eachArm.GetComponent<ControlDimensionIdentifier> ());
			}
		}

	}

	private void UpdateAngle (GameObject joint, GameObject hand, GameObject target, ControlDimensionIdentifier cdi) {
		Vector3 jointVec = joint.transform.position;
		Vector3 handVec = hand.transform.position;
		Vector3 targetVec = target.transform.position;
		Dimension organizeDimension = cdi.controlDimension;

		handVec [(int)organizeDimension] = jointVec [(int)organizeDimension];
		targetVec [(int)organizeDimension] = jointVec [(int)organizeDimension];

		float angle = this.GetNewAngle (jointVec, handVec, targetVec, organizeDimension);
		Vector3 newRotateEulerRotation = Vector3.zero;

		newRotateEulerRotation [(int)organizeDimension] = angle;

		if (cdi.limitAngleEnable) {
			this.Rotate (joint, newRotateEulerRotation, cdi.limitAngle);
		} else {
			joint.transform.Rotate (newRotateEulerRotation);
		}
	}

	private float GetNewAngle(Vector3 joint, Vector3 hand, Vector3 target, Dimension organizeDimension) {
		Vector3 pointToHand = hand - joint;
		Vector3 pointToTarget = target - joint;
		Vector3 cross = Vector3.Cross (pointToHand, pointToTarget);

		float result = Vector3.Angle (pointToHand, pointToTarget);

		if (cross [(int)organizeDimension] < 0)
			result *= -1;

		return result;
	}

	private void Rotate (GameObject gameObj, Vector3 rotation, float limitAngle) {
		gameObj.transform.localEulerAngles = this.RotateVector (gameObj.transform.localEulerAngles, rotation, limitAngle);
	}

	/// <summary>
	/// Rotates the vector. (この処理めっちゃ重い)
	/// </summary>
	/// <returns>The vector.</returns>
	/// <param name="vec1">Vec1.</param>
	/// <param name="vec2">Vec2.</param>
	private Vector3 RotateVector (Vector3 vec1, Vector3 vec2, float limitAngle) {

		for (int i = 0; i < 3; i++) {
			vec1 [i] = vec1 [i] > 180f ? vec1 [i] - 360f : vec1 [i];
		}

		Vector3 result = new Vector3 ();
		for (int i = 0; i < 3; i++) {
			float eachAngle = vec1 [i] + vec2 [i];
//			Debug.Log ("before calib: " + eachAngle);
			result [i] = 
				eachAngle > limitAngle ? limitAngle : 
				eachAngle < -1*limitAngle ? -1*limitAngle : eachAngle;

//			Debug.Log ("after calib: " + result [i]);
		}

		for (int i = 0; i < 3; i++) {
			result [i] = result [i] < 0f ? 360f + result [i] : result [i] >= 360f ? 360f - result [i] : result [i];
		}

//		Debug.Log ("original: " + vec1 + ", vector: " + vec2 + ", result: " + result);

		return result;
	}


}
