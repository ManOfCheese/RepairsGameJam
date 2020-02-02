using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : AClickable {

	private ScoreTally scoreTally;
	private ClickFunctionsStateMachine stateMachine;

	private GameObject playField;
	private GameObject prePhysicsplayField;

	private List<Jonko> currentJointList;
	private List<GameObject> currentStickList;
	private List<EndItem> currentEndItemList;

	private bool physicsOn;

	private void Awake() {
		scoreTally = FindObjectOfType<ScoreTally>();
		stateMachine = FindObjectOfType<ClickFunctionsStateMachine>();
		playField = FindObjectOfType<PlayField>().gameObject;
	}

	public override void OnClick() {
		if ( !physicsOn ) {
			prePhysicsplayField = Instantiate( playField );
			prePhysicsplayField.SetActive( false );

			currentEndItemList = scoreTally.GetEndItemList();
			currentJointList = scoreTally.GetJointList();
			currentStickList = scoreTally.GetStickList();

			for ( int i = 0; i < stateMachine.arrows.Count; i++ ) {
				if ( stateMachine.arrows[ i ].visible ) {
					stateMachine.arrows[ i ].gameObject.SetActive( false );
				}
			}
			for ( int i = 0; i < stateMachine.createEndItems.Count; i++ ) {
				if ( stateMachine.arrows[ i ].visible ) {
					stateMachine.createEndItems[ i ].gameObject.SetActive( false );
				}
			}

			for ( int i = 0; i < currentEndItemList.Count; i++ ) {
				currentEndItemList[ i ].GetComponent<Rigidbody>().isKinematic = false;
			}
			for ( int i = 0; i < currentJointList.Count; i++ ) {
				currentJointList[ i ].GetComponent<Rigidbody>().isKinematic = false;
			}
			for ( int i = 0; i < currentStickList.Count; i++ ) {
				currentStickList[ i ].GetComponent<Rigidbody>().isKinematic = false;
			}
			physicsOn = true;

			if ( scoreTally.LevelComplete ) {
				scoreTally.Win();
			}
		}
		else if ( physicsOn ) {
			currentEndItemList = scoreTally.GetEndItemList();
			currentJointList = scoreTally.GetJointList();
			currentStickList = scoreTally.GetStickList();

			for ( int i = 0; i < stateMachine.arrows.Count; i++ ) {
				stateMachine.arrows[ i ].ApplyVisibility();
			}
			for ( int i = 0; i < stateMachine.createEndItems.Count; i++ ) {
				stateMachine.createEndItems[ i ].ApplyVisibility();
			}

			prePhysicsplayField.SetActive( true );
			Destroy( playField );
			playField = prePhysicsplayField;
			physicsOn = false;
		}
	}

}
