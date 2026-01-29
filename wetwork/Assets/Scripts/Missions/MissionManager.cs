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

    // Dictionary to store mission checkpoints
    private Dictionary<string, int> missionCheckpoints = new Dictionary<string, int>();

    private Mission current;

    private void Start()
    {
        current = GetComponentInChildren<Mission>();
        missionStates[current.missionID] = MissionState.Inactive;
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
        missionCheckpoints[missionID] = 0; // Initialize checkpoint
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

    // Save mission checkpoint
    public void SaveMissionCheckpoint(string missionID, int checkpoint)
    {
        if(missionCheckpoints.ContainsKey(missionID))
        {
            missionCheckpoints[missionID] = checkpoint;
        }
    }
    // Load mission checkpoint
    public int LoadMissionCheckpoint(string missionID)
    {
        return missionCheckpoints.ContainsKey(missionID) ? missionCheckpoints[missionID] : 0;
    }
}
