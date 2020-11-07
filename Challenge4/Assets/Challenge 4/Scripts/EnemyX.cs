using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Sam Ferstein
 * Assignment 7
 * This is the enemy script.
 */

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private SpawnManagerX spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.FindGameObjectWithTag("PlayerGoal");
        spawnManager = FindObjectOfType<SpawnManagerX>();
        speed += spawnManager.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            spawnManager.gameOver = true;
            spawnManager.scoreText.text = "You Lose! \nPress R to Restart";
            Destroy(gameObject);
        }

    }

}
