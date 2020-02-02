using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : ADeletable {

	public List<DirectionalArrow> arrows;
	public GameObject createEndItem;
	public List<GameObject> models;

	private ScoreTally scoreTally;
	private FixedJoint fixedJoint;
	private bool hasFixedJoint;

	private void Awake() {
		scoreTally = FindObjectOfType<ScoreTally>();
		Instantiate( models[ Random.Range( 0, models.Count - 1 ) ], this.transform );
	}

	private void OnEnable() {
		scoreTally.AddStick( this.gameObject );
	}

	private void OnDisable() {
		scoreTally.RemoveStick( this.gameObject );
	}

	public void AddFixedJoint() {
		fixedJoint = gameObject.AddComponent<FixedJoint>();
		hasFixedJoint = true;
	}

	public void ConnectRigidBody( Rigidbody rigidBody ) {
		if ( hasFixedJoint ) {
			fixedJoint.connectedBody = rigidBody;
		}
	}

	public override void Delete() {
		if ( !GetComponent<Joint>() ) {
			Destroy( transform.parent.gameObject );
		}
	}

	private void Update() {
		if ( GetComponent<FixedJoint>() ) {
			if ( GetComponent<FixedJoint>().connectedBody == null ) {
				foreach ( DirectionalArrow arrow in arrows ) {
					if ( Physics.OverlapBox( arrow.transform.position, new Vector3( 0.2f, 0.2f, 0.2f ) ).Length <= 0 ) {
						arrow.gameObject.SetActive( true );
					}
				}
				createEndItem.SetActive( true );
				Destroy( GetComponent<FixedJoint>() );
			}
		}
		if ( GetComponent<HingeJoint>() ) {
			if ( GetComponent<HingeJoint>().connectedBody == null ) {
				foreach ( DirectionalArrow arrow in arrows ) {
					if ( Physics.OverlapBox( arrow.transform.position, new Vector3( 0.1f, 0.1f, 0.1f ) ).Length <= 0 ) {
						arrow.gameObject.SetActive( true );
					}
				}
				createEndItem.SetActive( true );
				Destroy( GetComponent<HingeJoint>() );
			}
		}
	}

	private void OnTriggerEnter( Collider other ) {
		if ( other.GetComponent<Jonko>() ) {
			AddFixedJoint();
			ConnectRigidBody( other.GetComponent<Rigidbody>() );
		}
	}
}
