using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BuildingLight : MonoBehaviour
{
    [SerializeField] private Light2D[] lights;

    public float minInterval;
    public float maxInterval;
    public float flickerTimer;
    public bool flicker;
    private float rememberFlickerTimer;

    // Start is called before the first frame update
    void Start()
    {
        flicker = true;
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Flicker()
    {
        while (flicker)
        {
            yield return new WaitForSeconds(flickerTimer);

            int random = Random.Range(0, lights.Length);
            lights[random].enabled = !lights[random].enabled;

            rememberFlickerTimer += Time.deltaTime;

            if (rememberFlickerTimer >= 1)
            {
                flicker = false;
                rememberFlickerTimer = 0;

                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].enabled = false;
                }
            }
        }

        if (!flicker)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            flicker = true;
        }

        StartCoroutine(Flicker());
    }
}
