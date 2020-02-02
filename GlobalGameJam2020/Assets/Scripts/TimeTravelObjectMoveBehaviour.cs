using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectMoveBehaviour : TimeTravelObjectBehaviour
{
    public Vector3 Present = Vector3.one;
    public Vector3 Past = Vector3.one;

    void Update()
    {
        UpdateAffectionLevel();

        transform.localPosition = Vector3.Lerp(Present, Past, AffectionLevel);
    }
}

