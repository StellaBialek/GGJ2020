﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Commander : MonoBehaviour
{
    public GameObject HelperPrefab;
    public int NumHelpers = 3;

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
            helpers = helpers.OrderBy(x => Vector3.Distance(x.Target.transform.position, transform.position)).ToList<Helper>();
            foreach(Helper helper in helpers)
            {
                if (TrySendHelper(helper) || TryRetrieveHelper(helper))
                {
                    return;
                }
            }
            Debug.Log("no more helper interaction possible!");
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
                helper.Target = transform;
                return true;
            }
        }
        return false;
    }
}