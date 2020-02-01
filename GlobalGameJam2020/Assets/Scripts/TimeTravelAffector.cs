using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelAffector : MonoBehaviour
{
    public TimeTravelObject ClosestAvailableTimeTravelObject
    {
        get
        {
            if (availableObjects.Count > 0)
            {
                float closestDistance = float.PositiveInfinity;
                TimeTravelObject closestObject = null;
                foreach (TimeTravelObject availableObject in availableObjects)
                {
                    if (availableObject.IsLocked) continue;

                    float distance = Vector3.Distance(availableObject.transform.position, transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = availableObject;
                    }
                }
                return closestObject;
            }
            else
            {
                return null;
            }
        }
    }
    public void AddAvailableObject(TimeTravelObject newObject)
    {
        availableObjects.Add(newObject);
    }

    public void RemoveAvailableObject(TimeTravelObject newObject)
    {
        availableObjects.Remove(newObject);
    }

    private List<TimeTravelObject> availableObjects = new List<TimeTravelObject>();

    public bool IsTimeTravelObjectInRange(TimeTravelObject t)
    {
        return availableObjects.Contains(t);
    }
}
