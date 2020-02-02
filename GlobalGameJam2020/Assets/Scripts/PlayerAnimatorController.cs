using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public AnimationCurve WalkAnimationSpeed;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("X");
        float y = Input.GetAxis("Y");
        float movespeed = x * x + y * y ;
        anim.SetFloat("movespeed", movespeed);
        anim.SetFloat("walkanimationspeed", WalkAnimationSpeed.Evaluate(movespeed));
    }
}
