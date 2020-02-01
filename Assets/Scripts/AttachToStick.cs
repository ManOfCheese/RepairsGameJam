using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToStick : AAttachable {

	public GameObject createEndItem;

	//References
	public Stick stick;

	//Prefabs
	public GameObject fixedJointPrefab;
	public GameObject hingeJointPrefab;
	public GameObject stickPrefab;

	private float jointOffset = 2f;

	public override void Attach( DirectionalArrow arrow, Transform arrowTransform ) {
		GameObject newStickAndJoint = Instantiate( stickPrefab );
		//float halfStickLength = newStickAndJoint.GetComponent<AttachToStick>().stick.GetComponent<MeshRenderer>().bounds.extents.y;
		//float jointDiameter = newStickAndJoint.GetComponent<AttachToStick>().GetComponentInChildren<Jonko>().GetComponent<MeshRenderer>().bounds.extents.y * 2;
		//float totalDistanceBetweenCenters = jointDiameter + jointOffset;

		newStickAndJoint.transform.rotation = arrow.gameObject.transform.rotation;
		Vector3 normalizedDir = ( arrowTransform.position - transform.position ).normalized;
		Vector3 dir1or0 = new Vector3( Mathf.RoundToInt( normalizedDir.x ), Mathf.RoundToInt( normalizedDir.y ), 0 );
		Debug.Log( "arrowTransform: " + arrowTransform.position + " - transform.position " + transform.position + " | normalized = " + ( arrowTransform.position - transform.position ).normalized + " | Round to ints " + new Vector3( Mathf.RoundToInt( normalizedDir.x ), Mathf.RoundToInt( normalizedDir.y ), 0 ) + " * " + jointOffset );
		newStickAndJoint.transform.position = stick.transform.position + ( dir1or0 * jointOffset );

		stick.AddFixedJoint();
		stick.ConnectRigidBody( newStickAndJoint.GetComponentInChildren<HingeJoint>().GetComponent<Rigidbody>() );
		base.Attach( arrow, arrowTransform );
		Destroy( createEndItem );
	}
}
