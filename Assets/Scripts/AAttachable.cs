using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttachable : MonoBehaviour {

	public virtual void Attach( DirectionalArrow arrow, Transform arrowTransform ) {
		arrow.visible = false;
		arrow.ApplyVisibility();
	}
}
