using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelAnimation : TimeTravelObjectBehaviour
{
    public float Present;
    public float Past;

    private Animator animator;
    private Animation ani;


    public void Start()
    {
        animator = GetComponent<Animator>();
        ani = GetComponent<Animation>();
        ani.Play();
    }

    void Update()
    {
        UpdateAffectionLevel();
        animator.SetFloat("RewindTime", Mathf.Lerp(Present, Past, AffectionLevel));
    }
}
