using UnityEngine;
using UnityEngine.Events;

public class RadioMissionEvent : MonoBehaviour
{
    public UnityEvent activateRelay;
    public UnityEvent activateRadio;
    private RadioTower tower;
    private int active = 0;
    bool completed = false;

    private void Start()
    {
        tower = GetComponent<RadioTower>();
    }

    private void Update()
    {
        if (completed)
            return;

        int diff = tower.NumActive() - active;
        for(int i = 0; i < diff; i++)
        {
            active++;
            activateRelay.Invoke();
        }
        if (tower.AllActive())
        {
            activateRadio.Invoke();
            completed = true;
        }
    }


}
