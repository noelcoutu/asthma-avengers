using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public string moveType = "LeftRight";
    public float moveSpeed = 5;
    float sinCenterY;
    public float amplitude = 2;
    public float frequency = .5f;
    public float rotationDuration = 1.0f;


    public bool inverted = true;
    public float initialStraightDuration = 2f; // Duration of initial straight movement
    private float rotationTimer = 0f;
    public Transform player; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Destroy(gameObject, 10);
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (moveType == "LeftRight")
        {
            Vector2 pos = transform.position;

            pos.x -= moveSpeed * Time.fixedDeltaTime;

            transform.position = pos;
        }
        else if (moveType == "UvulaSpitter")
        {
            Vector2 pos = transform.position;

            pos.x -= moveSpeed * Time.fixedDeltaTime;

            transform.position = pos;
            rotationTimer += Time.deltaTime;

            // Check if it's time to rotate
            if (rotationTimer >= rotationDuration)
            {
                // Rotate the object by 90 degrees
                transform.Rotate(Vector3.forward * 90f);

                // Reset the timer
                rotationTimer = 0f;
            }
        }
        else if (moveType == "Sin")
        {
            Vector2 pos = transform.position;
            float sin = Mathf.Sin(pos.x * frequency) * amplitude;
            if (inverted)
            {
                sin *= -1;
            }
            pos.x -= moveSpeed * Time.fixedDeltaTime;
            pos.y = sinCenterY + sin;
            transform.position = pos;
        } else if(moveType == "Follow")
        {
            // Calculate the direction from the moving object to the player.
            Vector3 directionToPlayer = player.position - transform.position;

            // Normalize the direction vector to make the movement smooth.
            directionToPlayer.Normalize();

            // Move the object towards the player.
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
        }
    }
}
