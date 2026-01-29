using UnityEngine;

public class MissionActivateTower : Mission
{
    private int active = 0;

    override public string GetMissionText()
    {
        return $"Active 3 radio relays to power up the radio tower and sync your minimap.  Completed {active} total.";
    }

    public void ActivateRelay(int numActive)
    {
        active = numActive;
        manager.RefreshText();
    }

    public void ActivateRadio()
    {
        CompleteMission();
    }
}
