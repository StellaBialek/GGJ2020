using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectColliderBehaviour : TimeTravelObjectBehaviour
{
    public bool Present;
    public bool Past;

    private Collider collider;


    public void Start()
    {
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        UpdateAffectionLevel();
        if (AffectionLevel == 1)
            collider.enabled = Past;
        else
            collider.enabled = Present;
    }
}
