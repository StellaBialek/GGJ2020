using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator anim;
    private Vector3 lastPosition;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - lastPosition;
        dir.z = 0;
        float movespeed = dir.magnitude;
        Debug.Log(movespeed);
        anim.SetFloat("movespeed", movespeed);
        lastPosition = transform.position;   
    }
}
