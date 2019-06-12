using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CameraScaling : MonoBehaviour {

    public Transform cameraRig;
    private GameObject leftController;
    private GameObject rightController;
    public VRTK_ControllerEvents leftControllerEv;
    public VRTK_ControllerEvents rightControllerEv;

    bool bothGripPressed;
    bool leftGripPressed;
    bool rightGripPressed;

    float initialDistance;
    float currentDistance;

    Vector3 initialScale;
    public float sensitivity = 0.5f;

    protected virtual void OnEnable()
    {
        RegisterEventsLeft(leftControllerEv);
        RegisterEventsRight(rightControllerEv);
    }

    protected virtual void RegisterEventsLeft(VRTK_ControllerEvents events)
    {
        if (events != null)
        {
            events.GripPressed += LeftGrip;
            events.GripReleased += LeftUnGrip;
        }
    }
    protected virtual void RegisterEventsRight(VRTK_ControllerEvents events)
    {
        if (events != null)
        {
            events.GripPressed += RightGrip;
            events.GripReleased += RightUnGrip;
        }
    }

    void Start()
    {
        if (!cameraRig)
            cameraRig = this.transform;
        leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
    }

    void LeftGrip(object sender, ControllerInteractionEventArgs e)
    {
        leftGripPressed = true;
        Debug.Log("Left Grip has been pressed");
        if (rightGripPressed)
        {
            if (!bothGripPressed)
            {
                bothGripPressed = true;
                print("both grip pressed");
                StartCoroutine(IsScaling());
            }
        }
    }
    void LeftUnGrip(object sender, ControllerInteractionEventArgs e)
    {
        leftGripPressed = false;
        bothGripPressed = false;
    }

    void RightGrip(object sender, ControllerInteractionEventArgs e)
    {
        rightGripPressed = true;
        Debug.Log("Right Grip has been pressed");
        if (leftGripPressed)
        {
            if (!bothGripPressed)
            {
                bothGripPressed = true;
                print("both grip pressed");
                StartCoroutine(IsScaling());
            }
        }
    }
    void RightUnGrip(object sender, ControllerInteractionEventArgs e)
    {
       rightGripPressed = false;
       bothGripPressed = false;
    }

    IEnumerator IsScaling()
    {
        initialDistance = Vector3.Distance(leftController.transform.position, rightController.transform.position);
        initialScale = this.transform.localScale;
        while (bothGripPressed)
        {
            currentDistance = Vector3.Distance(leftController.transform.position, rightController.transform.position);
            float distance = Vector3.Distance(leftController.transform.position, rightController.transform.position);
            this.transform.localScale = new Vector3(((currentDistance - initialDistance) * sensitivity) + initialScale.x, ((currentDistance - initialDistance) * sensitivity) + initialScale.y, ((currentDistance - initialDistance) * sensitivity) + initialScale.z);
            yield return null;
        }
    }
}
