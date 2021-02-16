using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    LEFT,
    RIGHT
}

public class PlatformerPlayerSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    public FacingDirection currentFacingDirection;

    private Vector3 lastCoordinate;
    private PlatformerMovement pm;

    // Update is called once per frame
    void Update()
    {
        pm = GetComponent<PlatformerMovement>();

        //CHECK IF LAST POSITION IS TOWARDS THE LEFT OF CURRENT POSITION
        CheckHeadingDirection();
        FlipSprite();
    }

    private void CheckHeadingDirection()
    {
        if (pm.P1)
        {
            float input = Input.GetAxisRaw("Horizontal_P1");
            if (input != 0)
            {
                currentFacingDirection = input > 0 ? FacingDirection.RIGHT : FacingDirection.LEFT;
            }
        }
        else if (pm.P2)
        {
            float input = Input.GetAxisRaw("Horizontal_P2");
            if (input != 0)
            {
                currentFacingDirection = input > 0 ? FacingDirection.RIGHT : FacingDirection.LEFT;
            }
        }
    }

    private void FlipSprite()
    {
        sprite.flipX = currentFacingDirection == FacingDirection.RIGHT ? true : false;
    }
}
