using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     public TextMeshProUGUI coinsText;
    public Image healthBar;
    public Image inventoryItemImage;

    private static GameManager instance;
    public static GameManager Instance {  
        get 
        {
            if (instance == null)
            { 
                instance = GameObject.FindObjectOfType<GameManager>();
            }

            return instance;
        } 
    }
    private void Awake()
    {
       if(GameObject.Find("New Game Manager")) Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Game Manager";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
