using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : AClickable {

	public AAttachable attachable;
	public bool visible;

	private ClickFunctionsStateMachine stateMachine;

	private void Awake() {
		stateMachine = FindObjectOfType<ClickFunctionsStateMachine>();
		stateMachine.arrows.Add( this );
		if ( enabled ) {
			visible = true;
		}
		else {
			visible = false;
		}
	}

	private void OnDestroy() {
		stateMachine.arrows.Remove( this );
	}

	public void ApplyVisibility() {
		if ( visible ) {
			gameObject.SetActive( true );
		}
		else {
			gameObject.SetActive( false );
		}
	}

	public override void OnClick() {
		attachable.Attach( this,  transform );
	}
}
