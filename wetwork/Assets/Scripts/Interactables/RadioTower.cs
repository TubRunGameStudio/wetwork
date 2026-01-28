using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RadioTower : MonoBehaviour
{
    [SerializeField] RadioRelay[] relays;
    [SerializeField] Animator animator;
    private GameController controller;
    public const float RANGE = 15f;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void Refresh()
    {
        bool allActive = true;
        bool anyActive = false;

        foreach(RadioRelay relay in relays)
        {
            if (relay.active)
                anyActive = true;
            else
                allActive = false;
        }

        if (anyActive)
            animator.SetBool("anyActive", true);

        if (allActive)
        {
            animator.SetBool("allActive", true);
            controller.SetMinimap(true);
        }
    }
}
