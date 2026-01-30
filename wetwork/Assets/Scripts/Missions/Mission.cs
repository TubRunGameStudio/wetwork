using System;
using System.Reflection;
using UnityEngine;

public abstract class Mission : MonoBehaviour
{
    [SerializeField] public string missionID;
    protected MissionManager manager;
    protected bool init = false;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        if (manager == null)
            throw new RogueMissionException($"This mission {missionID} does not belong to a manager");
    }

    public virtual void StartMission()
    {
        if(manager.IsMissionActive(missionID) || manager.IsMissionCompleted(missionID))
            throw new IllegalMissionException();

        manager.StartMission(missionID);
        Debug.Log($"Mission started: {missionID}");
    }

    protected void CompleteMission()
    {
        if (manager.IsMissionActive(missionID))
        {
            Debug.Log($"Mission completed: {missionID}");
            manager.CompleteMission(this);
        }
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

    private class IllegalMissionException : System.Exception
    {
        public IllegalMissionException()
        { }

        public IllegalMissionException(string message)
            : base(message)
        { }

        public IllegalMissionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
