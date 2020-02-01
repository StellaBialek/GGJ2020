using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectColorBehaviour : TimeTravelObjectBehaviour
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

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(mpb);
        Color color = Color.Lerp(Present, Past, CurrentAffectionLevel);
        mpb.SetColor("_Color", color);
        meshRenderer.SetPropertyBlock(mpb);
    }
}
