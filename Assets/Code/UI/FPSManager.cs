using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    public int targetFps = 30;
    private void Start()
    {
        Application.targetFrameRate = targetFps;
    }
}
