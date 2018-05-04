using System.Collections;
using System; 
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using ChartAndGraph;

 
public class customSocketIO : MonoBehaviour
{
    Dictionary<string, string> BrandSalesDictionary;
    PieChart PieChart;
    Material[] MaterialColorsList;

    // Use this for initialization
    void Start()
    {
       // GameObject go = GameObject.Find("SocketIO");
       // SocketIOComponent socket = go.GetComponent<SocketIOComponent>();
        PieChart = GameObject.Find("PieChart").GetComponent<PieChart>();

        /*socket.On("connect", (SocketIOEvent e) =>
        {
            if (PieChart)
            {
                Debug.Log("HERE");
                PieChart.DataSource.SetValue("Brand1", 200);
            } 
                

        });

        // need to call this pie chart later
       // socket.On("data", (SocketIOEvent e) =>
        {
            var foo = GameObject.Find("Material1").GetComponent<Renderer>().material;
            PieChart.DataSource.AddCategory("foo", foo);
            PieChart.DataSource.SetValue("foo", 5000);
          //  this.UpdatePieChart(e.data);
        });*/
    }

    // Update is called once per frame
    void Update(){}


}
