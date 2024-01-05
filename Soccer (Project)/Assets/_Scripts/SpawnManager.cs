using System;
using System.Collections;
using UnityEngine;

public sealed class SpawnManager : MonoBehaviour
{
    public Action OnGoal;

    [Space(20)]
    [Header("Transforms")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform powerup;

    private int enemiesQuantity = 1;

    private void Start()
    {
        SpawnPowerup();
        SpawnEnemy();
    }

    private void OnEnable()
    {
        OnGoal += SpawnEnemy;
        OnGoal += SpawnPowerup;
    }

    private void OnDestroy()
    {
        OnGoal -= SpawnEnemy;
        OnGoal -= SpawnPowerup;
    }

    public void SpawnPowerup()
    {
        Vector3 randVector;
        GeneratyRandomVector(out randVector);
        Instantiate(powerup, randVector, Quaternion.identity);
    }

    public void SpawnEnemy() => StartCoroutine(SpawnWithDelay());

    private void GeneratyRandomVector(out Vector3 randVector)
    {
        const int edgeOfCliff = 6;
        int randXPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);
        int randZPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);

        randVector = new Vector3(randXPos, 0, randZPos);
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < enemiesQuantity; i++)
        {
            Vector3 randVector;
            GeneratyRandomVector(out randVector);
            Instantiate(enemy, randVector, Quaternion.identity);
        }
        EnemyDeathCount.Instance.Count = enemiesQuantity;
        enemiesQuantity++;
    }
}