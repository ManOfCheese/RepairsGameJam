using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEndItem : AClickable {

	public GameObject stick;
	public GameObject endItemPrefab;

	public override void OnClick() {
		GameObject newEndItem = Instantiate( endItemPrefab );

		newEndItem.transform.rotation = stick.transform.rotation;
		newEndItem.transform.position = stick.transform.position + new Vector3( 0, stick.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z );
	}

}
