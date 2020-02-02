using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectBehaviour : MonoBehaviour
{
    public TimeTravelObject[] AdditionalRequiredTimeTravelObjects = { };
    public float Smooth = 1;
    public float StartOffset = 0;
    public float EndOffset = 1;

    public virtual float AffectionLevel
    {
        get
        {
            float affectionLevel = Mathf.InverseLerp(StartOffset, EndOffset, Parent.AffectionLevel);
            foreach (TimeTravelObject requiredTimeTravelObject in AdditionalRequiredTimeTravelObjects)
            {
                affectionLevel *= requiredTimeTravelObject.AffectionLevel;
            }
            return affectionLevel;
        }
    }

    public bool AdditonalRequirementsFullfilled
    {
        get
        {
            if (AdditionalRequiredTimeTravelObjects.Length == 0)
                return true;
            bool fullfilled = true;
            foreach(TimeTravelObject requiredTimeTravelObject in AdditionalRequiredTimeTravelObjects)
            {
                fullfilled = fullfilled && requiredTimeTravelObject.AffectionLevel > 0.9f;
            }
            return fullfilled;
        }
    }
    public TimeTravelObject Parent { get; set; }

    public virtual void UpdateAffectionLevel()
    {
        CurrentAffectionLevel = Mathf.Lerp(LastAffectionLevel, AffectionLevel, Time.deltaTime * Smooth);
        LastAffectionLevel = CurrentAffectionLevel;
    }

    protected float CurrentAffectionLevel;
    protected float LastAffectionLevel;
}
