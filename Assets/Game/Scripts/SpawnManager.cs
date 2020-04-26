using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerups;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartCoroutines()
    {
        StartCoroutine(SpawnEnemyPrefab());
        StartCoroutine(SpawnPowerupPrefab());
    } 
    IEnumerator SpawnEnemyPrefab()
    {
        while (!_gameManager.gameOver)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-7.8f, 7.8f), 7f, 0),
                Quaternion.identity);
            yield return new WaitForSeconds(4f);
            Debug.Log("printing" + _gameManager.gameOver);
        }
    }

    IEnumerator SpawnPowerupPrefab()
    {
        while (!_gameManager.gameOver)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], new Vector3(Random.Range(-7.8f, 7.8f), 6.5f, 0),
                Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
