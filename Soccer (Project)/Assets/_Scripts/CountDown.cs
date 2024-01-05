using System.Collections;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private void Start() => StartCoroutine(PowerupCountDown());

    private IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}