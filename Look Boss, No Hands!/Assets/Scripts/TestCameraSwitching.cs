using System.Collections;
using Wrld;
using Wrld.Space;
using UnityEngine;

public class TestCameraSwitching : MonoBehaviour
{
    GameObject VRTK_SDKManager; 
    private void OnEnable()
    {
        var currentCamera = Camera.allCameras[0];
        Api.Instance.CameraApi.SetControlledCamera(currentCamera);
        VRTK_SDKManager = GameObject.Find("[VRTK_SDKManager]");
        StartCoroutine(Example());
    }

    public void SwitchToWrld3D(LatLong destLocation)
    {
        VRTK_SDKManager.SetActive(false);
        Api.Instance.CameraApi.MoveTo(destLocation, distanceFromInterest: 500);
    }


    public void switchToDataViewing()
    {
        VRTK_SDKManager.SetActive(true);
    }


    // used for debugging 
    IEnumerator Example()
    {
        yield return new WaitForSeconds(4f); 
    }



    private void OnDisable()
    {
        StopAllCoroutines();
    }
}



