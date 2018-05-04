using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Wrld;
using Wrld.Space;
using SocketIO;



public class CustomPositionLatLng : MonoBehaviour {

    public GeographicTransform coordinateFrame;
    public Transform box;
    public SocketIOComponent socket;


    // Use this for initialization
    void Start () {
        Api.Instance.GeographicApi.RegisterGeographicTransform(coordinateFrame);
        handleSockets();
    }

    private void OnEnable()
    {
        Debug.Log("I'm enabled!");
    }


    void handleSockets()
    {
        socket.On("connect", (SocketIOEvent e) =>
        {
                Debug.Log("Connected from custom lat long");
        
        });

        socket.On("map", (SocketIOEvent e) =>
        {
            var LatLng = e.data.ToDictionary();
            var geoPos = LatLong.FromDegrees(Convert.ToDouble(LatLng["lat"]) - 0.001f, Convert.ToDouble(LatLng["lng"]));

            box.localPosition = new Vector3(0.0f, 40.0f, 0.0f);
            Debug.Log(coordinateFrame);
            coordinateFrame.SetPosition(geoPos);
        });
        
    }

    // Update is called once per frame
    void Update () {}
}
