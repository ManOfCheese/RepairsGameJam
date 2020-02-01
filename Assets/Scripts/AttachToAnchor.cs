using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToAnchor : AAttachable {

	public GameObject anchor;
	public List<DirectionalArrow> arrows;

	//Prefabs
	public GameObject fixedJointPrefab;
	public GameObject hingeJointPrefab;
	public GameObject stickPrefab;

	private float jointOffset = 2f;

	public override void Attach( DirectionalArrow arrow, Transform arrowTransform ) {
		GameObject newStick = Instantiate( stickPrefab );
		float halfObjectLength = newStick.GetComponent<AttachToStick>().stick.GetComponent<MeshRenderer>().bounds.extents.y;

		newStick.transform.rotation = arrowTransform.rotation;
		Vector3 normalizedDir = ( arrowTransform.position - transform.position ).normalized;
		Vector3 dir1or0 = new Vector3( Mathf.RoundToInt( normalizedDir.x ), Mathf.RoundToInt( normalizedDir.y ), 0 );
		newStick.transform.position = anchor.transform.position + ( dir1or0 * jointOffset );
		if ( anchor.GetComponent<FixedJoint>() ){
			anchor.GetComponent<FixedJoint>().connectedBody = newStick.GetComponentInChildren<Rigidbody>();
			anchor.GetComponent<FixedJoint>().connectedAnchor = new Vector3( 0, 2, 0 );
		}
		else if ( anchor.GetComponent<HingeJoint>() ) {
			anchor.GetComponent<HingeJoint>().connectedBody = newStick.GetComponentInChildren<Rigidbody>();
			anchor.GetComponent<HingeJoint>().connectedAnchor = new Vector3( 0, 2, 0 );
		}
		base.Attach( arrow, arrowTransform );
	}

	private void Update() {
		if ( GetComponentInChildren<FixedJoint>() ) {
			if ( GetComponentInChildren<FixedJoint>().connectedBody == null ) {
				foreach ( DirectionalArrow arrow in arrows ) {
					if ( Physics.OverlapBox( arrow.transform.position, new Vector3( 0.2f, 0.2f, 0.2f ) ).Length <= 0 ) {
						arrow.gameObject.SetActive( true );
					}
				}
			}
		}
		if ( GetComponentInChildren<HingeJoint>() ) {
			if ( GetComponentInChildren<HingeJoint>().connectedBody == null ) {
				foreach ( DirectionalArrow arrow in arrows ) {
					if ( Physics.OverlapBox( arrow.transform.position, new Vector3( 0.1f, 0.1f, 0.1f ) ).Length <= 0 ) {
						arrow.gameObject.SetActive( true );
					}
				}
			}
		}
	}
}
