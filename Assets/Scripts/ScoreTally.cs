#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTally : MonoBehaviour
{
    [Header("Level settings")]
    public Levels Levels;
    public int LevelSelect;
    [Space]
    public Level CurrentLevel;
    Level GetCurrentLevel()
    {
        if (Levels == null)
        {
            Debug.LogError("Assign a Levels scriptable object first");
            return null;
        }
        return null;
    }

    public void SetCurrentLevel()
    {
        if (Levels == null)
        {
            Debug.LogError("Assign a Levels scriptable object first");
            return;
        }
        LevelSelect = Mathf.Clamp(LevelSelect, 1, Levels.LevelList.Count);
        CurrentLevel = Levels.LevelList[LevelSelect - 1];
    }
    
    public void SetCurrentLevel(int newLevelIndex)
    {
        if (Levels == null)
        {
            Debug.LogError("Assign a Levels scriptable object first");
            return;
        }
        if (newLevelIndex < 0 || Levels.LevelList.Count <= newLevelIndex)
        {
            Debug.LogError("Invalid level index");
            return;
        }
        
        LevelSelect = newLevelIndex;
        CurrentLevel = Levels.LevelList[newLevelIndex];
    }

    #region Progress
    [Header("Level progress")]
    public bool LevelComplete;

    public bool StickReqMet;
    public bool EndItemReqMet;
    public bool JointReqMet;

    [SerializeField] private List<GameObject> Sticks;
    public void AddStick(GameObject stick)
    {
        Sticks.Add(stick);
        //CheckStickRequirementMet();
        CheckAllRequirements();
    }
    public void RemoveStick(GameObject stick)
    {
        Sticks.Remove(stick);
        //CheckStickRequirementMet();
        CheckAllRequirements();
    }
    public bool CheckStickRequirementMet()
    {
        StickReqMet = Sticks.Count >= CurrentLevel.StickRequirement;
        if (StickProgressText != null)
        {
            StickProgressText.text = "Sticks: " + Sticks.Count + " / " + CurrentLevel.StickRequirement;
            if (StickReqMet)
            {
                StickProgressText.color = ReqCompleteColor;
            }
            else
            {
                StickProgressText.color = ReqIncompleteColor;
            }
        }
        else Debug.Log("Stick Progress text not assigned");
        return StickReqMet;
    }

    [SerializeField] private List<EndItem> EndItems;
    public void AddEndItem(EndItem endItem)
    {
        EndItems.Add(endItem);
        //CheckEndItemRequirementMet();
        CheckAllRequirements();
    }
    public void RemoveEndItem(EndItem endItem)
    {
        EndItems.Remove(endItem);
        //CheckEndItemRequirementMet();
        CheckAllRequirements();
    }
    public bool CheckEndItemRequirementMet()
    {
        EndItemReqMet = EndItems.Count >= CurrentLevel.EndItemRequirement;
        if (EndItemProgressText != null)
        {
            EndItemProgressText.text = "Sticks: " + EndItems.Count + " / " + CurrentLevel.EndItemRequirement;
            if (EndItemReqMet)
            {
                EndItemProgressText.color = ReqCompleteColor;
            }
            else
            {
                EndItemProgressText.color = ReqIncompleteColor;
            }
        }
        else Debug.Log("End Item Progress text not assigned");
        return EndItemReqMet;
    }

    [SerializeField] private List<ConnectionJoint> Joints;
    public void AddConnectionJoint(ConnectionJoint connectionJoint)
    {
        Joints.Add(connectionJoint);
        //CheckConnectionJointRequirementMet();
        CheckAllRequirements();
    }
    public void RemoveConnectionJoint(ConnectionJoint connectionJoint)
    {
        Joints.Remove(connectionJoint);
        //CheckConnectionJointRequirementMet();
        CheckAllRequirements();
    }
    public bool CheckConnectionJointRequirementMet()
    {
        JointReqMet = Joints.Count >= CurrentLevel.JointRequirement;
        if (JointProgressText != null)
        {
            JointProgressText.text = "Sticks: " + Joints.Count + " / " + CurrentLevel.JointRequirement;
            if (JointReqMet)
            {
                JointProgressText.color = ReqCompleteColor;
            }
            else
            {
                JointProgressText.color = ReqIncompleteColor;
            }
        }
        else Debug.Log("Joint Progress text not assigned");
        return JointReqMet;
    }

    public bool CheckAllRequirements()
    {
        Debug.Log("Checking all level requirements");
        CheckStickRequirementMet();
        CheckEndItemRequirementMet();
        CheckConnectionJointRequirementMet();
        LevelComplete = StickReqMet && EndItemReqMet && JointReqMet;
        if (ProgressRect != null)
        {
            if (LevelComplete)
            {
                ProgressRect.color = ReqCompleteColor;
            }
            else
            {
                ProgressRect.color = Color.white;
            }
        }
        return LevelComplete;
    }
    #endregion

    #region Progress UI
    [Header("Progress UI")]
    public Image ProgressRect;
    public Text StickProgressText;
    public Text EndItemProgressText;
    public Text JointProgressText;

    public Color ReqIncompleteColor = Color.red;
    public Color ReqCompleteColor = Color.green;
    #endregion

    #region Unity Functions
    private void Start()
    {
        CheckAllRequirements();
    }
    #endregion
}
#pragma warning restore CS0649
