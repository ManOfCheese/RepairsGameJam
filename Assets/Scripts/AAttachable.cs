using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttachable : MonoBehaviour {

	//To Delete.
	public GameObject arrows;

	public virtual void Attach( Transform arrowTransform ) {
		Destroy( arrows );
	}
}
