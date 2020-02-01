using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickFunctionsStateMachine : MonoBehaviour
{
    #region Variables
    public Color PlaceStickColor = Color.white;
    public Color ToggleJointColor = Color.white;
    public Color ToggleEndPartColor = Color.white;
    public Color DeleteColor = Color.white;
    public Image ControlsBackground;
    #endregion

    #region States
    public enum States
    {
        PlaceStick,
        ToggleJoint,
        ToggleEndPart
    }

    [Header("States")]
    public States CurrentState;
    public void SetState(States pNewState)
    {
        CurrentState = pNewState;
        if (ControlsBackground == null)
        {
            return;
        }
        switch (CurrentState)
        {
            case States.PlaceStick:
                ControlsBackground.color = PlaceStickColor;
                break;
            case States.ToggleJoint:
                ControlsBackground.color = ToggleJointColor;
                break;
            case States.ToggleEndPart:
                ControlsBackground.color = ToggleEndPartColor;
                break;
            default:
                Debug.LogError("Unknown state");
                break;
        }
    }

    public void SetState(int pNewState)
    {
        SetState((States)pNewState);
    }

    public void PlaceStick()
    {
		if ( Input.GetMouseButtonDown( 0 ) ) {
            Debug.Log("Place stick");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
				if ( hit.transform.gameObject.GetComponent<DirectionalArrow>() ) {
					hit.transform.gameObject.GetComponent<DirectionalArrow>().OnClick();
				}
			}
		}
	}

    public void ToggleJoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Toggle joint");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
				if ( hit.transform.gameObject.GetComponent<ConnectionJoint>() ) {
					hit.transform.gameObject.GetComponent<ConnectionJoint>().ToggleJoint();
				}
				if ( hit.transform.gameObject.GetComponent<EndItem>() ) {
					hit.transform.gameObject.GetComponent<EndItem>().ToggleJoint();
				}
			}
		}
	}

    public void ToggleEndPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Toggle end part");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
				if ( hit.transform.gameObject.GetComponent<ConnectionJoint>() ) {
					hit.transform.gameObject.GetComponent<ConnectionJoint>().CreateEndItem();
				}
			}
		}
    }

    #endregion
    public void Delete()
    {
        Debug.Log("Delete");
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
			if ( hit.transform.gameObject.GetComponent<ADeletable>() ) {
				hit.transform.gameObject.GetComponent<ADeletable>().Delete();
			}
		}
	}

    #region Unity Functions
    void Start()
    {
        SetState(States.PlaceStick);
    }

    void Update()
    {
        Debug.Log(CurrentState);
        if (Input.GetMouseButtonDown(1))
        {
            Delete();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(States.PlaceStick);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(States.ToggleJoint);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(States.ToggleEndPart);
            return;
        }

        switch (CurrentState)
        {
            case States.PlaceStick:
                PlaceStick();
                break;
            case States.ToggleJoint:
                ToggleJoint();
                break;
            case States.ToggleEndPart:
                ToggleEndPart();
                break;
            default:
                Debug.LogError("Unknown state");
                break;
        }
    }
    #endregion
}
