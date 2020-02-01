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
            return numClimbables > 0; 
        } 
    }
    private int numClimbables = 0;

    public void AddClimbable()
    {
        numClimbables++;
    }
    
    public void RemoveClimbable()
    {
        numClimbables--;
    }
}
