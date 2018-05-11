using System.Collections;
using System;
using Wrld;
using Wrld.Space;
using UnityEngine;
using SocketIO; 

/* 
 * Class Component that listens on the websocket for any commands that may switch the 
 * VR environment from maps to home or vice versa
 * 
 */
public class TestCameraSwitching : MonoBehaviour
{
    public GameObject PositionedFrame;
    public GameObject Wrld_Camera;
    public GameObject Wrld_3D;
    public SocketIOComponent socket;

    private bool atHome = true;

    public void Start()
    {
        InitSocketIO();
    }

    public void InitSocketIO()
    {
        socket.On("map", (SocketIOEvent e) =>
        {
            var LatLng = e.data.ToDictionary();
            Globals.latLng = LatLong.FromDegrees(Convert.ToDouble(LatLng["lat"]), Convert.ToDouble(LatLng["lng"]));
            var Camera_Pos = LatLongAltitude.FromDegrees(Convert.ToDouble(LatLng["lat"]) - 0.002f, Convert.ToDouble(LatLng["lng"]), 150.0);

            SwitchToWrld3D(Camera_Pos);

        });

        socket.On("home", (SocketIOEvent e) =>
        {
             SwitchToDataViewing();
        });

    }

    public void SwitchToWrld3D(LatLongAltitude Camera_Pos)
    {
        PositionedFrame.SetActive(true);

        Wrld_3D.SetActive(true);
        Wrld_Camera.gameObject.transform.position = Api.Instance.CameraApi.GeographicToWorldPoint(Camera_Pos);
        atHome = false; 
    }


    public void SwitchToDataViewing()
    {
        Wrld_3D.SetActive(false);
        PositionedFrame.SetActive(true);
        if (!atHome)
        {
            Wrld_Camera.gameObject.transform.position = new Vector3(0, 0, 0f);
        }
        atHome = true;
    }
}



