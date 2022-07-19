using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In meters per second")] [SerializeField] float movementSpeed = 4f;
    [Tooltip("In meters")] [SerializeField] float xRange = 5f;
    [Tooltip("In meters")] [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    bool isControlEnabled = true;

    float xThrow, yThrow;

    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath() // called by string reference
    {
        print("Controls frozen");
        isControlEnabled = false;
        SetGunsActive(false);
    }

    private void ProcessTranslation()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        float yOffset = yThrow * movementSpeed * Time.deltaTime;
        float xOffset = xThrow * movementSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float rawXPos = transform.localPosition.x + xOffset;

        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else 
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var particleEmissionModule = gun.GetComponent<ParticleSystem>().emission;
            particleEmissionModule.enabled = isActive;
        }
    }
}

