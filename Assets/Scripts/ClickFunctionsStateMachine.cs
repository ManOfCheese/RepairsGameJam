using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickFunctionsStateMachine : MonoBehaviour
{
    #region Variables
    public Color NoFunctionColor = Color.white;
    public Color PlaceStickColor = Color.white;
    public Color PlaceJointColor = Color.white;
    public Color PlaceEndPartColor = Color.white;
    public Color DeleteColor = Color.white;
    public Image ControlsBackground;
    #endregion

    #region States
    public enum States
    {
        NoFunction,
        PlaceStick,
        PlaceJoint,
        PlaceEndPart,
        Delete
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
            case States.NoFunction:
                ControlsBackground.color = NoFunctionColor;
                break;
            case States.PlaceJoint:
                ControlsBackground.color = PlaceJointColor;
                break;
            case States.PlaceStick:
                ControlsBackground.color = PlaceStickColor;
                break;
            case States.PlaceEndPart:
                ControlsBackground.color = PlaceEndPartColor;
                break;
            case States.Delete:
                ControlsBackground.color = DeleteColor;
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

    public void NoFunction()
    {

    }

    public void PlaceStick()
    {

    }

    public void PlaceJoint()
    {

    }

    public void PlaceEndPart()
    {

    }

    public void Delete()
    {

    }
    #endregion

    #region Unity Functions
    void Start()
    {
        SetState(States.NoFunction);
    }

    void Update()
    {
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case States.NoFunction:
                NoFunction();
                break;
            case States.PlaceJoint:
                PlaceJoint();
                break;
            case States.PlaceStick:
                PlaceStick();
                break;
            case States.PlaceEndPart:
                PlaceEndPart();
                break;
            case States.Delete:
                Delete();
                break;
            default:
                Debug.LogError("Unknown state");
                break;
        }
    }
    #endregion
}
