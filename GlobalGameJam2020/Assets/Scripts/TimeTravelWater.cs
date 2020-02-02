using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelWater : TimeTravelObjectBehaviour
{
    public float Present;
    public float Past;

    private MeshRenderer meshRenderer;


    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        UpdateAffectionLevel();
        meshRenderer.material.SetFloat("_FillRate", Mathf.Lerp(Present, Past, AffectionLevel));
    }
}
