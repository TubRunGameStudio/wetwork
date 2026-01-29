using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// Define event for mission completion
public class MissionCompletedEventArgs : EventArgs
{
    public string MissionID { get; private set; }

    public MissionCompletedEventArgs(string missionID)
    {
        MissionID = missionID;
    }
}


public class MissionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI missionText;
    // Event for mission completion
    public static event EventHandler<MissionCompletedEventArgs> MissionCompleted;

    // Dictionary to store mission states
    private Dictionary<string, MissionState> missionStates = new Dictionary<string, MissionState>();
    private List<Mission> missions = new();
    private int missionIndex = 0;

    private Mission current;

    private void Start()
    {
        missions.AddRange(GetComponentsInChildren<Mission>());
        foreach (Mission mission in missions)
            missionStates[mission.missionID] = MissionState.Inactive;

        current = missions[0];
    }

    private void Update()
    {
        if (missionStates[current.missionID] == MissionState.Inactive)
            current.StartMission();
    }

    // Start a mission
    public void StartMission(string missionID)
    {
        missionStates[missionID] = MissionState.Active;
        Debug.Log($"Mission started: {missionID}");
        missionText.text = current.GetMissionText();

        // Trigger mission started event
        // You can hook into this event to handle mission specific initialization, like showing UI or starting dialogue
    }

    public void RefreshText()
    {
        missionText.text = current.GetMissionText();
    }

    public void CompleteMission(string missionID)
    {
        missionStates[missionID] = MissionState.Completed;
        Debug.Log($"Mission completed: {missionID}");
        missionIndex++;
        if (missions.Count > missionIndex)
            missions[missionIndex].StartMission();
        else
            missionText.text = string.Empty;

        // Trigger mission completed event
        OnMissionCompleted(missionID);
    }

    public bool IsMissionActive(string missionID)
    {
        return missionStates.ContainsKey(missionID) && missionStates[missionID] == MissionState.Active;
    }

    public bool IsMissionCompleted(string missionID)
    {
        return missionStates.ContainsKey(missionID) && missionStates[missionID] == MissionState.Completed;
    }

    // Trigger mission completed event
    protected virtual void OnMissionCompleted(string missionID)
    {
        EventHandler<MissionCompletedEventArgs> handler = MissionCompleted;
        if(handler != null)
        {
            handler(this, new MissionCompletedEventArgs(missionID));
        }
    }
}
