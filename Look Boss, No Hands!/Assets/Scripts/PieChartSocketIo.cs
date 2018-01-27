using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using SocketIO;
using ChartAndGraph;



public class PieChartSocketIo : MonoBehaviour
{

    PieChart PieChart;
    SocketIOComponent socket;
    Dictionary<string, string> BrandSalesDictionary;
    Material[] MaterialColorArray = new Material[10];

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        PieChart = GameObject.Find("PieChart").GetComponent<WorldSpacePieChart>();

        MaterialBuilder Mat = new MaterialBuilder();
        Mat.BuildMaterials();
        MaterialColorArray = Mat.MaterialColorArray;
        SocketIOConnection();
        HandleData();
    }

   

    void SocketIOConnection()
    {
        socket.On("connect", (SocketIOEvent e) =>
        {
            if (PieChart)
            {
                Debug.Log("Connected Successfully!");
            }
        });
    }

    void HandleData()
    {
        socket.On("Pie_Chart", (SocketIOEvent obj) => // change data to piechart later
        {
            this.BrandSalesDictionary = obj.data.ToDictionary();
            int idx = 0;
            PieChart.DataSource.Clear();
            

            foreach (var item in this.BrandSalesDictionary)
            {
                if(item.Key != "user")
                {
                    PieChart.DataSource.AddCategory(item.Key, MaterialColorArray[idx]);
                    PieChart.DataSource.SetValue(item.Key, Convert.ToDouble(item.Value));
                    idx++;
                }
                
            }
        });
    }

    // Update is called once per frame
    void Update() { }
}

