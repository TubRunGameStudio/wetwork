using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PLAYER { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] Animator animator;
    [SerializeField] Animator silhouette;
    [SerializeField] SpriteRenderer silhouetteSprite;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] GameObject damageFlash;
    [SerializeField] public GameObject reticule;

    [SerializeField] InputActionReference fire;
    [SerializeField] InputActionReference interact;
    [SerializeField] InputActionReference menu;

    private GameController controller;
    private PlayerHealthBar healthBar;
    private Rigidbody2D rb;
    private float movementX;
    private float movementY;
    private bool damageFrame = false;
    private Interactable interactable;
    private List<Animator> animators;

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

        animators = new List<Animator>();
        animators.Add(animator);
        animators.Add(silhouette);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * 1.6f;
        movementY = movementVector.y * .9f;
    }

    #region - Enable / Disable - 
    void OnEnable()
    {
        fire.action.started += Fire;
        interact.action.started += Interact;
        menu.action.started += Menu;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
        interact.action.started -= Interact;
        menu.action.started -= Menu;

    }
    #endregion

    private void Fire(InputAction.CallbackContext ctx)
    {
        inventory.Fire(ctx, controller);
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (interactable != null)
            interactable.Interact();
    }

    private void Menu(InputAction.CallbackContext ctx)
    {
        controller.Menu();
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
            SetAnimators("Walking", true);
        else
            SetAnimators("Walking", false);

        if (movementY > 0)
            SetAnimators("North", true);
        else if (movementY < 0)
            SetAnimators("North", false);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (movementX > 0)
        {
            sprite.flipX = false;
            silhouetteSprite.flipX = false;
        }
        else if (movementX < 0)
        {
            sprite.flipX = true;
            silhouetteSprite.flipX = true;
        }
    }

    private void SetAnimators(string param, bool val)
    {
        foreach(Animator anim in animators)
        {
            anim.SetBool(param, val);
        }
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

    public void Reset()
    {
        PlayerState.InitGame(maxHealth, new List<Weapon>());
    }
}
