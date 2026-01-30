using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;


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
    public static MissionManager MANAGER { get; private set; }

    // Event for mission completion
    public static event EventHandler<MissionCompletedEventArgs> MissionCompleted;

    // Dictionary to store mission states
    private Dictionary<string, MissionState> missionStates = new Dictionary<string, MissionState>();
    private List<Mission> missions = new();
    private int missionIndex = 0;
    private TextMeshProUGUI missionText;
    private GameController controller;

    private Mission current;

    private void Awake()
    {
        if (MANAGER != null && MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            MANAGER = this;

            foreach (Mission mission in missions)
                missionStates[mission.missionID] = MissionState.Inactive;

            DontDestroyOnLoad(gameObject);
        }
    }

    // Start a mission
    public void StartMission(Mission mission)
    {
        Initialize();
        current = mission;
        missionText.text = current.GetMissionText();
        current.StartMission();

        // Trigger mission started event
        // You can hook into this event to handle mission specific initialization, like showing UI or starting dialogue
    }

    public void RefreshMissionList()
    {
        GameObject missionList = GameObject.FindGameObjectWithTag("MissionList");
        if (missionList == null)
            return;

        missions = new List<Mission>();
        missions.AddRange(missionList.GetComponentsInChildren<Mission>());
        for (int i = 0; i < missions.Count; i++)
        {
            if (IsMissionCompleted(missions[i].missionID))
                continue;

            current = missions[i];
            if (!IsMissionActive(current.missionID))
                StartMission(current);
            break;
        }
    }

    public void StartMission(string missionID)
    {
        missionStates[missionID] = MissionState.Active;

    }

    public void RefreshText()
    {
        Initialize();
        missionText.text = current.GetMissionText();
    }

    private void Initialize()
    {
        if (controller == null)
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (missionText == null)
            missionText = controller.MISSION_TEXT;
    }

    public void CompleteMission(Mission mission)
    {
        missionStates[mission.missionID] = MissionState.Completed;
        missionIndex++;
        if (missions.Count > missionIndex)
             StartMission(missions[missionIndex]);
        else
            missionText.text = string.Empty;

        // Trigger mission completed event
        OnMissionCompleted(mission.missionID);
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
