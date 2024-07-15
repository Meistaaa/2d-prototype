using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [Header("Attributes")]
    public int attackPower = 30;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float attackDuration = 0.1f;


    [Header("Inventory")]
    [SerializeField] public int coinsCollected;
    [SerializeField] public int health = 100;
    private int maxHealth = 100;


    [Header("References")]
    [SerializeField] private GameObject attackBox; // attack box duration
    private Vector2 healthBarOrignalSize;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); // dictionary storing all invetory items string and values
    public Sprite inventoryItemBlank; // the default inventory item slot sprite
    public Sprite keySprite; // key inventory item

    // Start is called before the first frame update

    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";

        healthBarOrignalSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();

        SetSpawnLocation();
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);
        //If the player presses "Jump" and we're grounded, set the velocity to a jump power value
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        // local scale to -1 or 1 (left or right)

        if (targetVelocity.x > 0.1)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (targetVelocity.x < -0.1)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttackBox());

        }
        if (health < 0)
        {
            Die();
        }

    }

    public void SetSpawnLocation()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    // Activate attack function

    public IEnumerator ActivateAttackBox()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    // Update UI elements
    public void UpdateUI()
    {
        if(healthBarOrignalSize == Vector2.zero) healthBarOrignalSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        GameManager.Instance.coinsText.text = coinsCollected.ToString();
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrignalSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);

    }

    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }
    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }
}
