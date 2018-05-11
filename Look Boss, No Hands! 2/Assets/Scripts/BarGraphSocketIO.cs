using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketIO;
using ChartAndGraph;
using JSONTypes;


// Class component used to pull data from the websocket and populate the bar charts
public class BarGraphSocketIO : MonoBehaviour {
    public BarChart BarChart;
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
        socket.On("Bar_Chart", HandleData);
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

    void HandleData(SocketIOEvent obj)
    {
        if(obj.data.Count != 1)
            ClearBarChart();
        BarChart.DataSource.AutomaticMaxValue = true;
        BarChart.DataSource.AutomaticMinValue = true;
        var json = obj.data;
        string[] KeyArray = new string[100];
        KeyArray = json.keys.ToArray();
        int idx = 0;
        foreach (var key in KeyArray) // By Brand
        {
            if (key != "user")
            {
                BarChart.DataSource.AddCategory(key, MaterialColorArray[idx]);

                int monthsSoldCarBrand = json[key].Count;
                for (var i = 0; i < monthsSoldCarBrand; i++) // By Month
                {
                    var month = json[key][i]["month"].ToString();
                    var sales = (long)Convert.ToDouble(json[key][i]["sales"].ToString());
                    try { BarChart.DataSource.AddGroup(month.Substring(0, 4)); }
                    catch (Exception e) { }
                    BarChart.DataSource.SlideValue(key, month.Substring(0, 4), sales, 3);
                }
            }
            idx++;
        }
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
