using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public enum Buttons
    {
        Play,
        Tutorial,
        Options,
        Exit,
        Sound_Master,
        Sound_Music,
        Sound_Effects,
        Back
    }

    public Buttons SelectedButton;
    public LayerMask MenuLayer;

    public Animator FirstMenuParent;
    public List<WorldspaceButton> FirstMenu;
    public Animator SecondMenuParent;
    public List<WorldspaceButton> SecondMenu;

    private void Start()
    {
        ShowSubMenu(SubMenu.Main);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, MenuLayer))
            {
                WorldspaceButton buttonComponent = hit.transform.GetComponent<WorldspaceButton>();
                if (buttonComponent != null)
                {
                    switch (buttonComponent.MenuButton)
                    {
                        case Buttons.Play:
                            Play();
                            break;
                        case Buttons.Tutorial:
                            Tutorial();
                            break;
                        case Buttons.Options:
                            Options();
                            break;
                        case Buttons.Exit:
                            Exit();
                            break;
                        case Buttons.Sound_Master:
                            SoundMaster(buttonComponent);
                            break;
                        case Buttons.Sound_Music:
                            SoundMusic(buttonComponent);
                            break;
                        case Buttons.Sound_Effects:
                            SoundEffects(buttonComponent);
                            break;
                        case Buttons.Back:
                            Back();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    #region Menu Functions
    #region FirstMenu
    public void Play()
    {
        Debug.Log("Play");
    }
    public void Tutorial()
    {
        Debug.Log("Tutorial");
    }
    public void Options()
    {
        Debug.Log("Options -- switch menus");
        ShowSubMenu(SubMenu.Options);
    }
    public void Exit()
    {
        Debug.Log("Exit");
    }
    #endregion

    #region Second_Menu
    public void SoundMaster(WorldspaceButton buttonComponent)
    {
        Debug.Log("ToggleSoundMaster");
        if (buttonComponent.CheckMark != null) buttonComponent.CheckMark.SetActive(!buttonComponent.CheckMark.activeInHierarchy);
    }
    public void SoundMusic(WorldspaceButton buttonComponent)
    {
        Debug.Log("ToggleSoundMusic");
        if (buttonComponent.CheckMark != null) buttonComponent.CheckMark.SetActive(!buttonComponent.CheckMark.activeInHierarchy);
    }
    public void SoundEffects(WorldspaceButton buttonComponent)
    {
        Debug.Log("ToggleSoundEffects");
        if (buttonComponent.CheckMark != null) buttonComponent.CheckMark.SetActive(!buttonComponent.CheckMark.activeInHierarchy);
    }
    public void Back()
    {
        Debug.Log("Back");
        ShowSubMenu(SubMenu.Main);
    }
    #endregion
    #endregion

    #region Additional menu functions
    public enum SubMenu
    {
        Main,
        Options
    }
    public void ShowSubMenu(SubMenu subMenu)
    {
        foreach (WorldspaceButton button in FirstMenu)
        {
            button.GetComponent<Collider>().enabled = subMenu == SubMenu.Main;
        }
        foreach (WorldspaceButton button in SecondMenu)
        {
            button.GetComponent<Collider>().enabled = subMenu == SubMenu.Options;
        }
        if (subMenu == SubMenu.Main)
        {
            FirstMenuParent.Play("Text_Up");
            SecondMenuParent.Play("Text_Down");
            SecondMenuParent.Play();
        } 
        else if (subMenu == SubMenu.Options)
        {
            FirstMenuParent.Play();
            //FirstMenuParent.Play("Text_Up");
            //SecondMenuParent.Play("Text_Down");
        }

    }
    #endregion
}
