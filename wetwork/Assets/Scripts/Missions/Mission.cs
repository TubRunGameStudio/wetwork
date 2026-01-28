using System;
using UnityEngine;

public abstract class Mission : MonoBehaviour
{
    [SerializeField] protected string missionID;
    protected MissionManager manager;

    private void Start()
    {
        manager = GetComponentInParent<MissionManager>();
        if (manager == null)
            throw new RogueMissionException($"This mission {missionID} does not belong to a manager");
    }
    public void StartMission()
    {
        manager.StartMission(missionID);
    }

    protected void CompleteMission()
    {
        manager.CompleteMission(missionID);
    }

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
