using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	private void Update() {
		if ( Input.GetMouseButtonDown( 0 ) ) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
				if ( hit.transform.gameObject.GetComponent<AClickable>() ) {
					hit.transform.gameObject.GetComponent<AClickable>().OnClick();
				}
			}
		}
	}
}
