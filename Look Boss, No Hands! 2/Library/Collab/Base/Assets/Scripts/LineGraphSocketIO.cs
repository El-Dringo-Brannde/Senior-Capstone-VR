using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketIO;
using ChartAndGraph; 

public class LineGraphSocketIO : MonoBehaviour {
    public GraphChartBase LineChart;
    public SocketIOComponent socket;
    Material[] MaterialColorArray = new Material[10]; 


	// Use this for initialization
	void Start () {
        var Mat = new MaterialBuilder();
        Mat.BuildMaterials();
        MaterialColorArray = Mat.MaterialColorArray;
        SocketIOConnect();
        HandleData();
    }

    void SocketIOConnect()
    {
        socket.On("connect", (SocketIOEvent e) =>
        {
            if (LineChart)
            {
                Debug.Log("Connected from Line Chart");
            }
        });
    }

    void HandleData()
    {
        socket.On("Bubble_Chart", (SocketIOEvent obj) =>
        {
            var json = obj.data;
            string[] KeyArray = new string[100];
            KeyArray = json.keys.ToArray();
            int idx = 0;
            int nxtMat = 0;

            GameObject LinePrefab = Instantiate(Resources.Load("BoxLine", typeof(GameObject))) as GameObject;

            //string[] cats = { "Player 1", "Player 2", "Player 3", "Player 4", "Player 5" };
            List<string> cats = new List<string>();
            foreach (var key in KeyArray) // By Brand
            {
                if (key != "user")
                {
                    //format brand name nicely
                    string brand = key.Substring(0, 1).ToString().ToUpper() + key.Substring(1).ToString().ToLower();

                    cats.Add(brand);
                    LineChart.DataSource.AddCategory(brand, MaterialColorArray[nxtMat], 0.2, new MaterialTiling(), MaterialColorArray[nxtMat], false, null, 0.75);
                    nxtMat++;

                    int monthsSoldCarBrand = json[key].Count;
                    for (var i = 0; i < monthsSoldCarBrand; i++) // By Month
                    {
                        var month = (long)Convert.ToDouble(json[key][i]["month"].ToString());
                        var sales = (long)Convert.ToDouble(json[key][i]["sales"].ToString());
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
