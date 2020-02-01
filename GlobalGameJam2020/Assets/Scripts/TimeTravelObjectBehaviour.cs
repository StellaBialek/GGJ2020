using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectBehaviour : MonoBehaviour
{
    public Color Present;
    public Color Past;

    public TimeTravelObject Parent;

    private MeshRenderer meshRenderer;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    void Update()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(mpb);
        Color color = Color.Lerp(Present, Past, Parent.AffectionLevel);
        mpb.SetColor("_Color", color);
        meshRenderer.SetPropertyBlock(mpb);
    }
}
