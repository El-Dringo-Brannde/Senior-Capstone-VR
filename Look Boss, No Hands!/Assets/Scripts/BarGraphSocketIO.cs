using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketIO;
using ChartAndGraph;
using JSONTypes;


public class BarGraphSocketIO : MonoBehaviour {
    BarChart BarChart;
    SocketIOComponent socket;
    Dictionary<string, string> BrandSalesDictionary;
    Material[] MaterialColorArray = new Material[10];

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        BarChart = GameObject.Find("BarChart").GetComponent<BarChart>();
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
            if (BarChart)
            {
                Debug.Log("Connected Successfully From Bars!");
            }
        });
    }

    void HandleData()
    {
        socket.On("Bar_Chart", (SocketIOEvent obj) => // change data to piechart later
        {
            ClearBarChart();
            BarChart.DataSource.AutomaticMaxValue = true;
            BarChart.DataSource.AutomaticMinValue = true;
            var json = obj.data;
            string[] KeyArray = new string[100];
            KeyArray = json.keys.ToArray();
            int idx = 0;
            foreach (var key in KeyArray) // By Brand
            {
                if(key != "user")
                {
                    BarChart.DataSource.AddCategory(key, MaterialColorArray[idx]);

                    int monthsSoldCarBrand = json[key].Count;
                    for (var i = 0; i < monthsSoldCarBrand; i++) // By Month
                    {
                        var month = json[key][i]["month"].ToString();
                        var sales = (long)Convert.ToDouble(json[key][i]["sales"].ToString());
                        try { BarChart.DataSource.AddGroup(month); }
                        catch (Exception e) { }
                        BarChart.DataSource.SlideValue(key, month, sales, 3);
                    }
                }
                idx++;
            }
        });
    }

    void ClearBarChart()
    {
        BarChart.DataSource.ClearCategories();
        BarChart.DataSource.ClearGroups();
        BarChart.DataSource.ClearValues();
    }

   

    // Update is called once per frame
    void Update () {}

    
}
