using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SteamVR_TrackedObject))]


public class ParentFixedJoint : MonoBehaviour {

    SteamVR_Controller.Device device;
    SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
    void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("You have collided with " + other.name);
    }
}
