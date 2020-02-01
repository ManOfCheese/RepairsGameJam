using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceTextBox : MonoBehaviour
{
    public Canvas WorldSpaceCanvas;

    [Range(.01f, 10f)]
    public float TextScale;

    public bool Rotate;

    // Start is called before the first frame update
    void Start()
    {
        //OffsetFromAttachTarget = transform.position - WorldSpaceCanvas.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //WorldSpaceCanvas.transform.position = transform.position + OffsetFromAttachTarget;
        if (Rotate)
        {
            transform.Rotate(0f, 1f, 0f);
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
        WorldSpaceCanvas.GetComponent<RectTransform>().sizeDelta = transform.localScale / TextScale;
        Vector3 newScale = Vector3.one;
        newScale.x = Mathf.Pow(transform.localScale.x, -1) * TextScale;
        newScale.y = Mathf.Pow(transform.localScale.y, -1) * TextScale;
        WorldSpaceCanvas.transform.localScale = newScale;
    }
}
