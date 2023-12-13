using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Duck : MonoBehaviour
{
    Throat[] throats;

    float moveSpeed = 3.5f;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool shoot;

    PlayerHealthController playerHealthController;

    private bool isCharging = false;
    private float chargeDuration = 1.0f;
    private float currentChargeTime =  0.0f;
    private bool isCharged = false;
    private float shootingDuration = 0.1f; // Adjust this to control how long the shooting sprite is displayed.
    private float shootingTimer = 0f;

    private float quackDuration = 0.1f; // Adjust this to control how long the shooting sprite is displayed.
    private float quackTimer = 0f;

    public AudioSource audioSource;
    public AudioClip quack;
    public AudioClip quackHigh;
    public AudioClip quackLow;
    public AudioClip pause;
    public AudioClip charge;

    private SpriteRenderer sprite;
    public Sprite[] sprites;

    bool paused;

    bool chargeSoundPlaying;

    // Start is called before the first frame update
    void Start()
    {
        sprite= GameObject.FindWithTag("Throat").GetComponent<SpriteRenderer>();
        throats = transform.GetComponentsInChildren<Throat>();
        playerHealthController = GameObject.FindWithTag("PlayerHealthContainer").GetComponent<PlayerHealthController>();
        paused = false;
        chargeSoundPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetAxis("Vertical") >0f ? true : false;
        moveDown= Input.GetAxis("Vertical") < 0f ? true : false;
        moveLeft = Input.GetAxis("Horizontal") < 0f ? true : false;
        moveRight = Input.GetAxis("Horizontal") > 0f ? true : false;
        sprite.sprite = sprites[0];
        shootingTimer += Time.deltaTime;
        quackTimer += Time.deltaTime;

        // If the timer exceeds the desired duration, switch back to the idle sprite.
        if (shootingTimer <= shootingDuration)
        {
            sprite.sprite = sprites[1];

        }
        if (quackTimer <= quackDuration)
        {
            sprite.sprite = sprites[1];

        }
        if (Input.GetButtonDown("Fire1"))
        {
            // Start charging when the spacebar is initially pressed
            shoot = true;
            isCharging = true;
        }
        else if (Input.GetButton("Fire1") && isCharging)
        {
            // Continue charging as long as the spacebar is held down
            currentChargeTime += Time.deltaTime;

            if (!chargeSoundPlaying)
            {
                audioSource.clip = charge;
                audioSource.Play();
            }
            chargeSoundPlaying= true;
            // Check if charging is complete
            if (currentChargeTime >= chargeDuration)
            {
                audioSource.Stop();
                isCharged = true;
                sprite.sprite = sprites[2];
            }
        }
        else if (Input.GetButtonUp("Fire1") && isCharged)
        {
            // Release the charged shot when the spacebar is released
            isCharging = false;
            isCharged = false;
            currentChargeTime = 0.0f;
            shootingTimer = 0.0f;
            audioSource.Stop();
            chargeSoundPlaying = false;
            // Call the superShoot() function here
            foreach (Throat throat in throats)
            {
                Debug.Log("SuperShooteeeddd");

                throat.SuperShoot();
            }
        }
        else if (Input.GetButtonUp("Fire1") && !isCharged)
        {
            // Reset charging if the spacebar is released before the charge duration
            isCharging = false;
            currentChargeTime = 0.0f;
            shootingTimer= 0.0f;
            audioSource.Stop();
            chargeSoundPlaying = false;

            if (shoot )
            {
                Debug.Log("Shooted");
                

                
                shoot = false;
                foreach (Throat throat in throats)
                {
                    throat.Shoot();
                }
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            quackTimer = 0.0f;
            audioSource.PlayOneShot(quackHigh);
            sprite.sprite = sprites[1];
        }

        else if (Input.GetButtonDown("Fire3"))
        {
            quackTimer = 0.0f;
            audioSource.PlayOneShot(quack);
            sprite.sprite = sprites[1];
        }

        else if (Input.GetButtonDown("Fire2"))
        {
            quackTimer = 0.0f;
            audioSource.PlayOneShot(quackLow);
            sprite.sprite = sprites[1];
        }
        else if (Input.GetButtonDown("Cancel") && !paused)
        {
            Time.timeScale = 0;
            audioSource.PlayOneShot(pause);
            paused = true;
        }
        else if (Input.GetButtonDown("Cancel") && paused)
        {
            Time.timeScale = 1;
            paused= false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }
        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount) 
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        pos += move;

        if(pos.x<=1.5f)
        {
            pos.x = 1.5f;
        }
        if(pos.x >= 16f)
        {
            pos.x = 16f;
        }
        if (pos.y <= 1f)
        {
            pos.y = 1f;
        }
        if (pos.y >= 9f)
        {
            pos.y = 9f;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Egg egg  = collision.gameObject.GetComponent<Egg>();
        if (egg != null)
        {
            if (egg.isEnemy)
            {
                Destroy(egg.gameObject);
                playerHealthController.RemoveHealth();
            }
            return;
        }

        Damageable damageable =  collision.gameObject.GetComponent<Damageable>();
        if(damageable!=null)
        {
            Destroy(damageable.gameObject);
            playerHealthController.RemoveHealth();
        }
    }
}
