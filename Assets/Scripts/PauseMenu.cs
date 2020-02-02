using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuObject;

    [SerializeField] private List<SubMenu> SubMenus;
    public List<SubMenu> GetSubMenus()
    {
        if (SubMenus == null || SubMenus.Count == 0)
        {
            if (MenuObject == null)
            {
                Debug.LogError("No menu object provided", this);
                return SubMenus;
            }

            Debug.Log("Finding submenus in children");
            SubMenus = new List<SubMenu>();
            SubMenus.AddRange(MenuObject.GetComponentsInChildren<SubMenu>());
        }
        return SubMenus;
    }

    public List<WorldspaceButton> AllButtons;
    public LayerMask MenuLayer;


    public enum ButtonFunctions
    {
        // Main pause menu
        Resume,
        MainMenu,
        Options,
        Exit,

        // Option pause menu
        Controls,
        SoundMaster,
        SoundEffects,
        SoundMusic,

        // Shared between Option and Controls
        Back,

        None
    }



    void Start()
    {
        foreach (SubMenu subMenu in GetSubMenus())
        {
            foreach (WorldspaceButton button in subMenu.GetButtons())
            {
                AllButtons.Add(button);
                //button.gameObject.layer = MenuLayer;
                //Debug.Log("Setting layer of " + button.name + " to " + MenuLayer, button);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuObject == null)
            {
                Debug.LogError("Menu object not assigned");
                return;
            }

            if (MenuObject.activeInHierarchy)
            {
                // Already open, close.
                MenuObject.SetActive(false);
            }
            else
            {
                // Already closed, open.
                MenuObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, MenuLayer))
            {
                WorldspaceButton buttonComponent = hit.transform.GetComponent<WorldspaceButton>();
                if (buttonComponent != null)
                {
                    switch (buttonComponent.PauseMenuButton)
                    {
                        case ButtonFunctions.Resume:
                            Resume();
                            break;
                        case ButtonFunctions.Options:
                            Options();
                            break;
                        case ButtonFunctions.Exit:
                            Exit();
                            break;
                        case ButtonFunctions.Controls:
                            Controls();
                            break;
                        case ButtonFunctions.SoundMaster:
                            ToggleMaster();
                            break;
                        case ButtonFunctions.SoundEffects:
                            ToggleSounds();
                            break;
                        case ButtonFunctions.SoundMusic:
                            ToggleMusic();
                            break;
                        case ButtonFunctions.Back:
                            Back();
                            break;
                        case ButtonFunctions.MainMenu:
                            MainMenu();
                            break;
                    }
                }
            }
        }
    }

    private void MainMenu()
    {
        SceneManager_GGJ sceneManagerIntance = FindObjectOfType<SceneManager_GGJ>();
        sceneManagerIntance.PreviousScene();
    }

    #region Button Functions
    private void Resume()
    {
        ClosePauseMenu();
    }

    private void Options()
    {
        throw new NotImplementedException();
    }

    private void Exit()
    {
        Application.Quit(0);
    }

    private void Controls()
    {
        throw new NotImplementedException();
    }

    private void ToggleMaster()
    {
        throw new NotImplementedException();
    }

    private void ToggleSounds()
    {
        throw new NotImplementedException();
    }

    private void ToggleMusic()
    {
        throw new NotImplementedException();
    }

    private void Back()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Other Functions
    private void ClosePauseMenu()
    {
        MenuObject.SetActive(false);
    }
    #endregion
}
