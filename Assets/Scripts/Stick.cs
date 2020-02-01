using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

	private FixedJoint fixedJoint;
	private bool hasFixedJoint;

	public void AddFixedJoint() {
		fixedJoint = gameObject.AddComponent<FixedJoint>();
		hasFixedJoint = true;
	}

	public void ConnectRigidBody( Rigidbody rigidBody ) {
		if ( hasFixedJoint ) {
			fixedJoint.connectedBody = rigidBody;
		}
	}
    
}
