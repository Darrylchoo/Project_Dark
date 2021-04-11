using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeIntensity;
    public float shakeDuration;
    private Transform target;
    private Vector3 initialPos;
    private Vector3 randomPos;
    private float shakeTimer;
    private float startTime;
    private bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Transform>();
        initialPos = target.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0 && !isShaking)
        {
            StartCoroutine(DoShake());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake(shakeDuration);
        }
    }

    public void Shake(float timer)
    {
        if (timer > 0)
        {
            shakeTimer += timer;
        }
    }

    private IEnumerator DoShake()
    {
        isShaking = true;
        startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + shakeTimer)
        {
            randomPos = new Vector3(Random.Range(-1, 1) * shakeIntensity, Random.Range(-1, 1) * shakeIntensity, initialPos.z);
            target.localPosition = randomPos;
            yield return null;
        }

        shakeTimer = 0;
        target.localPosition = initialPos;
        isShaking = false;
    }
}
