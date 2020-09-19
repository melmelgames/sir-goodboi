using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float enemyAttackRate = 0.25f;
    private int enemyAttackStrength = 1;
    private AudioSource audioSource;
    private AudioClip explosionClip;

    public float speed;
    public int health;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyAttackRate *= player.GetComponent<PlayerController>().difficulty;
        audioSource = player.GetComponent<AudioSource>();
        explosionClip = player.GetComponent<PlayerController>().explosionClip;

    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<PlayerController>().gameOver)
        {
            MoveTowardsPlayer();
        }
        if(health < 0)
        {
            player.GetComponent<PlayerController>().lastEnemyDefeatedTime = Time.time;
            audioSource.PlayOneShot(explosionClip, 0.5f);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 movement = player.transform.position - transform.position;
        Vector3 playerDirection = movement.normalized;
        transform.Translate(playerDirection * Time.deltaTime * speed, Space.World);
        transform.rotation = Quaternion.LookRotation(movement);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!player.GetComponent<PlayerController>().gameOver)
        {
            // Perform an attack if spacebar is pressed and in contact with enemy
            if(collision.gameObject.CompareTag("Player") && Random.value < enemyAttackRate)
            {
                Vector3 attackDirection = (collision.transform.position - transform.position).normalized;
                collision.gameObject.GetComponent<PlayerController>().health -= enemyAttackStrength;
                int health = collision.gameObject.GetComponent<PlayerController>().health;
                collision.gameObject.GetComponent<PlayerController>().healthText.text = "Health: " + health;
                //Debug.Log("Enemy attack!");
            }
        }
    }

}
