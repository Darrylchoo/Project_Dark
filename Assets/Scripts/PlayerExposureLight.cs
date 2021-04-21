using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerExposureLight : MonoBehaviour
{
    [SerializeField] private Light2D exposureLight;
    public float stillTimer;
    private PlatformerMovement pm;
    private float rememberStillTimer;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlatformerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        EnableLightExposure();
    }

    private void EnableLightExposure()
    {
        if (pm.moving)
        {
            exposureLight.enabled = false;
            rememberStillTimer = stillTimer;
        }
        else
        {
            if (rememberStillTimer > 0)
            {
                rememberStillTimer -= Time.deltaTime;
            }

            if (rememberStillTimer <= 0)
            {
                exposureLight.enabled = true;
            }
        }
    }
}
