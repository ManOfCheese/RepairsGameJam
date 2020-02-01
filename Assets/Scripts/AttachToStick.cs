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

	private float jointOffset = 1f;

	public override void Attach( Transform arrowTransform ) {
		GameObject newStickAndJoint = Instantiate( stickPrefab );
		float halfObjectLength = newStickAndJoint.GetComponent<AttachToStick>().stick.GetComponent<MeshRenderer>().bounds.extents.y;

		newStickAndJoint.transform.rotation = arrowTransform.rotation;
		newStickAndJoint.transform.position = ( stick.transform.position + ( arrowTransform.position - stick.transform.position ) * jointOffset ) +
			new Vector3( ( arrowTransform.position.x - stick.transform.position.x ) * ( halfObjectLength / 2 ), ( arrowTransform.position.y - stick.transform.position.y ) * ( halfObjectLength / 2 ), transform.position.z );

		stick.AddFixedJoint();
		stick.ConnectRigidBody( newStickAndJoint.GetComponentInChildren<HingeJoint>().GetComponent<Rigidbody>() );
		base.Attach( arrowTransform );
		Destroy( createEndItem );
	}
}
