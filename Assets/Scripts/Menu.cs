using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    #region Variables
    [SerializeField] private SceneManager_GGJ SceneManager_;
    public SceneManager_GGJ GetSceneManager()
    {
        if (SceneManager_ == null)
        {
            SceneManager_ = GetComponent<SceneManager_GGJ>();
            if (SceneManager_ == null)
            {
                SceneManager_ = gameObject.AddComponent<SceneManager_GGJ>();
            }
        }
        return SceneManager_;
    }

    public enum Buttons
    {
        Play,
        Tutorial,
        Options,
        Exit,
        Sound_Master,
        Sound_Music,
        Sound_Effects,
        Back,
        Previous,
        Next,
        None
    }

    public Buttons SelectedButton;
    public LayerMask MenuLayer;

    [Header("Animations")]
    public bool AnimateMenuTransitions;
    [Space]
    public Animator FirstMenuParent;
    public List<WorldspaceButton> FirstMenu;
    public Animator SecondMenuParent;
    public List<WorldspaceButton> SecondMenu;

    [Header("Audio")]
    public AudioMixer AudioMixer;

    [Header("TutorialMenu")]
    public GameObject TutorialMenu;
    public GameObject Slides;
    [SerializeField] private List<Transform> SlideList;
    public float SlideOffset = 1f;
    public int CurrentSlide = 0;
    #endregion

    #region Menu Functions
    #region FirstMenu
    public void Play()
    {
        Debug.Log("Play");
        GetSceneManager().NextScene();
    }
    public void Tutorial()
    {
        Debug.Log("Tutorial");
        ShowSubMenu(SubMenu.Tutorial);
    }
    public void Options()
    {
        Debug.Log("Options -- switch menus");
        ShowSubMenu(SubMenu.Options);
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit(0);
    }
    #endregion

    #region Second_Menu
    public void SoundMaster(WorldspaceButton buttonComponent)
    {
        Debug.Log("ToggleSoundMaster");
        if (buttonComponent.CheckMark != null)
        {
            bool newState = !buttonComponent.CheckMark.activeInHierarchy;
            buttonComponent.CheckMark.SetActive(newState);
            if (AudioMixer != null)
            {
                AudioMixer.SetFloat("Master", 
                    newState? 0f : -80f);
            }
        }
    }
    public void SoundMusic(WorldspaceButton buttonComponent)
    {
        Debug.Log("ToggleSoundMusic");
        if (buttonComponent.CheckMark != null)
        {
            bool newState = !buttonComponent.CheckMark.activeInHierarchy;
            buttonComponent.CheckMark.SetActive(newState);
            if (AudioMixer  != null)
            {
                AudioMixer.SetFloat("Music",
                    newState ? 0f : -80f);
            }
        }
    }
    public void SoundEffects(WorldspaceButton buttonComponent)
    {
        Debug.Log("ToggleSoundEffects");
        if (buttonComponent.CheckMark != null)
        {
            bool newState = !buttonComponent.CheckMark.activeInHierarchy;
            buttonComponent.CheckMark.SetActive(newState);
            if (AudioMixer != null)
            {
                AudioMixer.SetFloat("SFX",
                    newState ? 0f : -80f);
                AudioMixer.SetFloat("Background",
                    newState ? 0f : -80f);
                AudioMixer.SetFloat("Objects",
                    newState ? 0f : -80f);
            }
        }
    }
    public void Back()
    {
        Debug.Log("Back");
        ShowSubMenu(SubMenu.Main);
    }
    #endregion

    #region Tutorial Menu
    public void Previous()
    {
        if (CurrentSlide <= 0)
        {
            Debug.Log("Already at first slide");
            return;
        }
        Debug.Log("Previous");
        CurrentSlide--;
        foreach (Transform slide in SlideList)
        {
            slide.transform.localPosition = slide.transform.localPosition + new Vector3(SlideOffset, 0f, 0f);
        }
    }

    public void Next()
    {
        if (CurrentSlide >= SlideList.Count - 1)
        {
            Debug.Log("Already at last slide");
            return;
        }
        Debug.Log("Next");
        CurrentSlide++;
        foreach (Transform slide in SlideList)
        {
            slide.transform.localPosition = slide.transform.localPosition + new Vector3(-SlideOffset, 0f, 0f);
        }

    }
    #endregion
    #endregion

    #region Additional menu functions
    public enum SubMenu
    {
        Main,
        Options,
        Tutorial
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
        if (AnimateMenuTransitions)
        {
            Debug.LogError("Menu animations not implemented");
            FirstMenuParent.gameObject.SetActive(true);
            SecondMenuParent.gameObject.SetActive(true);
            FirstMenuParent.SetBool(0, subMenu == SubMenu.Main);
            SecondMenuParent.SetBool(0, subMenu == SubMenu.Options);
        }
        else
        {
            FirstMenuParent.gameObject.SetActive(subMenu == SubMenu.Main);
            SecondMenuParent.gameObject.SetActive(subMenu == SubMenu.Options);
            TutorialMenu.SetActive(subMenu == SubMenu.Tutorial);
            foreach (WorldspaceButton button in TutorialMenu.GetComponentsInChildren<WorldspaceButton>())
            {
                button.GetComponent<Collider>().enabled = subMenu == SubMenu.Tutorial;
            }
        }
    }

    public void InitSlides()
    {
        if (TutorialMenu == null)
        {
            Debug.LogError("Tutorial menu not found");
            return;
        }
        if (Slides == null)
        {
            Debug.LogError("Slides not found");
            return;
        }

        CurrentSlide = 0;

        foreach (Transform slide in Slides.GetComponentsInChildren<Transform>())
        {
            SlideList.Add(slide);
            Debug.Log("Adding slide", slide);
        }
        SlideList.RemoveAt(0);
        for (int i = 0; i < SlideList.Count; i++)
        {
            Transform slide = SlideList[i];
            Debug.Log("Initiating slide with offset " + SlideOffset, slide);
            slide.localPosition = new Vector3(Slides.transform.localPosition.x + SlideOffset * i, 0f, 0f);
        }
    }
    #endregion

    #region Unity Functions
    private void Start()
    {
        InitSlides();
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
                        case Buttons.Previous:
                            Previous();
                            break;
                        case Buttons.Next:
                            Next();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    #endregion
}
