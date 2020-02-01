using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TimeTravelObject : MonoBehaviour
{
    public float Radius = 1f;
    public AnimationCurve Affection;
    public bool IsAffected { get { return affectors.Count > 0; } }
    public bool IsLocked
    {
        get
        {
            if (affectors.Count > 0)
            {
                foreach(TimeTravelAffector affector in affectors)
                {
                    Helper helper = affector.GetComponent<Helper>();
                    if(helper && helper.Target == transform)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
    }

    public float AffectionLevel
    {
        get
        {
            float affection = 0f;
            if(affectors.Count > 0)
            {
                foreach (TimeTravelAffector affector in affectors)
                {
                    float distance = Vector3.Distance(affector.transform.position, transform.position);
                    affection = Mathf.Max(affection, Affection.Evaluate(1f - (distance / Radius)));
                }
            }
            return affection;
        }
    }

    public void AddAffector(TimeTravelAffector affector)
    {
        affectors.Add(affector);
    }

    public void RemoveAffector(TimeTravelAffector affector)
    {
        affectors.Remove(affector);
    }

    private List<TimeTravelAffector> affectors = new List<TimeTravelAffector>();

    void Start()
    {
        foreach(TimeTravelObjectBehaviour behaviour in GetComponentsInChildren<TimeTravelObjectBehaviour>())
        {
            behaviour.Parent = this;
        }
        SphereCollider trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = Radius;
    }
    public void OnTriggerEnter(Collider other)
    {
        TimeTravelAffector affector = other.gameObject.GetComponent<TimeTravelAffector>();
        if (affector)
        {
            AddAffector(affector);
            affector.AddAvailableObject(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        TimeTravelAffector affector = other.gameObject.GetComponent<TimeTravelAffector>();
        if (affector)
        {
            RemoveAffector(affector);
            affector.RemoveAvailableObject(this);
        }
    }
}
