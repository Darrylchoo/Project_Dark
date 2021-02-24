using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerInteraction : MonoBehaviour
{
    [SerializeField] private TorchBattery torch;
    [SerializeField] private SpawnManager sm;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Vector2 interactableSize;
    [SerializeField] private Vector3 interactableOffset;

    public Collider2D interaction;

    //Interaction Field
    public bool canInteract = false;
    public Tile itemTile;
    private Tilemap interactableTile;
    private Vector3Int currentTile;

    private PlatformerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlatformerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        interaction = Physics2D.OverlapBox(transform.position + interactableOffset, interactableSize, 0, interactableLayer);

        if (interaction)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }

        if (Input.GetKeyDown(pm.down))
        {
            if (canInteract)
            {
                //Interaction();
                Destroy(interaction.transform.gameObject);
                sm.crateNum--;
            }
        }
    }

    private void Interaction()
    {
        interactableTile = interaction.transform.GetComponent<Tilemap>();
        currentTile = interactableTile.WorldToCell(transform.position);

        if (interactableTile.GetTile(currentTile) != itemTile)
        {
            interactableTile.SetTile(currentTile, itemTile);
        }
        else
        {
            torch.Recharge();

            if (torch.canCharge)
            {
                torch.canCharge = false;
                interactableTile.SetTile(currentTile, null);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + interactableOffset, interactableSize);
    }
}
