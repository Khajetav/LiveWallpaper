using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScrolling : MonoBehaviour
{
    // Static scrolling logic
    [SerializeField] private RawImage Sky;
    [SerializeField] private RawImage Floor;
    private float SkyScrollingSpeed=0.02f;
    private float FloorScrollingSpeed=0.10f;
    // Update is called once per frame
    void Update()
    {
        Sky.uvRect = new Rect(Sky.uvRect.position + new Vector2(SkyScrollingSpeed, 0) * Time.deltaTime, Sky.uvRect.size);
        Floor.uvRect = new Rect(Floor.uvRect.position + new Vector2(FloorScrollingSpeed, 0) * Time.deltaTime, Floor.uvRect.size);
    }
}
