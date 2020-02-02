using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickFunctionsStateMachine : MonoBehaviour
{
    #region Variables
    public Color PlaceStickColor = Color.white;
    public Color ToggleJointColor = Color.white;
    public Color ToggleEndPartColor = Color.white;
    public Color DeleteColor = Color.white;
    public Image ControlsBackground;
	public List<DirectionalArrow> arrows;
	public List<CreateEndItem> createEndItems;
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
				else if ( hit.transform.gameObject.GetComponent<PlayButton>() ) {
					hit.transform.gameObject.GetComponent<PlayButton>().OnClick();
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
				if ( hit.transform.gameObject.GetComponent<Jonko>() ) {
					hit.transform.gameObject.GetComponent<Jonko>().ToggleJoint();
				}
				else if ( hit.transform.gameObject.GetComponent<PlayButton>() ) {
					hit.transform.gameObject.GetComponent<PlayButton>().OnClick();
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
				if ( hit.transform.gameObject.GetComponent<CreateEndItem>() ) {
					hit.transform.gameObject.GetComponent<CreateEndItem>().OnClick();
				}
				else if ( hit.transform.gameObject.GetComponent<PlayButton>() ) {
					hit.transform.gameObject.GetComponent<PlayButton>().OnClick();
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
        if (Input.GetMouseButtonDown(1))
        {
            Delete();
            return;
        }
		if ( Input.GetKeyDown( KeyCode.P ) ) {
			SceneManager.LoadScene( "FinalMainMenu" );
		}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(States.PlaceStick);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
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
