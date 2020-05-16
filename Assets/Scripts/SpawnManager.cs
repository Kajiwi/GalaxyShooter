using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    public GameObject[] _powerUps;


    private bool _stopSpawning = false;

    public void StartSpawning ()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);

        while(_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemy, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

 
    }

    IEnumerator SpawnPowerUp ()
    {
        yield return new WaitForSeconds(2f);

        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerID = Random.Range(0, _powerUps.Length);
            GameObject newPowerUp = Instantiate(_powerUps[randomPowerID], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4f, 10f));
        }

    }

    public void OnPlayerDeath ()
    {
        _stopSpawning = true;
    }
}
