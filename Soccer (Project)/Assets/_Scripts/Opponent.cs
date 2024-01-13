using UnityEngine;

public sealed class Opponent : MonoBehaviour
{
    private SpawnManager spawnManager;
    private GameObject goal;
    private Rigidbody rb;
    private int chaseSpeed = 20;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        goal = GameObject.FindGameObjectWithTag("PlayerGoal");
        rb = GetComponent<Rigidbody>();
    }

    private void Start() => chaseSpeed += EnemyDeathCount.Instance.Count * 2;

    private void FixedUpdate() => Chase();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerGoal"))
        {
            DecreaseCount();
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("OpponentGoal"))
            gameObject.SetActive(false);
    }

    public void DecreaseCount()
    {
        int count = --EnemyDeathCount.Instance.Count;
        if (count == 0)
            spawnManager.OnGoal.Invoke();
    }

    private void Chase()
    {
        Vector3 lookDirection = goal.transform.position - transform.position.normalized;
        rb.AddForce(lookDirection * chaseSpeed * Time.fixedDeltaTime);
    }
}