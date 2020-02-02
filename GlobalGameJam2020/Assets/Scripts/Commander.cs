using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AK;

public class Commander : MonoBehaviour
{
    public GameObject HelperPrefab;
    public int NumHelpers = 2;

    private List<Helper> helpers = new List<Helper>();
    private TimeTravelAffector affector;

    void Start()
    {
        affector = GetComponent<TimeTravelAffector>();
        for (int i = 0; i < NumHelpers; i++)
        {
            GameObject helperObject = GameObject.Instantiate(HelperPrefab);
            Helper helper = helperObject.GetComponent<Helper>();
            helpers.Add(helper);
            helper.Target = transform;
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Command"))
        {
            bool outOfHelpers = true;

            foreach(Helper helper in helpers)
            {
                if(helper.Target == transform) //at least one helper available
                {
                    outOfHelpers = false;
                }

                if (TrySendHelper(helper) || TryRetrieveHelper(helper))
                {
                    return;
                }
            }

            if(outOfHelpers)
            {
                AkSoundEngine.PostEvent("sfx_lock_far", gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("sfx_lock_fail", gameObject);
            }
        }
    }

    private bool TrySendHelper(Helper helper)
    {
        if (helper.Target == transform)
        {
            TimeTravelObject availableObject = helper.Affector.ClosestAvailableTimeTravelObject;
            if (availableObject != null)
            {
                helper.Target = availableObject.transform;

                AkSoundEngine.PostEvent("sfx_lock_music", gameObject);
                AkSoundEngine.PostEvent("sfx_lock_object_light", helper.Target.gameObject);

                return  true;
            }
        }
        return false;
    }

    private bool TryRetrieveHelper(Helper helper)
    {
        if (helper.Target != transform)
        {
            TimeTravelObject target = helper.Target.GetComponent<TimeTravelObject>();
            if (affector.IsTimeTravelObjectInRange(target))
            {
                AkSoundEngine.PostEvent("sfx_unlock_music", gameObject);
                AkSoundEngine.PostEvent("sfx_unlock_object_light", helper.Target.gameObject);

                helper.Target = transform;

                return true;
            }
        }
        return false;
    }
}
