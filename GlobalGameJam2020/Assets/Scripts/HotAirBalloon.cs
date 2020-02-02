using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloon : MonoBehaviour
{
    public Vector3 Direction = Vector3.right;
    public float Speed = 2f;
    public float HeightVariation = 1f;
    public float HeightVariationSpeed = 0.5f;

    private UI ui;
    private CameraMovement cam;

    private float startTime;
    private bool isFlying;
    private Transform t;
    private float baseheight;

    public void Start()
    {
        ui = FindObjectOfType<UI>();
        cam = FindObjectOfType<CameraMovement>();
        t = new GameObject().transform;
        baseheight = transform.position.y;
    }

    public void Update()
    {
        if(isFlying)
        {
            transform.position += Direction * Speed * Time.deltaTime;
            float height = baseheight + HeightVariation * Mathf.Sin((Time.time - startTime) * HeightVariationSpeed);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFlying)
            return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player)
        {
            ui.ShowCredits();
            startTime = Time.time;
            isFlying = true;
            t.position = player.transform.position;
            cam.Target = t;
        }
    }
}
