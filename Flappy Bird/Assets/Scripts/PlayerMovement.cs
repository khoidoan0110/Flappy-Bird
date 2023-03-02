using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float gravity = -9.8f;
    private bool isColliding = false;

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, direction.y * 5f);

        if (IsCollidingWithPipe())
    {
        if (!isColliding)
        {
            isColliding = true;
            Debug.Log("Collision detected!");
            // Play sound effect
        }
    }
    else
    {
        isColliding = false;
    }
    }

    private bool IsCollidingWithPipe()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            Transform upperPipe = pipe.transform.Find("UpperPipe");
            Transform lowerPipe = pipe.transform.Find("LowerPipe");
            if (upperPipe != null && lowerPipe != null)
            {
                // Get the position and size of the upper and lower pipes
                Vector3 upperPipePosition = upperPipe.position;
                Vector3 lowerPipePosition = lowerPipe.position;
                float pipeWidth = upperPipe.GetComponent<SpriteRenderer>().bounds.size.x;

                // Check if the bird is within the x bounds of the pipe
                if (transform.position.x > upperPipePosition.x - pipeWidth / 2f && transform.position.x < upperPipePosition.x + pipeWidth / 2f)
                {
                    // Check if the bird's y position is within the height of the upper or lower pipe
                    float upperPipeHeight = upperPipe.GetComponent<SpriteRenderer>().bounds.size.y;
                    float lowerPipeHeight = lowerPipe.GetComponent<SpriteRenderer>().bounds.size.y;
                    if (transform.position.y > upperPipePosition.y - upperPipeHeight / 2f || transform.position.y < lowerPipePosition.y + lowerPipeHeight / 2f)
                    {
                        // If the bird is inside the pipe, it's colliding with it
                        return true;
                    }
                }
            }
        }
        return false;
    }

    

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * 5f;
        }

        Flap();
    }

    private void Flap()
    {
        //v = gt
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }
}
