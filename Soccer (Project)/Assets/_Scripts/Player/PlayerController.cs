using System.Collections;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [Space(10)]
    [Header("Scripts")]
    [SerializeField] private SpawnManager spawnManager;

    [Space(20)]
    [Header("Transforms")]
    [SerializeField] private UnityEngine.Transform focusCentre;
    [SerializeField] private UnityEngine.Transform powerupIndicator;

    [Space(20)]
    [Header("Particles")]
    [SerializeField] private ParticleSystem smokeParticle;

    private Rigidbody rb;
    private PlayerMove playerMove;

    private bool hasPowerup = false;
    private const int powerupStrength = 15;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = new PlayerMove(rb, focusCentre, smokeParticle);
    }

    private void Start() => SetPowerupIndicator(false);

    private void OnEnable() => spawnManager.OnGoal += ResetProperties;

    private void OnDestroy() => spawnManager.OnGoal -= ResetProperties;

    private void Update() => playerMove.CheckForBoost();

    private void FixedUpdate()
    {
        playerMove.CheckOrMove();
        powerupIndicator.position = transform.position;
        focusCentre.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Opponent") && hasPowerup)
            ApplyPowerup(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            SetPowerupIndicator(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDown());
        }
    }

    public void ApplyPowerup(Collision collision)
    {
        Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
        Vector3 direction = collision.transform.position - transform.position;
        enemyRb.AddForce(direction * powerupStrength, ForceMode.Impulse);
    }

    public void SetPowerupIndicator(bool flag) => powerupIndicator.gameObject.SetActive(flag);

    private IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        SetPowerupIndicator(false);
        hasPowerup = false;
    }

    private void ResetProperties()
    {
        transform.position = new Vector3(0, 0, -7);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}