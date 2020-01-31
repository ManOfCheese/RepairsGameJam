using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFunctionsStateMachine : MonoBehaviour
{
    #region Variables
    public Color NoFunctionColor = Color.white;
    public Color PlaceStickColor = Color.white;
    public Color PlaceJointColor = Color.white;
    public Color PlaceEndPartColor = Color.white;
    public Color DeleteColor = Color.white;
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

    public States CurrentState;
    public void SetState(States pNewState)
    {
        CurrentState = pNewState;
    }

    public void SetState(int pNewState)
    {
        CurrentState = (States)pNewState;
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
