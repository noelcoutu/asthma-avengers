using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Throat : MonoBehaviour
{
    public Egg egg;
    public Egg superEgg;
    Vector2 direction;
    public bool isTonsil;
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    public float shootTimer = 0f;
    public float delayTimer = 0f;
    private GameObject duck;

    // Start is called before the first frame update
    void Start()
    {
        duck = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTonsil && duck)
        {
            Vector2 directionToPlayer = duck.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }
        direction = (transform.rotation * Vector2.right).normalized;
        //if(autoShoot) 
        //{
        //    if(delayTimer >= shootDelaySeconds)
        //    {
        //        if(shootTimer >= shootIntervalSeconds)
        //        {
        //            Shoot();
        //            shootTimer = 0;
        //        }
        //        else
        //        {
        //            shootTimer += Time.deltaTime;
        //        }
        //    }
        //    else
        //    {
        //        delayTimer += Time.deltaTime;
        //    }
        //}
    }

    public void Shoot()
    {
        GameObject go = Instantiate(egg.gameObject, transform.position, Quaternion.identity);
        Egg goEgg= go.GetComponent<Egg>();
        goEgg.direction= direction;
        Vector3 lookDirection = direction.normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        // Apply the rotation to the instantiated egg
        go.transform.rotation = rotation;
    }

    public void SuperShoot()
    {
        GameObject go = Instantiate(superEgg.gameObject, transform.position, Quaternion.identity);
        Egg goEgg = go.GetComponent<Egg>();
        goEgg.direction = direction;
        Vector3 lookDirection = direction.normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        // Apply the rotation to the instantiated egg
        go.transform.rotation = rotation;
    }
}
