using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RadioTower : MonoBehaviour
{
    [SerializeField] RadioRelay[] relays;
    [SerializeField] Animator animator;
    [SerializeField] GameObject range;
    private GameController controller;
    public const float RANGE = 20f;
    bool allActive = false;
    int numActive = 0;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void Refresh()
    {
        allActive = true;
        numActive = 0;

        foreach (RadioRelay relay in relays)
        {
            if (relay.active)
                numActive++;
            else
                allActive = false;
        }

        if (numActive > 0)
            animator.SetBool("anyActive", true);

        if (allActive)
        {
            animator.SetBool("allActive", true);
            range.SetActive(true);
            foreach (RadioRelay relay in relays)
            {
                relay.AllActive();
            }
        }
    }

    public bool AllActive()
    {
        return allActive;
    }

    public int NumActive()
    {
        return numActive;
    }

    public int NumRelays()
    {
        return relays.Length;
    }
}
