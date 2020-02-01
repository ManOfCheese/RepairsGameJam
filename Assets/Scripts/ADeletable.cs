using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADeletable : AClickable {

	public virtual void Delete() {
		Destroy( this.gameObject );
	}

}
