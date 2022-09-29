using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 1f;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }


    public void Play()
    {
        StartCoroutine(ScreenShake());
    }

    IEnumerator ScreenShake()
    {
        var elapsedTime = 0f;
        do
        {
            transform.position = initialPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (elapsedTime < shakeDuration);

        transform.position = initialPos;
    }
}
