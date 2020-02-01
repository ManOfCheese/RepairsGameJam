using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Collection", menuName = "Levels")]
public class Levels : ScriptableObject
{
    public List<Level> LevelList;
}
[System.Serializable]
public class Level
{
    public string LevelName;
    public int StickRequirement;
    public int EndItemRequirement;
    public int JointRequirement;
}
