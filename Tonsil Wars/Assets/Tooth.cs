using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    public float initialXOffset = 5f;
    public Transform spawnPoint;
    public float requiredTimeInside = 3.0f; // Time in seconds
    private float elapsedTimeInside = 0.0f;
    private bool isInside = false;
    public float moveSpeed = 5f;
    public float waitTime = 0f;

    private PlayerHealthController playerHealthController;

    private void Start()
    {
        playerHealthController= GameObject.FindGameObjectWithTag("PlayerHealthContainer").GetComponent<PlayerHealthController>();
        StartCoroutine(SpawnAndMove());
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // You can use a specific tag to identify the object you want to detect.
        {
            isInside = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
            elapsedTimeInside = 0.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            elapsedTimeInside += Time.deltaTime;

            if (elapsedTimeInside >= requiredTimeInside)
            {
                // Trigger the event or execute the desired action here.
                Debug.Log("Object has been inside for 3 seconds!");
                Damage();
            }
        }
        transform.Rotate(Vector3.forward * 10 * Time.deltaTime);

    }

    private void Damage()
    {
        if (playerHealthController != null)
        {
            playerHealthController.RemoveHealth();
            elapsedTimeInside = 0.0f;
        }
    }

    IEnumerator SpawnAndMove()
    {
        float storePos = spawnPoint.position.y;
        float targetY = spawnPoint.position.y + initialXOffset;
        Vector2 targetPosition = new Vector2(transform.position.x, targetY);
        yield return new WaitForSeconds(waitTime); // Wait for the initial delay
        while (true)
        {
            //capture and calculate variables

            Debug.Log("TargetY " + targetY);
            Debug.Log("current Y " + transform.position.y);
            //move outward
            while (spawnPoint.position.y < targetY)
            {
                spawnPoint.position = Vector2.MoveTowards(spawnPoint.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(10f);
            Vector2 newTargetPosition = new Vector2(transform.position.x, storePos);
            while (spawnPoint.position.y > newTargetPosition.y)
            {
                spawnPoint.position = Vector2.MoveTowards(spawnPoint.position, newTargetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
