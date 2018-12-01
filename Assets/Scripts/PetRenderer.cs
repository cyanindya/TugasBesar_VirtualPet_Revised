using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRenderer : MonoBehaviour {

    // placeholder for rendering - probably should be put on separate code
    PetBehavior pet;
    Animator animator;

    int animationCount = 0;
    int maxAnimationCount = 3;
    
    // Use this for initialization
    void Awake () {
        pet = GameObject.FindObjectOfType<PetBehavior>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
        SetAnimationState();
    }

    void SetAnimationState()
    {

        manualTrigger("sleep");

        // sad
        if (pet.happiness < 40 && pet.happiness > 0)
        {
            animator.SetBool("isSad", true);
            animator.SetBool("isSick", false);
        }
        // sick
        else if (pet.happiness == 0 || pet.energy == 0)
        {
            animator.SetBool("isSad", false);
            animator.SetBool("isSick", true);
        }
        // idle
        else
        {
            animator.SetBool("isSad", false);
            animator.SetBool("isSick", false);
        }
        

    }

    public void manualTrigger(string flag)
    {
        switch (flag)
        {
            case "eat":
                animator.SetTrigger("isEating");
                break;
            case "play":
                animator.SetTrigger("isPlaying");
                break;
            case "bath":
                animator.SetTrigger("isBathing");
                break;
            case "sleep":
                if (pet.isSleep)
                    animator.SetBool("isSleeping", true);
                else
                    animator.SetBool("isSleeping", false);
                Debug.Log(animator.GetBool("isSleeping"));
                break;
            case "pat":
                animator.SetTrigger("isPatted");
                break;
            default:
                break;
        }
    }

    void ReturnToDefaultState()
    {
        if (animator.GetInteger("animationCount") < maxAnimationCount)
        {
            animationCount += 1;
            animator.SetInteger("animationCount", animationCount);
            
        }
        else
        {
            animator.ResetTrigger("isEating");
            animator.ResetTrigger("isPlaying");
            animator.ResetTrigger("isBathing");
            animator.ResetTrigger("isPatted");

            animator.SetInteger("animationCount", 0);
        }

    }
}
