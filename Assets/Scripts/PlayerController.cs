using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float yRange = 7f;

    [SerializeField] private float positionPitchFactor = -2f;

    [SerializeField] private float controlPitchFactor = -10;
    [SerializeField] private float controlYawFactor = 2;
    [SerializeField] private float controlRollFactor = -20;

    [SerializeField] private GameObject[] arrayLaser;

    float xThrow, yThrow;
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFireing();
    }



    private void ProcessRotation()
    {
        float pitchDuoToPosition = transform.localRotation.y * positionPitchFactor;
        float pitchDuoToThrow = yThrow * controlPitchFactor;
        float pitch = pitchDuoToPosition + pitchDuoToThrow;
        float yaw = transform.localPosition.x  * controlYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
         xThrow = Input.GetAxis("Horizontal");
         yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedX = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampedY = Mathf.Clamp(newYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }
    private void ProcessFireing()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLaserActive(true);
        } else
        {
            SetLaserActive(false);

        }
    }

    private void SetLaserActive(bool value)
    {
        foreach(GameObject laser in arrayLaser)
        {
            var particleModule = laser.GetComponent<ParticleSystem>();
            particleModule.enableEmission = value;
        }
    }
}
