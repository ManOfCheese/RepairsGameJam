using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    List<WorldspaceButton> Buttons;
    public List<WorldspaceButton> GetButtons()
    {
        if (Buttons == null || Buttons.Count == 0)
        {
            Debug.Log("Finding buttons in children");
            Buttons = new List<WorldspaceButton>();
            Buttons.AddRange(GetComponentsInChildren<WorldspaceButton>());
        }
        return Buttons;
    }
}
