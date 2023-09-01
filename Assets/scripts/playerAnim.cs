using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Animator animator;
    public void UpdateAnim(string anim, bool bool_)
    {
        switch (anim)
        {
            case "hit":
                animator.SetBool("isHit",bool_);
                break;
            default:
                break;
        }
    }
}
