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

		newStickAndJoint.transform.rotation = arrow.gameObject.transform.rotation;
		Vector3 normalizedDir = ( arrowTransform.position - transform.position ).normalized;
		Vector3 dir1or0 = new Vector3( Mathf.RoundToInt( normalizedDir.x ), Mathf.RoundToInt( normalizedDir.y ), 0 );
		newStickAndJoint.transform.position = stick.transform.position + ( dir1or0 * jointOffset );

		stick.AddFixedJoint();
		stick.ConnectRigidBody( newStickAndJoint.transform.Find("HingeJoint").GetComponent<Rigidbody>() );
		base.Attach( arrow, arrowTransform );
		Destroy( createEndItem );
	}
}
