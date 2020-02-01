using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : AClickable {

	public AAttachable attachable;

	public override void OnClick() {
		attachable.Attach( this,  transform );
	}
}
