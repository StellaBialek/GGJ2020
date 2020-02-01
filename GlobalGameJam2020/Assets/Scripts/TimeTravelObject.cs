using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TimeTravelObject : MonoBehaviour
{
    public float Radius = 1f;
    public AnimationCurve Affection;
    public bool IsAffected { get { return affectors.Count > 0; } }

    public float AffectionLevel
    {
        get
        {
            float result = 0f;
            if(affectors.Count > 0)
            {
                foreach (TimeTravelAffector affector in affectors)
                {
                    float distance = Vector3.Distance(affector.transform.position, transform.position);
                    result += Affection.Evaluate(1f - (distance / Radius));
                }
                result /= affectors.Count;
            }
            return result;
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
        }
    }

    public void OnTriggerExit(Collider other)
    {
        TimeTravelAffector affector = other.gameObject.GetComponent<TimeTravelAffector>();
        if (affector)
        {
            RemoveAffector(affector);
        }
    }
}
