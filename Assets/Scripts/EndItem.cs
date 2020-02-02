using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItem : ADeletable {

	public Rigidbody attachedToStick;
	public GameObject jointPrefab;
	public GameObject arrows;
	public List<GameObject> models;

	private ScoreTally scoreTally;

	private float jointOffset = 1f;

	private void Awake() {
		scoreTally = FindObjectOfType<ScoreTally>();
		Instantiate( models[ Random.Range( 0, models.Count - 1 ) ], this.transform );
	}

	private void OnEnable() {
		scoreTally.AddEndItem( this );
	}

	private void OnDisable() {
		scoreTally.RemoveEndItem( this );
	}

	private void Start() {
        if (GetComponent<FixedJoint>() != null)
            GetComponent<FixedJoint>().connectedBody = attachedToStick;
	}

	public void ToggleJoint() {
		Instantiate( jointPrefab, transform.position + ( transform.position - attachedToStick.transform.position ).normalized * jointOffset, transform.rotation );
		Jonko connectionJoint = jointPrefab.GetComponent<Jonko>();
	}

	public override void Delete() {
		if ( !GetComponent<Joint>() ) {
			arrows.SetActive( true );
			Destroy( this.gameObject );
		}
	}
}
