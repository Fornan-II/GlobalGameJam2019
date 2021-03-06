﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ToggleChildren : ElementSensitive
{
    public override void OnElementTouch()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(!child.activeSelf);
        }
    }
}