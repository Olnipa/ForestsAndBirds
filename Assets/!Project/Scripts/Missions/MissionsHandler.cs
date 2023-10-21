using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _missionButtonPrefab;
    [SerializeField] private MissionPanel _leftMissionPanel;
    [SerializeField] private MissionPanel _rightMissionPanel;

    [SerializeField] private HeroesHandler _heroesHandler;

    private Transform _missoinContainer;
    private UIStateMachine _uiStateMachine;

    private List<MissionData> _missionsData = new List<MissionData>();
    private List<MissionButton> _missionsButtonsModel = new List<MissionButton>();

    private void Start()
    {
        _uiStateMachine = new UIStateMachine();
    }

    private void OnMissionComplete(List<string> missionsIDToUnlock, List<Type> heroesTypesToUnlock)
    {
        foreach (var missionIDToUnlock in missionsIDToUnlock)
        {
            MissionData missionToUnlock = _missionsData.FirstOrDefault(missionData => missionData.ID == missionIDToUnlock);

            if (missionToUnlock != null && missionToUnlock.State == MissionState.Blocked)
                missionToUnlock.SetNewState(MissionState.Active);
        }

        _heroesHandler.TryUnlockHeroesByType(heroesTypesToUnlock);
    }

    private readonly List<MissionData> _usedMissionsData = new List<MissionData>();

    private void FillMissionButtonsModelList()
    {
        foreach (var missionData in _missionsData)
        {
            if (_usedMissionsData.Contains(missionData))
                continue;

            _missionsButtonsModel.Add(GetButton(missionData));
        }
    }

    private MissionButton GetButton(MissionData missionData)
    {
        if (string.IsNullOrEmpty(missionData.MutuallyExclusiveID))
            return GetSingleMissionButton(missionData);

        return GetDoubleMissionButton(missionData);
    }

    private MissionButton GetDoubleMissionButton(MissionData currentMissionData)
    {
        MissionData MutuallyExclusiveMissionData = _missionsData.FirstOrDefault(missionData => 
            missionData.ID == currentMissionData.MutuallyExclusiveID);

        if (MutuallyExclusiveMissionData == null)
            throw new InvalidOperationException($"Exclusive mission with ID {currentMissionData.MutuallyExclusiveID} does not exist.");

        _usedMissionsData.Add(MutuallyExclusiveMissionData);

        return new DoubleMissionButton(currentMissionData, MutuallyExclusiveMissionData, _leftMissionPanel, _rightMissionPanel);
    }

    private MissionButton GetSingleMissionButton(MissionData missionData)
    {
        _usedMissionsData.Add(missionData);
        return new SingleMissionButton(missionData, _leftMissionPanel);
    }

    private void OnMissionButtonClick(MissionButton missionButton)
    {
        if (missionButton is SingleMissionButton)
            _uiStateMachine.ChangeState(new SingleMissionInfoUIState(_leftMissionPanel));
        else if (missionButton is DoubleMissionButton)
            _uiStateMachine.ChangeState(new DoubleMissionInfoUIState(_leftMissionPanel, _rightMissionPanel));
    }

    private void InstantiateMissionButtonsView()
    {
        foreach (MissionButton missionButtonModel in _missionsButtonsModel)
        {
            missionButtonModel.MissionButtonClicked += OnMissionButtonClick;

            GameObject missionButtonGameObject = Instantiate(_missionButtonPrefab, _missoinContainer);

            MissionButtonView missionButtonView = missionButtonGameObject.GetComponent<MissionButtonView>();

            missionButtonModel.InitializeView(missionButtonView);
        }
    }

    private void OnDisable()
    {
        foreach(MissionButton missionButtonModel in _missionsButtonsModel)
        {
            missionButtonModel.MissionButtonClicked -= OnMissionButtonClick;
        }

        foreach (var missionData in _missionsData)
        {
            missionData.Completed += OnMissionComplete;
        }
    }

    public void Initialize(List<MissionData> missionsData)
    {
        foreach (var missionData in missionsData)
        {
            missionData.Completed += OnMissionComplete;
        }

        _missoinContainer = GetComponent<Transform>();
        _missionsData = missionsData;

        FillMissionButtonsModelList();
        InstantiateMissionButtonsView();
    }
}