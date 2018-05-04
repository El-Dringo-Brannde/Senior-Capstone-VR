using System;
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

    public Material FindColor(string color, int idx)
    {
        //Make sure the color input is in the proper format, e.g. change to Red if given red
        color = color.Substring(0, 1).ToString().ToUpper() + color.Substring(1).ToString().ToLower();

        //Add the proper ending for the materials
        string matName = color + "Material";
        Material f = null;
        try
        {
            f = GameObject.Find(matName).GetComponent<Renderer>().material;
        } catch(Exception err) { 
            return MaterialColorArray[idx];   
        }
        //Return the proper material from the gameobject
        return GameObject.Find(matName).GetComponent<Renderer>().material;
    }
}