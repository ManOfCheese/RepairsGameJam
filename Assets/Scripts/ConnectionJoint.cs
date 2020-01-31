using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType {
	Fixed,
	Hinge
}

public class ConnectionJoint : MonoBehaviour {

	public Rigidbody rigidBody;
	public ConnectionType connectionType;
	public Rigidbody attachedToStick;
	public GameObject fixedStick; //stick prefab
	public GameObject hingeStick; //stick prefab

	public void CreateNewStick( Transform arrowTransform ) {
		GameObject newStick = null;
		if ( connectionType == ConnectionType.Fixed ) {
			newStick = Instantiate( fixedStick );
			newStick.GetComponent<FixedJoint>().connectedBody = attachedToStick;
		}
		else if ( connectionType == ConnectionType.Hinge ) {
			newStick = Instantiate( hingeStick );
			newStick.GetComponent<HingeJoint>().connectedBody = attachedToStick;
		}
		if (newStick != null){
			float halfObjectLength = newStick.GetComponent<MeshRenderer>().bounds.extents.y + 0.5f;
			newStick.transform.position = new Vector3( arrowTransform.position.x * halfObjectLength, arrowTransform.position.y * halfObjectLength, arrowTransform.position.z * halfObjectLength );
			newStick.transform.rotation = arrowTransform.rotation;
		}
	}

}
