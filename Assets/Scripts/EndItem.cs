using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItem : MonoBehaviour {

	public Rigidbody attachedToStick;
	public GameObject jointPrefab;

	private float jointOffset = 1f;

	private void Start() {
        if (GetComponent<FixedJoint>() != null)
            GetComponent<FixedJoint>().connectedBody = attachedToStick;
	}

	public void ToggleJoint() {
		Instantiate( jointPrefab, transform.position + ( transform.position - attachedToStick.transform.position ).normalized * jointOffset, transform.rotation );
		ConnectionJoint connectionJoint = jointPrefab.GetComponent<ConnectionJoint>();
		connectionJoint.connectionType = ConnectionType.Fixed;
		connectionJoint.attachedToStick = attachedToStick;
	}

}
