﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class A_PaintPalette : MonoBehaviour {

    #region variables
    //paint Brush
    [Tooltip("Attach the [Paint_Brush] script!")]
    public GameObject PaintBrush;
    A_PaintBrush paintBrush;
    [Space(10)]

    [Header("Initial brush options:")]
    //material
    [Tooltip("Choose a material for the brush or leave blank to select it on the palette")]
    public Material material;

    //Color
    [Tooltip("Choose a color for the brush or leave blank to select it on the palette")]
    public Color color;

    //sizes
    [Tooltip("Choose a size for the brush or leave blank to select it on the palette")]
    public float brushSize = 0.1f;

    [Tooltip("Choose a type of brush or leave blank to select it on the palette")]
    public GameObject paintBrushType;
    
    //ray pointer
    GameObject rayPointer;
    [HideInInspector]
    public RayPointer ray;
    [Space(10)]

    //straight line   
    [Header("Straight Lines options:")]
    public bool straightLines;

    // freezing positions and rotations
    Vector3 initialPaintContactPos;
    public bool drawStraightOnX;
    public bool drawStraightOnY;
    public bool drawStraightOnZ;
    public bool StraightEnds;

    //density 
    [Tooltip("this is the distance between each new face being extruded. Use higher value for performance optimization or for drawing in straight lines")]
    [Range(0.01f, 1f)]
    public float paintDensity = 0.01f;

    //rotate brush toggle
    [HideInInspector]
    public bool rotateBrush;

    //pressure sensitivity
    [Space(10)]
    public bool PressureSensitivity;
    #endregion

    void Awake()
    {
        if (PaintBrush == null)
        {
            try { PaintBrush = GameObject.Find("[Paint_Brush]"); paintBrush = PaintBrush.GetComponent<A_PaintBrush>();  }
            catch (Exception e) { print("please attach the [Paint_Brush] object in the inspector"); };
        }
        else
        {
            paintBrush = PaintBrush.GetComponent<A_PaintBrush>();
            //get raypoint script
            try { ray = PaintBrush.transform.GetChild(0).GetComponent<RayPointer>(); }
            catch (Exception e) { print("ray pointer script doesnt exist or has been moved?"); };
        }
        
    }

    //incoming brush selections
    public void selectPaintBrush(GameObject incomingBrush)
    {
        paintBrushType = incomingBrush;
        paintBrush.activeBrush = paintBrushType;
        paintBrush.BrushSelection();
    }

    //incoming color slections
    public void selectColor(Color incomingColor)
    {
        color = incomingColor;
        paintBrush.color = color;
        paintBrush.ColorSelection(color);
    }

    //incoming material selections
    public void selectMaterial(Material incomingMaterial)
    {
        material = incomingMaterial;
        paintBrush.MaterialSelection(incomingMaterial);
    }

    //incoming size selections
    public void selectSize(float incomingSize, string scaleTarget)
    {
        if (scaleTarget == "XYZ")
        {
            brushSize = incomingSize;
            paintBrush.SizeSelection(incomingSize, "XYZ");
        }
        else if (scaleTarget == "X")
        {
            brushSize = incomingSize;
            paintBrush.SizeSelection(incomingSize, "X");
        }
        else if (scaleTarget == "Y")
        {
            brushSize = incomingSize;
            paintBrush.SizeSelection(incomingSize, "Y");
        }
        else if (scaleTarget == "Z")
        {
            brushSize = incomingSize;
            paintBrush.SizeSelection(incomingSize, "Z");
        }
    }

    //incoming density selections
    public void selectDensity(float incomingDensity)
    {
        paintDensity = incomingDensity;
        paintBrush.DensitySelection(incomingDensity);
    }

    public void RotateBrushSelection()
    {
        paintBrush.RotateBrushSelection();
    }

    public void FreezePositions(string axis)
    {
        paintBrush.FreezePositions(axis);
    }

    public void FreezeRotations()
    {
        paintBrush.FreezeRotations();
    }

    public void StraightLines()
    {
        //print("toggled straight lines");
        if (straightLines)
        {
            straightLines = false;
            paintBrush.StraightLines();
        }
        else
        {
            straightLines = true;
            paintBrush.StraightLines();
        }
    }
}