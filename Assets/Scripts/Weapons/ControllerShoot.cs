using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerShoot : MonoBehaviour
{
    public Gun gunScript;  // Drag your Gun script here in the Inspector
    public InputDeviceCharacteristics controllerCharacteristics;

    private InputDevice targetDevice;

    void Start()
    {
        InitializeController();
    }

    void InitializeController()
    {
        // Find the controller with the given characteristics (e.g., right hand or left hand)
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            Debug.Log("Controller found: " + targetDevice.name);
        }
        else
        {
            Debug.LogWarning("No controller found with specified characteristics");
        }
    }

    void Update()
    {
        // Check if the device is valid
        if (!targetDevice.isValid)
        {
            InitializeController();
        }

        // Check if the trigger button is pressed
        bool triggerValue;
        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            // If the trigger is pressed, call the shoot function from Gun script
            gunScript.Shoot();
        }
    }
}
