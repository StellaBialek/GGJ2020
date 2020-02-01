using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public Transform Target { get; set; }
    public TimeTravelAffector Affector { get; set; }

    public float Speed = 5f;
    public float MinOffset = 2;
    public float MaxOffset = 3;
    public float MinDirectionDuration = 0.2f;
    public float MaxDirectionDuration = 1f;
    public float SmoothTime = 5f;

    private Vector3 speed;
    private Vector3 directionDuration;

    private Vector3 offset;

    public void Start()
    {
        RandomizeDirection(ref speed.x, ref directionDuration.x);
        RandomizeDirection(ref speed.y, ref directionDuration.y);
        RandomizeDirection(ref speed.z, ref directionDuration.z);

        Affector = GetComponent<TimeTravelAffector>();
    }
    void Update()
    {
        UpdateDirection(ref speed.x, ref directionDuration.x);
        UpdateDirection(ref speed.y, ref directionDuration.y);
        UpdateDirection(ref speed.z, ref directionDuration.z);

        offset.x += speed.x;
        offset.y += speed.y;
        offset.z = MinOffset;

        Vector3 o = Vector3.forward;
        o = Quaternion.AngleAxis(offset.x, Vector3.up) * o;
        o = Quaternion.AngleAxis(offset.y, Vector3.Cross(o, Vector3.up)) * o;
        o *= offset.z;

        Vector3 lastPosition = transform.position;
        Vector3 newPosition = Target.transform.position + o;
        transform.position = Vector3.Lerp(lastPosition, newPosition, Time.deltaTime * SmoothTime);

        transform.forward = Vector3.Normalize(lastPosition - transform.position);
    }

    private void UpdateDirection(ref float speed, ref float duration)
    {
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            RandomizeDirection(ref speed, ref duration);
        }
    }

    private void RandomizeDirection(ref float speed, ref float duration)
    {
        speed = Random.Range(-Speed, Speed);
        duration = Random.Range(MinDirectionDuration, MaxDirectionDuration);
    }
}
