using System;
using UnityEngine;

public abstract class Mission : MonoBehaviour
{
    [SerializeField] public string missionID;
    protected MissionManager manager;

    private void Start()
    {
        manager = GetComponentInParent<MissionManager>();
        if (manager == null)
            throw new RogueMissionException($"This mission {missionID} does not belong to a manager");
    }
    public void StartMission()
    {
        manager.StartMission(this);
    }

    protected void CompleteMission()
    {
        manager.CompleteMission(this);
    }

    public abstract string GetMissionText();

    private class RogueMissionException : System.Exception
    {
        public RogueMissionException()
        { }

        public RogueMissionException(string message)
            : base(message)
        { }

        public RogueMissionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
