using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelReverse : TimeTravelObjectBehaviour
{


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
}
