using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MissionButtonsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _missionButtonPrefab;
    [SerializeField] private MissionPanel _leftMissionPanel;
    [SerializeField] private MissionPanel _rightMissionPanel;

    private Transform _missoinContainer;
    private UIStateMachine _uiStateMachine;

    private List<MissionData> _missionsData = new List<MissionData>();
    private List<MissionButton> _missionsButtonsModel = new List<MissionButton>();

    private void Start()
    {
        _uiStateMachine = new UIStateMachine();
    }

    public void Initialize(List<MissionData> missionsData)
    {
        _missoinContainer = GetComponent<Transform>();
        _missionsData = missionsData;

        FillMissionButtonsModelList();
        InstantiateMissionButtonsView();
    }

    private void FillMissionButtonsModelList()
    {
        for (int i = 0; i < _missionsData.Count; i++)
        {
            _missionsButtonsModel.Add(GetButton(_missionsData[i]));
        }
    }

    private MissionButton GetButton(MissionData missionData)
    {
        if (missionData.ExclusiveMissionID != "")
            return TryCreateDoubleMissionButton(missionData);
        else
            return new SingleMissionButton(missionData, _leftMissionPanel);
    }

    private MissionButton TryCreateDoubleMissionButton(MissionData firstMissionData)
    {
        MissionData exclusiveMissionData = _missionsData.FirstOrDefault(mission => mission.ID == firstMissionData.ExclusiveMissionID);

        if (exclusiveMissionData == null)
            throw new InvalidOperationException($"Exclusive mission with ID {firstMissionData.ExclusiveMissionID} does not exist.");

        DoubleMissionButton doubleButton = new DoubleMissionButton(firstMissionData, exclusiveMissionData, _leftMissionPanel, _rightMissionPanel);
        _missionsData.Remove(exclusiveMissionData);
        return doubleButton;
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
    }
}