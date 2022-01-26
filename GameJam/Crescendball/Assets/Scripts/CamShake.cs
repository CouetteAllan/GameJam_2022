using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public Animator camAnimator;

    public void Shake()
    {
        camAnimator.SetTrigger("Shake");
    }
}
