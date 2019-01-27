using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : ElementSensitive
{
    public GameObject EndScreen;

    public override void OnElementTouch()
    {
        EndScreen.SetActive(true);
    }
}
