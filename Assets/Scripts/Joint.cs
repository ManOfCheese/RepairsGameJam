using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType {
	Fixed,
	Hinge
}

public class Joint : ADeletable {

	public GameObject fixedJointPrefab;
	public GameObject hingeJointPrefab;
	public GameObject endItemPrefab;

	public void ConnectRigidBody( Rigidbody rigidBody ) {
		GetComponent<HingeJoint>().connectedBody = rigidBody;
	}

	public void ToggleJoint() {
		if ( GetComponent<FixedJoint>() ) {
			HingeJoint hingeJoint = Instantiate( hingeJointPrefab, transform.position, transform.rotation ).GetComponent<HingeJoint>();
			hingeJoint.connectedBody = GetComponent<FixedJoint>().connectedBody;
			Destroy( this.gameObject );
		}
		if ( GetComponent<HingeJoint>() ) {
			FixedJoint fixedJoint = Instantiate( fixedJointPrefab, transform.position, transform.rotation ).GetComponent<FixedJoint>();
			fixedJoint.connectedBody = GetComponent<HingeJoint>().connectedBody;
			Destroy( this.gameObject );
		}
	}
}
