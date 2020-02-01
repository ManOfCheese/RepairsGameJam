using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickEndPoint : AClickable {

	public GameObject jointPrefab;

	private float jointOffset = 1f;

	public override void OnClick() {
		Instantiate( jointPrefab, transform.position + ( transform.position - transform.parent.transform.position ).normalized * jointOffset , transform.rotation );
		ConnectionJoint connectionJoint = jointPrefab.GetComponent<ConnectionJoint>();
		connectionJoint.connectionType = ConnectionType.Fixed;
		connectionJoint.attachedToStick = myRigidBody;
	}

}
