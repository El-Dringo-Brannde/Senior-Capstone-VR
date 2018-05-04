using System.Collections;
using System;
using Wrld;
using Wrld.Space;
using UnityEngine;
using SocketIO; 

public class TestCameraSwitching : MonoBehaviour
{
    public GameObject VRTK_SDKManager;
    public Camera Wrld_Camera;
    public GameObject Wrld_3D;
    public SocketIOComponent socket; 

    private PositionObjectAtLatitudeAndLongitude foo = new PositionObjectAtLatitudeAndLongitude();
    

    public void Start()
    {
        InitSocketIO();
    }

    public void InitSocketIO()
    {
        socket.On("map", (SocketIOEvent e) =>
        {
            var LatLng = e.data.ToDictionary();
            Wrld_3D.SetActive(true);
            foo.Init(); // Why does this work..
            SwitchToWrld3D(LatLong.FromDegrees(Convert.ToDouble(LatLng["lat"]), Convert.ToDouble(LatLng["lng"])));
        });

        socket.On("home", (SocketIOEvent e) =>
        {
             SwitchToDataViewing();
        });

    }

    public void SwitchToWrld3D(LatLong DestLocation)
    {
        VRTK_SDKManager.SetActive(false);
        Wrld_Camera.gameObject.SetActive(true);
        
        foo.MoveGraphs(DestLocation);
    }


    public void SwitchToDataViewing()
    {
        Wrld_3D.SetActive(false);
        Wrld_Camera.gameObject.SetActive(false);
        VRTK_SDKManager.SetActive(true);
    }
}



