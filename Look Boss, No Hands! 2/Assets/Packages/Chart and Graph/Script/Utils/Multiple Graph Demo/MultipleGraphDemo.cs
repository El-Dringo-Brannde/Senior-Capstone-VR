using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;

public class MultipleGraphDemo : MonoBehaviour
{

    public GraphChart Graph;
    public GraphAnimation Animation;
    public int TotalPoints = 5;

    void Start()
    {
        if (Graph == null) // the ChartGraph info is obtained via the inspector
            return;
        List<Vector2> animationPoints = new List<Vector2>();
        float x = 0f; 

        Graph.DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        Graph.DataSource.ClearCategory("Player 1"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
        Graph.DataSource.ClearAndMakeBezierCurve("Player 2"); // clear the "Player 2" category. this category is defined using the GraphChart inspector

        for (int i = 0; i < TotalPoints; i++)  //add random points to the graph
        {   
            Graph.DataSource.AddPointToCategory("Player 1", System.DateTime.Now + System.TimeSpan.FromHours(x), Random.value * 20f + 20f); // each time we call AddPointToCategory 
            DoubleVector2 point = new DoubleVector2(ChartDateUtility.DateToValue(System.DateTime.Now + System.TimeSpan.FromHours(x)), Random.value * 20f );
            if (i == 0)
                Graph.DataSource.SetCurveInitialPoint("Player 2", point.x,point.y);
            else
                Graph.DataSource.AddLinearCurveToCategory("Player 2", point); // each time we call AddPointToCategory 
            animationPoints.Add(new Vector2(x, Random.value * 10f));
            //            Graph.DataSource.AddPointToCategory("Player 2", x, Random.value * 10f); // each time we call AddPointToCategory 
            x +=  1f;

        }
        Graph.DataSource.MakeCurveCategorySmooth("Player 2");
        Graph.DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        if (Animation != null)
        {
          //  Animation.Animate("Player 2",animationPoints,3f);
        }
    }
}
