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
    private ObjectPooler pooler;

    private int enemiesQuantity = 1;

    private void Awake() => pooler = GetComponent<ObjectPooler>();

    private void Start()
    {
        pooler.CreateObjects(transform);
        SpawnPowerup();
        ReleaseEnemy();
    }

    private void OnEnable()
    {
        OnGoal += ReleaseEnemy;
        OnGoal += SpawnPowerup;
    }

    private void OnDestroy()
    {
        OnGoal -= ReleaseEnemy;
        OnGoal -= SpawnPowerup;
    }

    public void SpawnPowerup() => Instantiate(powerup, RandomOpponentVector(zTopBorder: -5, zDownBorder: 1), Quaternion.identity);

    public void ReleaseEnemy() => StartCoroutine(ReleaseWithDelay());

    private IEnumerator ReleaseWithDelay()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < enemiesQuantity; i++)
        {
            Transform prefab = pooler.ReleaseObject();
            prefab.transform.position = RandomOpponentVector(zTopBorder:10, zDownBorder:25);
            prefab.gameObject.SetActive(true);
        }

        EnemyDeathCount.Instance.Count = enemiesQuantity;
        enemiesQuantity++;
    }

    private Vector3 RandomOpponentVector(int zTopBorder, int zDownBorder)
    {
        const int xRange = 6;
        int randXPos = UnityEngine.Random.Range(-xRange, xRange);
        int randZPos = UnityEngine.Random.Range(zTopBorder, zDownBorder);

        return new Vector3(randXPos, 0, randZPos);
    }
}