using UnityEngine;

public class MissionActivateTower : Mission
{
    [SerializeField] RadioTower tower;

    // Update is called once per frame
    void Update()
    {
        if (!manager.IsMissionActive(missionID))
            return;

        if (tower.AllActive())
        {
            CompleteMission();
        }
    }
}
