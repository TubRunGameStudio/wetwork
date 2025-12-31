using System;
using System.Collections.Generic;
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
    [SerializeField] InputActionReference interact;
    [SerializeField] GameObject damageFlash;
    [SerializeField] public GameObject reticule;

    private GameController controller;
    private PlayerHealthBar healthBar;
    private Rigidbody2D rb;
    private float movementX;
    private float movementY;
    private bool damageFrame = false;
    private Interactable interactable;

    private void Awake()
    {
        if (PLAYER != null && PLAYER != this)
        {
            Destroy(this);
        }
        else
        {
            PLAYER = this;
            PlayerState.InitGame(maxHealth, new List<Weapon>());
        }

        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();


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
        interact.action.started += Interact;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
        interact.action.started -= Interact;
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        inventory.Fire(ctx, controller);
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (interactable != null)
            interactable.Interact();
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
        PlayerState.Health += heal;
        if (PlayerState.Health > maxHealth)
            PlayerState.Health = maxHealth;

        healthBar.SetHealth(PlayerState.Health);
    }
    public void Damage(int damage)
    {
        PlayerState.Health -= damage;
        if (PlayerState.Health <= 0)
            controller.EndGame();

        healthBar.SetHealth(PlayerState.Health);
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
        healthBar.SetHealth(PlayerState.Health);

        inventory.controller = controller;
        inventory.Initiate();
    }

    public void SetExitSceneTransition(SceneTransition exit)
    {
        this.interactable = exit;
    }
}
