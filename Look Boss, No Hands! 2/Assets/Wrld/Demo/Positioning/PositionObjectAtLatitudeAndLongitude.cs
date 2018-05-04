using System.Collections;
using UnityEngine;
using Wrld;
using Wrld.Space;

public class PositionObjectAtLatitudeAndLongitude: MonoBehaviour
{
    public GeographicTransform coordinateFrame;
    
    public Camera cam;
    private LatLong latLng;

    private void OnEnable()
    {
        Api.Instance.GeographicApi.RegisterGeographicTransform(coordinateFrame);
        MoveGraphs();
    }

    private void MoveGraphs()
    {
        Api.Instance.CameraApi.SetControlledCamera(cam);

        latLng = Globals.latLng;
        coordinateFrame.SetPosition(latLng);
    }

    private void OnDisable()
    {
        Api.Instance.GeographicApi.UnregisterGeographicTransform(coordinateFrame);
    }
}

