using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrampolineDirection
{
    BOTTOM,
    TOP,
    LEFT,
    RIGHT
}

public class Trampoline : MonoBehaviour
{
    [SerializeField] private PlatformerMovement pm;
    [SerializeField] private float launchForce;    
    public TrampolineDirection direction;
    public bool canBounce = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = pm.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Bounce();
    }

    private void Bounce()
    {
        if (!canBounce) return;

        if (canBounce)
        {
            switch (direction)
            {
                case TrampolineDirection.BOTTOM:
                    rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
                    break;

                case TrampolineDirection.TOP:
                    rb.AddForce(-Vector2.up * launchForce, ForceMode2D.Impulse);
                    break;

                case TrampolineDirection.LEFT:
                    rb.AddForce(Vector2.right * launchForce, ForceMode2D.Impulse);
                    break;

                case TrampolineDirection.RIGHT:
                    rb.AddForce(-Vector2.right * launchForce, ForceMode2D.Impulse);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBounce = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBounce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canBounce = false;
    }
}
