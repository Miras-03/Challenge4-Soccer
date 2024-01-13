using UnityEngine;

public class PlayerMove
{
    private Rigidbody rb;
    private UnityEngine.Transform focusCentre;
    private ParticleSystem smokeParticle;

    private bool isTurboEnabled;

    private const int moveSpeed = 200;
    private const int turboSpeed = 600;

    public PlayerMove(Rigidbody rb, UnityEngine.Transform focusCentre , ParticleSystem smokeParticle)
    {
        this.rb = rb;
        this.focusCentre = focusCentre;
        this.smokeParticle = smokeParticle;
    }

    public void CheckOrMove()
    {
        if (GetVerticalInput() != 0)
            Move();
    }

    private void Move()
    {
        int speed = isTurboEnabled ? turboSpeed : moveSpeed;
        rb.AddForce(focusCentre.forward * GetVerticalInput() * speed * Time.fixedDeltaTime);
    }

    public void CheckForBoost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isTurboEnabled = true;
            smokeParticle.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        { 
            isTurboEnabled = false;
            smokeParticle.Stop();
        }
    }

    private float GetVerticalInput() => Input.GetAxis("Vertical");
}