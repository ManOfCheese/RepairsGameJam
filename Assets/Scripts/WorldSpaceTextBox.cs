using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldSpaceTextBox : MonoBehaviour
{
    public Transform CanvasHolder;
    public Canvas WorldSpaceCanvas;

    [Range(.01f, 50f)]
    public float TextScale;
    [Range(.1f, 10f)]
    public float TextScaleMultiplier = 1f;
    public float TextScaleFinal;

    [Header("Runtime rotation")]
    public bool Rotate;
    [SerializeField] private float time = 0f;
    [Space]
    [SerializeField] private float rotationX;
    [Range(0f, 1f)]
    [SerializeField] private float rotationAmountX;
    [SerializeField] private float rotationY;
    [Range(0f, 1f)]
    [SerializeField] private float rotationAmountY;
    [SerializeField] private float rotationZ;
    [Range(0f, 1f)]
    [SerializeField] private float rotationAmountZ;

    [Header("Text Type Writer")]
    public TextMeshProUGUI TextComponent;
    public int CharactersPerFrame = 1;
    [TextArea]
    public string SourceText;
    [TextArea]
    public string OnScreenText;
    public char CurrentCharacter;

    public int TextLength;
    public int CurrentCharacterNumber;

    private void Start()
    {
        LoadNewText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
    }

    void Update()
    {
        SwayRotation();
        TypeWriteText();
    }

    void SwayRotation()
    {
        if (Rotate)
        {
            time += Time.deltaTime;
            rotationX = rotationAmountX * Mathf.Sin(time - Mathf.PI / 2f);
            rotationY = rotationAmountY * Mathf.Sin(time - Mathf.PI / 2f);
            rotationZ = rotationAmountZ * Mathf.Sin(time - Mathf.PI / 2f);
            CanvasHolder.transform.Rotate(rotationX, rotationY, rotationZ);
        }
        else
        {
            CanvasHolder.transform.rotation = Quaternion.identity;
        }
    }

    private void OnDrawGizmos()
    {
        InverselyScaleCanvas();
    }

    public void InverselyScaleCanvas()
    {
        if (WorldSpaceCanvas == null)
        {
            Debug.LogError("Assign a canvas", this);
            return;
        }
        TextScaleFinal = TextScale * TextScaleMultiplier;

        WorldSpaceCanvas.GetComponent<RectTransform>().sizeDelta = CanvasHolder.transform.lossyScale / TextScaleFinal;
        Vector3 newScale = Vector3.one;
        newScale.x = Mathf.Pow(CanvasHolder.transform.lossyScale.x, -1) * TextScaleFinal;
        newScale.y = Mathf.Pow(CanvasHolder.transform.lossyScale.y, -1) * TextScaleFinal;
        WorldSpaceCanvas.transform.localScale = newScale;
    }

    public void LoadNewText(string text)
    {
        CurrentCharacterNumber = 0;
        SourceText = text;
    }

    public void TypeWriteText()
    {
        if (string.IsNullOrWhiteSpace(SourceText))
        {
            Debug.Log("No text to typewrite");
            return;
        }
        if (CurrentCharacterNumber < SourceText.Length)
        {
            OnScreenText += SourceText[CurrentCharacterNumber];
            CurrentCharacterNumber++;
            if (TextComponent != null)
            {
                TextComponent.text = OnScreenText;
            }
        }
    }

    public void SkipTypeWrite()
    {
        OnScreenText = SourceText;
        CurrentCharacterNumber = SourceText.Length;
    }
}
