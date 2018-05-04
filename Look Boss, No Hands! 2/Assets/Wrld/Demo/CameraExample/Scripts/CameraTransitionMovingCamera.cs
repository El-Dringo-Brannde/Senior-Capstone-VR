using System.Collections;
using Wrld;
using Wrld.Space;
using UnityEngine;

public class CameraTransitionMovingCamera : MonoBehaviour
{

    public Camera cam; 
    private void OnEnable()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        Api.Instance.CameraApi.RegisterShouldConsumeInputDelegate(ShouldConsumeCameraInput);

        var startLocation = LatLong.FromDegrees(37.7858, -122.401);
        Api.Instance.CameraApi.SetControlledCamera(cam);
        
        Debug.Log(cam);
        Api.Instance.CameraApi.MoveTo(startLocation, distanceFromInterest: 10000, headingDegrees: 90, tiltDegrees: 50);
        Debug.Log('f');
        yield return new WaitForSeconds(4.0f);

        var destLocation = LatLong.FromDegrees(37.7952, -122.4028);
        Api.Instance.CameraApi.MoveTo(destLocation, distanceFromInterest: 500);
    }

    public bool ShouldConsumeCameraInput()
    {
        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }

        return true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
