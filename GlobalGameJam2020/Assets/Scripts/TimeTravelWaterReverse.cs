using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelWaterReverse : TimeTravelObjectBehaviour
{

    private MeshRenderer meshRenderer;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override float AffectionLevel
    {
        get
        {
            float affectionLevel = 1 - Parent.AffectionLevel;
            foreach (TimeTravelObject requiredTimeTravelObject in AdditionalRequiredTimeTravelObjects)
            {
                affectionLevel *= requiredTimeTravelObject.AffectionLevel;
            }
            return affectionLevel;
        }
    }

    public override void UpdateAffectionLevel()
    {
        CurrentAffectionLevel = AffectionLevel;
        LastAffectionLevel = CurrentAffectionLevel;
    }

    void Update()
    {
        UpdateAffectionLevel();
        meshRenderer.material.SetFloat("_FillRate", CurrentAffectionLevel);
    }
}
