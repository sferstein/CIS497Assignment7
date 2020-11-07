using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* 
 * Sam Ferstein
 * Assignment 7
 * This manages the spawning of enemy waves.
 */

public class SpawnManager : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject enemyPrefab;
    public Text scoreText;
    public Text tutorialText;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public bool gameOver = false;
    public bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        scoreText.text = "Wave: " + waveNumber;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            tutorialText.gameObject.SetActive(false);
            gameStart = true;
        }
        if (enemyCount == 0 && gameStart)
        {
            waveNumber++;
            scoreText.text = "Wave: " + waveNumber;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
        if(waveNumber > 10)
        {
            gameOver = true;
            scoreText.text = "You Win! \nPress R to Restart";
        }
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosY);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
