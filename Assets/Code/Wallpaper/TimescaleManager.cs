using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleManager : MonoBehaviour
{
    public float timescale = 1f;
    void Start()
    {
        Time.timeScale = timescale;
    }

    private void Update()
    {
        Time.timeScale = timescale;
    }
}
