using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AClickable : MonoBehaviour {

	protected Rigidbody myRigidBody;
	protected Collider myCollider;

	void Start() {
		myRigidBody = GetComponent<Rigidbody>();
		myCollider = GetComponent<Collider>();
	}

	public virtual void OnClick() {

	}

	public virtual void OnClickRelease() {

	}
    
}
