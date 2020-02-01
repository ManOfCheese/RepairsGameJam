using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : ADeletable {

	public List<DirectionalArrow> arrows;
	public GameObject createEndItem;

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
				foreach ( DirectionalArrow arrow in arrows ) {
					if ( Physics.OverlapBox( arrow.transform.position, new Vector3( 0.2f, 0.2f, 0.2f ) ).Length <= 0 ) {
						arrow.gameObject.SetActive( true );
					}
				}
				createEndItem.SetActive( true );
				Destroy( GetComponent<FixedJoint>() );
			}
		}
		if ( GetComponent<HingeJoint>() ) {
			if ( GetComponent<HingeJoint>().connectedBody == null ) {
				foreach ( DirectionalArrow arrow in arrows ) {
					if ( Physics.OverlapBox( arrow.transform.position, new Vector3( 0.1f, 0.1f, 0.1f ) ).Length <= 0 ) {
						arrow.gameObject.SetActive( true );
					}
				}
				createEndItem.SetActive( true );
				Destroy( GetComponent<HingeJoint>() );
			}
		}
	}
}
