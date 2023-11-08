using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private List<Transform> backgroundList;
    [SerializeField] private List<Transform> middlegroundList;
    [SerializeField] private List<Transform> foregroundList;
    [SerializeField] private List<Transform> accessoryList;
    private float scrollSpeedBackground = 0.2f;
    private float scrollSpeedMiddleground = 0.35f;
    private float scrollSpeedForeground = 0.5f;
    private Vector2 leftSide;
    private Vector2 rightSide;

    private void Start()
    {
        // 0 - left 
        // 1 - middle
        // 2 - right
        leftSide = backgroundList[0].position;
        rightSide = backgroundList[2].position;
    }

    private void Update()
    {
        foreach (var background in backgroundList)
        {
            background.Translate(Vector3.left * scrollSpeedBackground * Time.deltaTime);
            if (background.position.x <= leftSide.x)
            {
                background.position = rightSide;
            }
        }
        foreach (var accessory in accessoryList)
        {
            accessory.Translate(Vector3.left * scrollSpeedBackground * Time.deltaTime);
            if (accessory.position.x <= leftSide.x)
            {
                accessory.position = rightSide;
            }
        }
        foreach (var middleground in middlegroundList)
        {
            middleground.Translate(Vector3.left * scrollSpeedMiddleground * Time.deltaTime);
            if (middleground.position.x <= leftSide.x)
            {
                middleground.position = rightSide;
            }
        }
        foreach (var foreground in foregroundList)
        {
            foreground.Translate(Vector3.left * scrollSpeedForeground * Time.deltaTime);
            if (foreground.position.x <= leftSide.x)
            {
                foreground.position = rightSide;
            }
        }
    }
}
