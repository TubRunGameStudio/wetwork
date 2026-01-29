using Unity.VisualScripting;
using UnityEngine;

public class MissionSecretArea : Mission
{
    public void ClearSecretArea()
    {
        if(manager.IsMissionActive((missionID)))
            CompleteMission();
    }

    override public string GetMissionText()
    {
        return $"Find the secret area.";
    }
}
