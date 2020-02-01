using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectBehaviour : MonoBehaviour
{
    public TimeTravelObject[] AdditionalRequiredTimeTravelObjects = { };
    public float Smooth = 5;

    public float AffectionLevel
    {
        get
        {
            float affectionLevel = Parent.AffectionLevel;
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

    public void UpdateAffectionLevel()
    {
        CurrentAffectionLevel = Mathf.Lerp(LastAffectionLevel, AffectionLevel, Time.deltaTime * Smooth);
        LastAffectionLevel = CurrentAffectionLevel;
    }

    protected float CurrentAffectionLevel;
    protected float LastAffectionLevel;
}
