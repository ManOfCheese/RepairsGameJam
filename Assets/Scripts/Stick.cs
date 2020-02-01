using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : ADeletable {

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

	public override void Delete() {
		if ( !GetComponent<Joint>() ) {
			Destroy( transform.parent.gameObject );
		}
	}

	private void Update() {
		if ( GetComponent<FixedJoint>() ) {
			if ( GetComponent<FixedJoint>().connectedBody == null ) {
				Destroy( GetComponent<FixedJoint>() );
			}
		}
		if ( GetComponent<HingeJoint>() ) {
			if ( GetComponent<HingeJoint>().connectedBody == null ) {
				Destroy( GetComponent<HingeJoint>() );
			}
		}
	}
}
