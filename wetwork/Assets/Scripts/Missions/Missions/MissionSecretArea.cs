using Unity.VisualScripting;
using UnityEngine;

public class MissionSecretArea : Mission
{
    public void ClearSecretArea()
    {
        CompleteMission();
    }

    override public string GetMissionText()
    {
        return $"Find the secret area.";
    }
}
