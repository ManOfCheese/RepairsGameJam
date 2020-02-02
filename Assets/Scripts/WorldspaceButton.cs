using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WorldspaceButton : MonoBehaviour
{
    public Menu.Buttons MenuButton;
    public PauseMenu.ButtonFunctions PauseMenuButton;

    public GameObject CheckMark;
}
