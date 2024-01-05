using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private const int rotationSpeed = 150;

    private void FixedUpdate() => CheckOrRotate();

    private void CheckOrRotate()
    {
        if (GetHorizontalInput() != 0)
            Rotate();
    }

    private void Rotate() => transform.Rotate(Vector3.up, GetHorizontalInput() * -rotationSpeed * Time.fixedDeltaTime);

    private float GetHorizontalInput() => Input.GetAxis("Horizontal");
}