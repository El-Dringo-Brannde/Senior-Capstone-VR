using System;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using JSONTypes;
using ChartAndGraph; 


public class RadarChartSocketIO : MonoBehaviour {
    public RadarChart Radar; 
    public SocketIOComponent socket;
    Dictionary<string, string> BrandSalesDictionary;

    Material[] MaterialColorArray = new Material[10];
	// Use this for initialization
	void Start () {
        var Mat = new MaterialBuilder();
        Mat.BuildMaterials();
        MaterialColorArray = Mat.MaterialColorArray;
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("Pie_Chart", HandleData);
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

    void HandleData(SocketIOEvent obj)
    {
        ClearRadarChart();
        this.BrandSalesDictionary = obj.data.ToDictionary();
        int idx = 0;
        foreach (var item in this.BrandSalesDictionary)
        {
            if (item.Key != "user")
            {
                try
                {
                    Radar.DataSource.AddGroup(item.Key);
                    Radar.DataSource.SetValue("Player 1", item.Key, Convert.ToDouble(item.Value));
                    idx++;
                } catch(Exception err) { Debug.Log(err);}
            }
        }
    }

    void ClearRadarChart()
    {
        Radar.DataSource.ClearGroups();
    }
	
	// Update is called once per frame
	void Update () {}
}
