using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : AClickable {

	private ConnectionJoint connectionPoint;

	private void Start() {
		connectionPoint = GetComponentInParent<ConnectionJoint>();
	}

	public override void OnClick() {
		base.OnClick();
		connectionPoint.CreateNewStick( transform );
	}
}
