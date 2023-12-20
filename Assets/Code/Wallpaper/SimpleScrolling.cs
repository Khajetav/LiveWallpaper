using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScrolling : MonoBehaviour
{
    [SerializeField] private RawImage Sky;
    [SerializeField] private RawImage Floor;
    [SerializeField] private RawImage Fog;
    private float SkyScrollingSpeed=0.0025f;
    private float FloorScrollingSpeed=0.01f;
    private float FogScrollingSpeed=0.005f;
    void Update()
    {
        Sky.uvRect = new Rect(Sky.uvRect.position + new Vector2(SkyScrollingSpeed, 0) * Time.deltaTime, Sky.uvRect.size);
        Floor.uvRect = new Rect(Floor.uvRect.position + new Vector2(FloorScrollingSpeed, 0) * Time.deltaTime, Floor.uvRect.size);
        Fog.uvRect = new Rect(Fog.uvRect.position + new Vector2(FogScrollingSpeed, 0) * Time.deltaTime, Floor.uvRect.size);
    }
}
