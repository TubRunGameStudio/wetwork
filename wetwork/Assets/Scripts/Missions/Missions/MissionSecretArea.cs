using Unity.VisualScripting;
using UnityEngine;

public class MissionSecretArea : Mission
{
    [SerializeField] GameObject fogOfWar;

    // Update is called once per frame
    void Update()
    {
        if (!manager.IsMissionActive(missionID))
            return;

        if (fogOfWar.IsDestroyed())
            CompleteMission();
    }

    override public string GetMissionText()
    {
        return $"Find the secret area.";
    }
}
