using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {
        if (Rotate)
        {
            time += Time.deltaTime;
            rotationX = rotationAmountX * Mathf.Sin(time - Mathf.PI /2f);
            rotationY = rotationAmountY * Mathf.Sin(time - Mathf.PI /2f);
            rotationZ = rotationAmountZ * Mathf.Sin(time - Mathf.PI /2f);
            CanvasHolder.transform.Rotate(rotationX, rotationY, rotationZ);
        } else
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

        WorldSpaceCanvas.GetComponent<RectTransform>().sizeDelta = CanvasHolder.transform.localScale / TextScaleFinal;
        Vector3 newScale = Vector3.one;
        newScale.x = Mathf.Pow(CanvasHolder.transform.localScale.x, -1) * TextScaleFinal;
        newScale.y = Mathf.Pow(CanvasHolder.transform.localScale.y, -1) * TextScaleFinal;
        WorldSpaceCanvas.transform.localScale = newScale;
    }
}
