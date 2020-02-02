using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : AClickable {

	public AAttachable attachStick;
	public AAttachable attachAnchor;
	public bool visible;
	public bool isAnchor;

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
		bool stickOnly = false;
		if ( !isAnchor ) {
			Collider[] colliders = Physics.OverlapSphere( transform.parent.transform.parent.transform.position, 0.2f );
			for ( int i = 0; i < colliders.Length; i++ ) {
				if ( colliders[ i ].GetComponent<Jonko>() ) {
					attachStick.Attach( this, transform, colliders[ i ].GetComponent<Jonko>() );
					stickOnly = true;
					break;
				}
			}
			if ( !stickOnly ) {
				attachStick.Attach( this, transform, null );
			}
		}
		else {
			attachAnchor.Attach( this, transform, null );
		}
	}
}
