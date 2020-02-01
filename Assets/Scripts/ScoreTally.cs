#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Level progress")]
    public bool LevelComplete;

    public bool StickReqMet;
    public bool EndItemReqMet;
    public bool ConnectionJointReqMet;

    [SerializeField] private List<GameObject> Sticks;
    public void AddStick(GameObject stick)
    {
        Sticks.Add(stick);
        CheckStickRequirementMet();
    }
    public void RemoveStick(GameObject stick)
    {
        Sticks.Remove(stick);
        CheckStickRequirementMet();
    }
    public bool CheckStickRequirementMet()
    {
         return StickReqMet = Sticks.Count >= CurrentLevel.StickRequirement;
    }

    [SerializeField] private List<EndItem> EndItems;
    public void AddEndItem(EndItem endItem)
    {
        EndItems.Add(endItem);
        CheckEndItemRequirementMet();
    }
    public void RemoveEndItem(EndItem endItem)
    {
        EndItems.Remove(endItem);
        CheckEndItemRequirementMet();
    }
    public bool CheckEndItemRequirementMet()
    {
        return EndItemReqMet = EndItems.Count >= CurrentLevel.EndItemRequirement;
    }

    [SerializeField] private List<ConnectionJoint> ConnectionJoints;
    public void AddConnectionJoint(ConnectionJoint connectionJoint)
    {
        ConnectionJoints.Add(connectionJoint);
        CheckConnectionJointRequirementMet();
    }
    public void RemoveConnectionJoint(ConnectionJoint connectionJoint)
    {
        ConnectionJoints.Remove(connectionJoint);
        CheckConnectionJointRequirementMet();
    }
    public bool CheckConnectionJointRequirementMet()
    {
        return ConnectionJointReqMet = ConnectionJoints.Count >= CurrentLevel.JointRequirement;
    }

    public bool CheckAllRequirements()
    {
        LevelComplete = CheckStickRequirementMet() && CheckEndItemRequirementMet() && CheckConnectionJointRequirementMet();
        return LevelComplete;
    }

    #region Unity Functions
    private void Start()
    {
        CheckAllRequirements();
    }

    private void Update()
    {
        
    }

    #endregion
}
#pragma warning restore CS0649
