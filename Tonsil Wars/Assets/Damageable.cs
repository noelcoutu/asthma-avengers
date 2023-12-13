using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{

    public int scoreValue = 100;
    public bool breakable = false;
    public GameObject scoreText;
    ScoreScript scoreScript;
    public float splitForce = 2f;
    public GameObject splitStone;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scoreText = GameObject.FindWithTag("Score");
        scoreScript = scoreText.GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Egg egg = collision.GetComponent<Egg>();
        if (egg != null && !egg.isEnemy)
        {
            if(breakable && !egg.isSuper)
            {
                // Create four smaller eggs
                for (int i = 0; i < 4; i++)
                {
                    GameObject smallerEgg = Instantiate(splitStone, transform.position, Quaternion.identity);
                    Rigidbody2D smallerEggRb = smallerEgg.GetComponent<Rigidbody2D>();

                    // Rotate the base direction by the deviation angle
                    Vector2 rotatedDirection = UnityEngine.Random.insideUnitCircle.normalized;


                    // Calculate the direction to move each smaller egg
                    // Apply an impulse force to the smaller egg in the calculated direction
                    smallerEggRb.velocity = rotatedDirection * splitForce;
                }
            }
            // Destroy the original egg
            Destroy(egg.gameObject);
            if(animator!= null)
            {
                animator.SetBool("Destroy", true);
                Destroy(gameObject, .25f);
            }
            else
            {
                Destroy(gameObject);
            }
            
            scoreScript.AddScore(scoreValue);
        }
    }
}
