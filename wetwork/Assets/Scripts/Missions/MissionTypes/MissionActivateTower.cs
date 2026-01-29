using UnityEngine;

public class MissionActivateTower : Mission
{
    private int numActive = 0;

    override public string GetMissionText()
    {
        return $"Active 3 radio relays to power up the radio tower and sync your minimap.  Completed {numActive} total.";
    }

    public void ActivateRelay(int num)
    {
        if (!manager.IsMissionActive((missionID)))
            return;
        numActive = num;
        manager.RefreshText();
    }

    public void ActivateRadio()
    {
        if(manager.IsMissionActive((missionID)))
            CompleteMission();
    }
}
