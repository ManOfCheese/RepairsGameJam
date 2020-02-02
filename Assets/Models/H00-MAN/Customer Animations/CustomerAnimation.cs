using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimation : MonoBehaviour {

    public Animator CharacterAnim;
    public Animator CameraAnim;

    public void isWalking()
    {
        CharacterAnim.SetBool("isWalking", true);
    }

    public void isNotWalking()
    {
        CharacterAnim.SetBool("isWalking", false);
    }

    public void CamTilt()
    {
       CameraAnim.SetBool("CamRotate", false);
    }

    public void CamTiltBack()
    {
        CameraAnim.SetBool("CamRotate", true);
    }

}
