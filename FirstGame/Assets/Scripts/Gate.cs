using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string requierdInventoryItemString;
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
            if (NewPlayer.Instance.inventory.ContainsKey(requierdInventoryItemString))
            {
                NewPlayer.Instance.RemoveInventoryItem(requierdInventoryItemString);
                Destroy(gameObject);
            }
        }
    }
}