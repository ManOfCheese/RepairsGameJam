﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToAnchor : AAttachable {

	public GameObject anchor;

	//Prefabs
	public GameObject fixedJointPrefab;
	public GameObject hingeJointPrefab;
	public GameObject stickPrefab;

	private float jointOffset = 1f;

	public override void Attach( Transform arrowTransform ) {
		GameObject newStick = Instantiate( stickPrefab );
		float halfObjectLength = newStick.GetComponent<AttachToStick>().stick.GetComponent<MeshRenderer>().bounds.extents.y;

		newStick.transform.rotation = arrowTransform.rotation;
		newStick.transform.position = ( transform.position + ( arrowTransform.position - anchor.transform.position ) * jointOffset ) +
			new Vector3( ( arrowTransform.position.x - anchor.transform.position.x ) * halfObjectLength, ( arrowTransform.position.y - anchor.transform.position.y ) * halfObjectLength, transform.position.z );
		if ( anchor.GetComponent<FixedJoint>() ){
			anchor.GetComponent<FixedJoint>().connectedBody = newStick.GetComponentInChildren<Rigidbody>();
			anchor.GetComponent<FixedJoint>().connectedAnchor = new Vector3( 0, 2, 0 );
		}
		else if ( anchor.GetComponent<HingeJoint>() ) {
			anchor.GetComponent<HingeJoint>().connectedBody = newStick.GetComponentInChildren<Rigidbody>();
			anchor.GetComponent<HingeJoint>().connectedAnchor = new Vector3( 0, 2, 0 );
		}
		base.Attach( arrowTransform );
	}

}