using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RadioTowerRelayEvent : UnityEvent<int> { }

public class RadioTower : MonoBehaviour
{
    [SerializeField] RadioRelay[] relays;
    [SerializeField] Animator animator;
    [SerializeField] GameObject range;
    public RadioTowerRelayEvent activateRelayEvent;
    public UnityEvent activateRadioEvent;
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
            {
                numActive++;
            }
            else
                allActive = false;
        }

        if (numActive > 0)
        {
            animator.SetBool("anyActive", true);
            activateRelayEvent.Invoke(numActive);
        }

            if (allActive)
        {
            animator.SetBool("allActive", true);
            range.SetActive(true);
            activateRadioEvent.Invoke();
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
