using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetWalking : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public float speed = 50f;
    // just two empties
    public Transform leftBoundary;
    public Transform rightBoundary;
    // -1 = go left
    // 1 = go right
    private float direction = 1;

    private float changeDirectionTime = 0f;
    private int directionChangeMinTime = 2;
    private int directionChangeMaxTime = 10;
    private void Update()
    {
        // RANDOM DIRECTION CHANGE
        if (Time.time >= changeDirectionTime)
        {
            // -1 = go left
            // 1 = go right
            direction = Random.Range(0, 2) == 0 ? -1 : 1;
            changeDirectionTime = Time.time + Random.Range(directionChangeMinTime, directionChangeMaxTime);
        }

        // MOVEMENT
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // BOUNDS
        // just bounces between the two empties lol
        if (transform.position.x <= leftBoundary.position.x && direction < 0 || transform.position.x >= rightBoundary.position.x && direction > 0)
        {
            direction *= -1; // Change direction
        }
        if (direction > 0 && transform.localScale.x < 0 || direction < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // CLAMP
        // prevent going out of bounds
        float clampedX = Mathf.Clamp(transform.position.x, leftBoundary.position.x, rightBoundary.position.x);
        transform.position = new Vector2(clampedX, transform.position.y);

        // ANIMATION
        // is handled for now by the animation controller
    }
}
