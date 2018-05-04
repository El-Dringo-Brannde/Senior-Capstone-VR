using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketIO;
using ChartAndGraph;
using System.Globalization;

public class LineGraphSocketIO : MonoBehaviour {
    public GraphChartBase LineChart;
    private SocketIOComponent socket;
    Material[] MaterialColorArray = new Material[10];

	// Use this for initialization
	void Start () {
        var Mat = new MaterialBuilder();
        Mat.BuildMaterials();
        MaterialColorArray = Mat.MaterialColorArray;
        //SocketIOConnect();
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        HandleData();
        
    }

    void HandleData()
    {
        socket.On("Bar_Chart", (SocketIOEvent obj) =>
        {
            var json = obj.data;
            string[] KeyArray = new string[100];
            KeyArray = json.keys.ToArray();
            int idx = 0;
            int nxtMat = 0;

            List<string> cats = new List<string>();
            foreach (var key in KeyArray) // By Brand
            {
                if (key != "user")
                {
                    //format brand name nicely
                    string brand = key.Substring(0, 1).ToString().ToUpper() + key.Substring(1).ToString().ToLower();
                    
                    cats.Add(brand);

                    if (!LineChart.DataSource.HasCategory(brand))
                    {
                        if (LineChart.DataSource.HasCategory("colora"))
                        {
                            LineChart.DataSource.RenameCategory("colora", brand);
                        }
                        else if(LineChart.DataSource.HasCategory("colorb"))
                        {
                            LineChart.DataSource.RenameCategory("colorb", brand);
                        }
                        else
                        {
                            Debug.Log("Missing extra categories");
                        }
                    }

                    int monthsSoldCarBrand = json[key].Count;
                    for (var i = 0; i < monthsSoldCarBrand; i++) // By Month
                    {
                        string monthStr = json[key][i]["month"].ToString();
                        monthStr = monthStr.Trim('"');
                        monthStr = monthStr.Trim('/');
                        Double month = (Double) Convert.ToDateTime("01-" + monthStr + "-2018").Month;
                        Double sales = Convert.ToDouble(json[key][i]["sales"].ToString());
                        try { LineChart.DataSource.AddPointToCategory(cats[idx], month, sales); }
                        catch (Exception e) { Debug.Log(e); }
                    }
                    idx++;
                }
                
            }
        });
    }
	
	// Update is called once per frame
	void Update () {

	}
}
