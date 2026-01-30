using Unity.VisualScripting;
using UnityEngine;

public class MissionSecretArea : Mission
{
    bool isCleared = false;

    public void ClearSecretArea()
    {
        if(manager.IsMissionActive((missionID)))
            CompleteMission();
        isCleared = true;
    }

    public override void StartMission()
    {
        base.StartMission();
        if (isCleared)
            CompleteMission();
    }

    override public string GetMissionText()
    {
        return $"Find the secret area.";
    }
}
