using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType {
	Fixed,
	Hinge
}

public class Jonko : ADeletable {

	public GameObject fixedJointPrefab;
	public GameObject hingeJointPrefab;
	public GameObject endItemPrefab;
	public ScoreTally scoreTally;
	public bool isAnchor;

	private void Awake() {
		scoreTally = FindObjectOfType<ScoreTally>();
	}

	private void OnEnable() {
		if ( !isAnchor ) {
			scoreTally.AddConnectionJoint( this );
		}
	}

	private void OnDisable() {
		if ( !isAnchor ) {
			scoreTally.RemoveConnectionJoint( this );
		}
	}

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

	public override void Delete() {
		if ( !GetComponent<HingeJoint>() ) {
			Destroy( transform.parent.gameObject );
		}
	}
}
