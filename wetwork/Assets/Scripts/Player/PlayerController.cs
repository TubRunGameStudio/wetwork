using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PLAYER { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] Animator animator;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] InputActionReference fire;
    [SerializeField] GameObject damageFlash;
    [SerializeField] public GameObject reticule;

    private GameController controller;
    private PlayerHealthBar healthBar;
    private Rigidbody2D rb;
    private float movementX;
    private float movementY;
    private int health;
    private bool damageFrame = false;


    private void Awake()
    {
        if (PLAYER != null && PLAYER != this)
        {
            Destroy(this);
        }
        else
        {
            PLAYER = this;
        }

        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * 1.6f;
        movementY = movementVector.y * .9f;
    }

    void OnEnable()
    {
        fire.action.started += Fire;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        inventory.Fire(ctx, controller);
    }

    private void FixedUpdate()
    {
        // Damage
        if(damageFrame)
        {
            damageFlash.SetActive(true);
            damageFrame = false;
        } else
        {
            damageFlash.SetActive(false);
        }

            // Animation
            Vector3 movement = new Vector3(movementX, movementY);
        rb.linearVelocity = movement * speed;

        if (rb.linearVelocity.magnitude > 0)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);

        if (movementY > 0)
            animator.SetBool("North", true);
        else if (movementY < 0)
            animator.SetBool("North", false);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (movementX > 0)
            sprite.flipX = false;
        else if (movementX < 0)
            sprite.flipX = true;
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;

        healthBar.SetHealth(health);
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
            controller.EndGame();

        healthBar.SetHealth(health);
        damageFrame = true;
    }

    public void Pickup(String name, int amount)
    {
        inventory.Pickup(name, amount);
    }

    public void Initiate(GameController controller)
    {
        this.controller = controller;
        healthBar = controller.HEALTH_BAR;
        healthBar.SetHealth(health);

        inventory.controller = controller;
        inventory.Initiate();
    }
}
