using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToStick : AAttachable {

	public CreateEndItem createEndItem;

	//References
	public Stick stick;

	//Prefabs
	public GameObject fixedJointPrefab;
	public GameObject hingeJointPrefab;

	private float jointOffset = 2f;
	private GameObject playField;

	public override void Attach( DirectionalArrow arrow, Transform arrowTransform, Jonko joint ) {
		playField = FindObjectOfType<PlayField>().gameObject;
		GameObject newStick = Instantiate( Resources.Load("JointAndStick", typeof( GameObject ) ) as GameObject , playField.transform );

		newStick.transform.rotation = arrow.gameObject.transform.rotation;
		Vector3 normalizedDir = ( arrowTransform.position - transform.position ).normalized;
		Vector3 dir1or0 = new Vector3( Mathf.RoundToInt( normalizedDir.x ), Mathf.RoundToInt( normalizedDir.y ), 0 );
		newStick.transform.position = stick.transform.position + ( dir1or0 * jointOffset );

		stick.AddFixedJoint();
		stick.ConnectRigidBody( newStick.transform.Find("HingeJoint").GetComponent<Rigidbody>() );
		base.Attach( arrow, arrowTransform, joint );
		createEndItem.visible = false;
		createEndItem.ApplyVisibility();
	}
}
