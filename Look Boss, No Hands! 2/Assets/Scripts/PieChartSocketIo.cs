using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using SocketIO;
using ChartAndGraph;


/* 
 * Component used to listen on the websocket and populate the 
 * pie charts when data on their channel comes through. 
 */
public class PieChartSocketIo : MonoBehaviour
{

    public PieChart PieChart;
    public SocketIOComponent socket;
    Dictionary<string, string> BrandSalesDictionary;
    Material[] MaterialColorArray = new Material[10];
    MaterialBuilder Mat = new MaterialBuilder();

    // Use this for initialization
    void Start()
    { 
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
                Debug.Log("Wrld Chart!");
            }
        });
    }

    void HandleData()
    {
        socket.On("Pie_Chart", (SocketIOEvent obj) => 
        {
            this.BrandSalesDictionary = obj.data.ToDictionary();
            if(BrandSalesDictionary.Count != 1)
                PieChart.DataSource.Clear();
            int i = 0;
            foreach (var item in this.BrandSalesDictionary)
            {
                if(item.Key != "user")
                {
                    try
                    { 
                        PieChart.DataSource.AddCategory(item.Key, Mat.FindColor(item.Key, i));

                        PieChart.DataSource.SetValue(item.Key, Convert.ToDouble(item.Value));
                        i++;
                    } catch(Exception err)
                    {
                        Debug.Log(err);
                    }
                }
            }
        });
    }

    // Update is called once per frame
    void Update() { }
}

