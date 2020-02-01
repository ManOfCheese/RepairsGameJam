using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEndItem : AClickable {

	public Stick stick;
	public GameObject endItemPrefab;
	public GameObject arrows;

	private float jointOffset = 2f;

	public override void OnClick() {
		GameObject newEndItem = Instantiate( endItemPrefab );

		newEndItem.transform.rotation = stick.transform.rotation;
		newEndItem.transform.position = stick.transform.position + new Vector3( 0, stick.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z );

		newEndItem.transform.rotation = transform.rotation;
		Vector3 normalizedDir = ( transform.position - stick.transform.position ).normalized;
		Vector3 dir1or0 = new Vector3( Mathf.RoundToInt( normalizedDir.x ), Mathf.RoundToInt( normalizedDir.y ), 0 );
		newEndItem.transform.position = stick.transform.position + ( dir1or0 * jointOffset );

		stick.AddFixedJoint();
		stick.ConnectRigidBody( newEndItem.GetComponent<Rigidbody>() );
		newEndItem.GetComponent<EndItem>().attachedToStick = stick.GetComponent<Rigidbody>();
		arrows.SetActive( false );
		newEndItem.GetComponent<EndItem>().arrows = arrows;
	}

}
