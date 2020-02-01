using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelWater : TimeTravelObjectBehaviour
{
    public Color Present;
    public Color Past;

    private MeshRenderer meshRenderer;


    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        UpdateAffectionLevel();
        meshRenderer.material.SetFloat("_FillRate", CurrentAffectionLevel);

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(mpb);
        Color color = Color.Lerp(Present, Past, CurrentAffectionLevel);
        mpb.SetColor("_Color", color);
        meshRenderer.SetPropertyBlock(mpb);
    }
}
