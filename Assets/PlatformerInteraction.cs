using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerInteraction : MonoBehaviour
{
    [SerializeField] private SpawnManager sm;
    [SerializeField] private GameObject itemManager;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Vector2 interactableSize;
    [SerializeField] private Vector3 interactableOffset;

    public GameObject battery;
    public GameObject[] randomItems;
    private GameObject item;

    //Interaction Field
    public bool canInteract = false;
    public bool interactItem;
    public GameObject interactedItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D interaction = Physics2D.OverlapBox(transform.position + interactableOffset, interactableSize, 0, interactableLayer);

        if (interaction)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }

        if (canInteract)
        {
            if (interaction.transform.gameObject.GetComponent<Crate>().hasLight)
            {
                item = Instantiate(battery, interaction.transform.position, Quaternion.identity);
                item.transform.SetParent(itemManager.transform);
            }
            else
            {
                //int index = Random.Range(0, randomItems.Length);
                //item = Instantiate(randomItems[index], interaction.transform.position, Quaternion.identity);
                //item.transform.SetParent(itemManager.transform);
            }

            Destroy(interaction.transform.gameObject);
            sm.crateNum--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactItem = true;
            interactedItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactItem = false;
        interactedItem = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + interactableOffset, interactableSize);
    }
}
