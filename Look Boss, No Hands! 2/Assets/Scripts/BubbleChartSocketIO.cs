using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketIO;
using ChartAndGraph; 

public class BubbleChartSocketIO : MonoBehaviour {
    public GraphChartBase BubbleChart;
    public SocketIOComponent socket;
    Material[] MaterialColorArray = new Material[10];

	// Use this for initialization
	void Start () {
        var Mat = new MaterialBuilder();
        Mat.BuildMaterials();
        MaterialColorArray = Mat.MaterialColorArray;
        //SocketIOConnection();
        //HandleData(); 
    }


    void SocketIOConnection()
    {
        socket.On("connect", (SocketIOEvent e) =>
        {
            if (BubbleChart)
            {
                Debug.Log("Connected from Bubble Chart");
            }
        });
    }

    void HandleData()
    {
        BubbleChart.DataSource.AutomaticcHorizontaViewGap = 2f;
        BubbleChart.DataSource.AutomaticVerticallViewGap = 2f;
        /*socket.On("Bubble_Chart", (SocketIOEvent obj) => 
        {   
            var json = obj.data;
            string[] KeyArray = new string[100];
            KeyArray = json.keys.ToArray();
            int idx = 0;
            string[] cats = { "Player 1", "Player 2", "Player 3", "Player 4", "Player 5" };
            foreach (var key in KeyArray) // By Brand
            {
                if (key != "user")
                {
                    int monthsSoldCarBrand = json[key].Count;
                    for (var i = 0; i < monthsSoldCarBrand; i++) // By Month
                    { 
                        var month = (long)Convert.ToDouble(json[key][i]["month"].ToString());
                        var sales = (long)Convert.ToDouble(json[key][i]["sales"].ToString());
                        try { BubbleChart.DataSource.AddPointToCategory(cats[idx], month, sales); }
                        catch (Exception e) { Debug.Log(e); }
                    }
                }
                idx++;
            }
        });*/
    }
	
	// Update is called once per frame
	void Update () {}
}
