using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 100.0f;
    public float lastEnemyDefeatedTime;
    private Animator playerAnim;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    public AudioClip explosionClip;
    public AudioClip healthItemClip;
    public AudioClip attackClip;
    public AudioClip pickupItemClip;
    private float attackClipDelayTime = 1.0f;
    private float playTime;

    private AudioSource audioSource;

    public int health;
    public int points;
    private int attackStrength = 5;
    private int maxHealth = 100;


    public bool gameOver;
    public bool isGameActive;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
        audioSource = GetComponent<AudioSource>();
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive)
        {
          MovePlayer();
          PlayerAttack();
        }

        if(health == 0){
            //Debug.Log("Game Over!");
            GameOver();
        }
    }

    void PlayerAttack()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            playerAnim.SetTrigger("attack");
            float currentTime = Time.time;
            if(currentTime > playTime + attackClipDelayTime)
            {
                audioSource.PlayOneShot(attackClip, 0.25f);
                playTime = Time.time;
            }
        }

    }
    // Moves the player based on input
    void MovePlayer()
    {
        // Get player input from axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // move player based on input
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed, Space.World);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed, Space.World);
        // apply walking animation if player is moving
        //playerAnim.SetFloat("speed", Mathf.Abs(horizontalInput + verticalInput));
        // rotates the player according to the movement
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");
        float moveVertical = Input.GetAxisRaw ("Vertical");
        if(moveVertical != 0 || moveHorizontal != 0)
        {
            playerAnim.SetBool("walk", true);
            Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);
            transform.rotation = Quaternion.LookRotation(movement);
        }
        if(moveVertical == 0 && moveHorizontal == 0)
        {
            playerAnim.SetBool("walk", false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
      // Perform an attack if spacebar is pressed and in contact with enemy
      if(Input.GetKeyDown(KeyCode.Space) && collision.gameObject.CompareTag("Enemy"))
      {
          Vector3 attackDirection = (collision.transform.position - transform.position).normalized;
          collision.gameObject.GetComponent<Enemy>().health -= attackStrength;
          //Debug.Log("Player attack!");
      }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Health Item") && health < maxHealth)
        {
            audioSource.PlayOneShot(healthItemClip, 1.0f);
            health += other.gameObject.GetComponent<HealthItem>().health;
            if(health > maxHealth)
            {
                health = maxHealth;
                healthText.text = "Health: " + health;
            }else
            {
                healthText.text = "Health: " + health;
            }
            Destroy(other.gameObject);
            //Debug.Log(health);
        }

        if(other.CompareTag("Pickup Item"))
        {
            audioSource.PlayOneShot(pickupItemClip, 1.0f);
            points += other.gameObject.GetComponent<PickupItem>().points;
            scoreText.text = "Score: " + points;
            Destroy(other.gameObject);
            //Debug.Log(points + " points");
        }
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int mode)
    {
        points = 0;
        health = 100;
        difficulty = mode;
        gameOver = false;
        isGameActive = true;

        scoreText.text = "Score: " + points;
        healthText.text = "Health: " + health;

        titleScreen.gameObject.SetActive(false);
    }
}
