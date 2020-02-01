using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType {
	Fixed,
	Hinge
}

public class ConnectionJoint : ADeletable {

	public ConnectionType connectionType;
	public Rigidbody attachedToStick;
	public GameObject jointPrefab;
	public GameObject endItemPrefab;
	public GameObject fixedStick; //stick prefab
	public GameObject hingeStick; //stick prefab
	public bool isAnchorPoint;

	private float jointOffset = 1f;

	public void CreateNewStick( Transform arrowTransform ) {
		GameObject newStick = null;
		if ( connectionType == ConnectionType.Fixed ) {
			newStick = Instantiate( fixedStick );
			newStick.GetComponent<FixedJoint>().connectedBody = myRigidBody;
		}
		else if ( connectionType == ConnectionType.Hinge ) {
			newStick = Instantiate( hingeStick );
			newStick.GetComponent<HingeJoint>().connectedBody = myRigidBody;
		}
		if (newStick != null){
			float halfObjectLength = newStick.GetComponent<MeshRenderer>().bounds.extents.y;

			newStick.transform.rotation = arrowTransform.rotation;
			newStick.transform.position = ( ( transform.position + ( arrowTransform.position - transform.position ) * jointOffset ) ) + 
				new Vector3( ( arrowTransform.position.x - transform.position.x ) * halfObjectLength, ( arrowTransform.position.y - transform.position.y ) * halfObjectLength, arrowTransform.position.z );

			//Create standard fixed joint.
			Instantiate( jointPrefab, transform.position + ( transform.position - attachedToStick.transform.position ).normalized * jointOffset, transform.rotation );
			ConnectionJoint connectionJoint = jointPrefab.GetComponent<ConnectionJoint>();
			connectionJoint.connectionType = ConnectionType.Fixed;
			connectionJoint.GetComponent<FixedJoint>().connectedBody = newStick.gameObject.GetComponent<Rigidbody>();
		}
	}

	public void ToggleJoint() {
		if ( connectionType == ConnectionType.Fixed ) {
			connectionType = ConnectionType.Hinge;
		}
		else if ( connectionType == ConnectionType.Hinge ) {
			connectionType = ConnectionType.Fixed;
		}
	}

	public void CreateEndItem() {
		Instantiate( jointPrefab, transform.position + ( transform.position - attachedToStick.transform.position ).normalized * jointOffset, transform.rotation );
		EndItem endItem = jointPrefab.GetComponent<EndItem>();
		if ( isAnchorPoint ) {
			endItem.attachedToStick = myRigidBody;
		}
		else {
			endItem.attachedToStick = attachedToStick;
		}
		Delete();
	}

	private void CreateJoint() {
		Instantiate( jointPrefab, transform.position + ( transform.position - attachedToStick.transform.position ).normalized * jointOffset, transform.rotation );
		ConnectionJoint connectionJoint = jointPrefab.GetComponent<ConnectionJoint>();
		connectionJoint.connectionType = ConnectionType.Fixed;
		if ( isAnchorPoint ) {
			connectionJoint.attachedToStick = myRigidBody;
		}
		else {
			connectionJoint.attachedToStick = attachedToStick;
		}
	}
}
