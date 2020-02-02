using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK;

public class WalkingSoundController : MonoBehaviour
{
    private GroundChecker ground;

    public void Start()
    {
        ground = FindObjectOfType<GroundChecker>();
    }

    public void OnTriggerEnter(Collider other)
    {
        foreach(GameObject groundObject in ground.GroundObjects)
        {
            SurfaceProperty surface = groundObject.GetComponentInChildren<SurfaceProperty>();
            if(surface)
            {
                TriggerSound(surface);
            }
        }
    }

    private void TriggerSound(SurfaceProperty surface)
    {
        Debug.Log(surface.Material);
        //TODO: trigger AKSoundEngine
    }

}
