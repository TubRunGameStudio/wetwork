using UnityEngine;

public class MissionActivateTower : Mission
{
    [SerializeField] RadioTower tower;
    private int active = 0;

    // Update is called once per frame
    void Update()
    {
        if (!manager.IsMissionActive(missionID))
            return;

        if(active < tower.NumActive())
        {
            active = tower.NumActive();
            manager.RefreshText();
        }
        if (tower.AllActive())
        {
            CompleteMission();
        }
    }

    override public string GetMissionText()
    {
        return $"Active three radio relays to power up the radio tower and sync your minimap.  Completed ({tower.NumActive()}/{tower.NumRelays()})";
    }
}
