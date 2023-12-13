using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public Vector2 direction = new Vector2(1,0);
    public float speed = 8;

    public Vector2 velocity;

    public bool isEnemy = false;
    public bool isSuper = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction* speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
