using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType {
	Fixed,
	Hinge
}

public class ConnectionJoint : MonoBehaviour {

	public ConnectionType connectionType;
	public Rigidbody attachedToStick;
	public GameObject fixedStick; //stick prefab
	public GameObject hingeStick; //stick prefab

	private float jointOffset = 1f;

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
			float halfObjectLength = newStick.GetComponent<MeshRenderer>().bounds.extents.y;

			newStick.transform.rotation = arrowTransform.rotation;
			newStick.transform.position = ( ( transform.position + ( arrowTransform.position - transform.position ) * jointOffset ) ) + new Vector3( ( arrowTransform.position.x - transform.position.x ) * halfObjectLength, ( arrowTransform.position.y - transform.position.y ) * halfObjectLength, arrowTransform.position.z );
		}
	}

}
