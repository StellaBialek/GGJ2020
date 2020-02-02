using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK;

public class WalkingSoundController : MonoBehaviour
{
    private GroundChecker ground;
	private Climber climber;

    public void Start()
    {
        ground = FindObjectOfType<GroundChecker>();
		climber = FindObjectOfType<Climber>();
    }

    public void OnTriggerEnter(Collider other)
    {
	/*	if(climber.IsClimbing)
		{
			TriggerSound("Wood");
			return;
		}*/
        foreach(GameObject groundObject in ground.GroundObjects)
        {
            SurfaceProperty surface = groundObject.GetComponentInChildren<SurfaceProperty>();
            if(surface)
            {
                TriggerSound(surface.Material);
				return;
            }
        }

    }

    private void TriggerSound(string material)
    {
        Debug.Log(material); //TODO: replace by triggering AKSoundEngine
		AkSoundEngine.SetSwitch("Material", material, gameObject);
		AkSoundEngine.PostEvent("footsteps",gameObject);
    }

}
