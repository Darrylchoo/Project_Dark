using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private PlatformerInteraction pi;

    public ItemDatabase item;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Player 1").GetComponent<PlatformerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (pi.interactItem)
            {
                Destroy(pi.interactedItem);
                UseItem();
            }
        }
    }

    private void UseItem()
    {
        switch (item.itemType)
        {
            case ItemType.Battery:
                break;

            case ItemType.HealthPack:
                break;

            case ItemType.Jetpack:
                break;
        }
    }
}
