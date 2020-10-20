﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialLabelManager : MonoBehaviour
{
    private GameObject value;

    // Start is called before the first frame update
    void Start()
    {
        value = GetComponentInParent<Transform>().Find("Value").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setMaterial(MAT_TYPE material)
    {
        switch (material)
        {
            case MAT_TYPE.WOOD:
                value.GetComponent<UnityEngine.UI.Text>().text = "wood";
                break;
            case MAT_TYPE.GLASS:
                value.GetComponent<UnityEngine.UI.Text>().text = "brick";
                break;
            case MAT_TYPE.STONE:
                value.GetComponent<UnityEngine.UI.Text>().text = "stone";
                break;
            default:
                value.GetComponent<UnityEngine.UI.Text>().text = "error";
                break;
        }

    }
}