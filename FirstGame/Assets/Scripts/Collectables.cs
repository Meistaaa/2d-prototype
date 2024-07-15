using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    
    enum ItemType { Coin, Health, Ammo, InventoryItem };
   
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite inventorySprite;
    [SerializeField] public string inventoryStringName;

    NewPlayer newPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if (type == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected += 1;
            }
            else if (type == ItemType.Health)
            {
                if (NewPlayer.Instance.health < 100)
                {
                    NewPlayer.Instance.health += 10;
                }
            }
            else if (type == ItemType.InventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            else { }
            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
    }
}
