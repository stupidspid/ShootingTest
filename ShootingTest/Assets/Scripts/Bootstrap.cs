using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 120;
#endif
    }
}
