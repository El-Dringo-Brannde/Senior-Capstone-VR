using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// My first C# class... Could be configured to do logic in constructor
public class MaterialBuilder
{
    public Material[] MaterialColorArray = new Material[10];

    public void BuildMaterials()
    {
        var idx = 0;
        var Materials = GameObject.FindGameObjectsWithTag("Materials");
        foreach (var material in Materials)
        {
            MaterialColorArray[idx] = material.GetComponent<Renderer>().material;
            idx++;
        }
    }
}