﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickEndPoint : AClickable {

	public GameObject jointPrefab;

	public override void OnClick() {
		Instantiate( jointPrefab, transform.position, transform.rotation );
		ConnectionJoint connectionJoint = jointPrefab.GetComponent<ConnectionJoint>();
		connectionJoint.connectionType = ConnectionType.Fixed;
		connectionJoint.attachedToStick = myRigidBody;
	}

}
