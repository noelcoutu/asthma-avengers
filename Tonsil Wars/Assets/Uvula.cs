using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Uvula : MonoBehaviour
{

    public GameObject[] enemy;
    public float moveSpeed = 5f;
    public float spawnDelay = 1.0f;
    public float initialXOffset = 5f;
    public Transform spawnPoint;
    public Transform headSpawn;
    public float howManyAttacks;
    public float waitTime = 5.0f; // Adjust the delay time as needed
    public SpriteRenderer sprite;
    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAndMove());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move()
    {
        float targetX = spawnPoint.position.x - initialXOffset;
        Vector2 targetPosition = new Vector2(targetX, transform.position.y);
        //Debug.Log("Target X " + targetX);

        if (targetPosition.x <= targetX)
        {
            //Debug.Log("CoRoutine Started");
            StartCoroutine(SpawnAndMove());
        }
    }


    IEnumerator SpawnAndMove()
    {
        float storePos = spawnPoint.position.x;
        float targetX = spawnPoint.position.x - initialXOffset;
        Vector2 targetPosition = new Vector2(targetX, transform.position.y);
        yield return new WaitForSeconds(waitTime); // Wait for the initial delay
        while (true)
        {
            //capture and calculate variables

            Debug.Log("TargetX "+ targetX);
            Debug.Log("current x " + transform.position.x);
            //move outward
            while (transform.position.x > targetX)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            determineAndAttack();

            yield return new WaitForSeconds(0.25f);
            //wait 1 second again 
            sprite.sprite = sprites[0];
            yield return new WaitForSeconds(spawnDelay);
            determineAndAttack();
            
            //move back inward
            yield return new WaitForSeconds(0.25f);
            sprite.sprite = sprites[0];
            Vector2 newTargetPosition = new Vector2(storePos, transform.position.y);
            while (transform.position.x < newTargetPosition.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, newTargetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void determineAndAttack()
    {
        int randomInt = UnityEngine.Random.Range(0, enemy.Length);
        Instantiate(enemy[randomInt], headSpawn.position, Quaternion.identity);
        sprite.sprite = sprites[1];
    }
}
