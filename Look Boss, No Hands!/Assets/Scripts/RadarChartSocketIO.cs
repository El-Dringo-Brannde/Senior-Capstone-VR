﻿using System;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using JSONTypes;
using ChartAndGraph; 


public class RadarChartSocketIO : MonoBehaviour {
    RadarChart Radar; 
    SocketIOComponent socket;
    Dictionary<string, string> BrandSalesDictionary;

    Material[] MaterialColorArray = new Material[10];
	// Use this for initialization
	void Start () {
        Radar = GameObject.Find("RadarChart").GetComponent<RadarChart>();
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();

        var Mat = new MaterialBuilder();
        Mat.BuildMaterials();
        MaterialColorArray = Mat.MaterialColorArray;
        SocketIOConnection();
        HandleData();
	}

    void SocketIOConnection()
    {
        socket.On("connect", (SocketIOEvent e) =>
        {
            if (Radar)
            {
                Debug.Log("Connected from Radar");
            }
        });
    }

    void HandleData()
    {

        socket.On("Pie_Chart", (SocketIOEvent obj) =>
        {
            ClearRadarChart();
            this.BrandSalesDictionary = obj.data.ToDictionary();
            int idx = 0;
            foreach (var item in this.BrandSalesDictionary)
            {
                if (item.Key != "user")
                {
                    Radar.DataSource.AddGroup(item.Key);
                    Radar.DataSource.SetValue("Player 1", item.Key, Convert.ToDouble(item.Value));
                    idx++;
                }
            }
        });
    }

    void ClearRadarChart()
    {
        Radar.DataSource.ClearGroups();
    }
	
	// Update is called once per frame
	void Update () {}
}
