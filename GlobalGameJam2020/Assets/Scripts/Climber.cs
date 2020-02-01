using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public float ClimbingSpeed = 1f;
    public bool IsClimbing 
    { 
        get 
        { 
            return climbables.Count > 0; 
        } 
    }

    public Vector3 Forward
    {
        get
        {
            Vector3 forward = Vector3.zero;
            foreach(var c in climbables)
            {
                Vector3 dir = transform.position - c.transform.position;
                dir.y = 0;
                forward += Vector3.Normalize(dir);
            }
            return Vector3.Normalize(forward);
        }
    }

    private List<Climbable> climbables = new List<Climbable>();

    public void AddClimbable(Climbable c)
    {
        climbables.Add(c);
    }
    
    public void RemoveClimbable(Climbable c)
    {
        climbables.Remove(c);
    }
}
