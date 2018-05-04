using System.Collections;
using UnityEngine;
using Wrld;
using Wrld.Space;

public class PositionObjectAtLatitudeAndLongitude: MonoBehaviour
{
    public GeographicTransform coordinateFrame;
    public Camera cam;
    private LatLong latLng;
    public GameObject BarChart;
    public GameObject PieChart;

    private void OnEnable()
    {
        BarChart.SetActive(true);
        PieChart.SetActive(true);
        Api.Instance.GeographicApi.RegisterGeographicTransform(coordinateFrame);
        MoveGraphs();
    }

    private void MoveGraphs()
    {
    
        Api.Instance.CameraApi.SetControlledCamera(cam);

        latLng = Globals.latLng;
        coordinateFrame.SetPosition(latLng);
        PieChart.transform.position -= new Vector3(0, 75,0);
        BarChart.transform.position -= new Vector3(0, 75, 0);

    }

    private void OnDisable()
    {
        Api.Instance.GeographicApi.UnregisterGeographicTransform(coordinateFrame);
    }
}

