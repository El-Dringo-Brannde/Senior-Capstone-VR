using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SteamVR_TrackedObject))]


public class PickUp : MonoBehaviour {
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject trackedObj;

    public Transform sphere;
    public Transform foo; 

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate() { 
        device =  SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
            Debug.Log("you pulled the trigger");
        } if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            Debug.Log("you activated press down on the trigger");
        } if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            Debug.Log("you activated touch down on the trigger");
        } if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
            Debug.Log("you activated touch Up on the trigger");
        } if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            Debug.Log("you activated press up on the Touch pad");
            sphere.transform.position = Vector3.zero;
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        } // reset the ball back to the center 
    } // runs almost every frame getting controller inputs

    void OnTriggerStay(Collider collidedWith)
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        Debug.Log("Collided!!!" + collidedWith.name);
        if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with" + collidedWith.name + " and held down touch");
            collidedWith.attachedRigidbody.isKinematic = true; 
            // remove rigid body
            collidedWith.gameObject.transform.SetParent(this.gameObject.transform); 
            // move sphere relative to controller
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            collidedWith.gameObject.transform.SetParent(null);
            collidedWith.attachedRigidbody.isKinematic = false;
            tossObject(collidedWith.attachedRigidbody);
        }
    }

    void OnTriggerExit(Collider collidedWith)
    {
        if (collidedWith)
        {
            collidedWith.gameObject.transform.SetParent(null);
            collidedWith.attachedRigidbody.isKinematic = false;
        }
        
    }

    void tossObject(Rigidbody rigidBody) {
        Transform origin = trackedObj ? trackedObj.origin : trackedObj.transform.parent; 
        if (origin) {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        } else {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }
        

    }
}
