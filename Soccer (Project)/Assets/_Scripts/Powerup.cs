using UnityEngine;

public class Powerup
{
    private Transform powerupIndicator;
    private Transform playerTransform;

    private const int powerupStrength = 15;

    public Powerup(Transform powerupIndicator, Transform playerTransform)
    {
        this.powerupIndicator = powerupIndicator;
        this.playerTransform = playerTransform;
    }

    public void ApplyPowerup(Collision collision)
    {
        Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
        Vector3 direction = collision.transform.position - playerTransform.position;
        enemyRb.AddForce(direction * powerupStrength, ForceMode.Impulse);
    }

    public void SetPowerupIndicator(bool flag) => powerupIndicator.gameObject.SetActive(flag);
}