using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private Button _closePanelsButton;
    [SerializeField] private MissionPanel _leftMissionPanel;
    [SerializeField] private MissionPanel _rightMissionPanel;
    [SerializeField] private FightPanel _fightPanel;
    [SerializeField] private UIPanelsCloser _uiPanelsCloser;
    [SerializeField] private ChooseHeroMessage _chooseHeroMessage;

    [SerializeField] private HeroViewFactory _heroViewFactory;
    [SerializeField] private MissionButtonsViewFactory _missionButtonViewFactory;

    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    private List<MissionData> _missionsData;
    private List<MissionButtonView> _missionButtonsViews;
    private List<MissionButton> _missionButtons;
    private StartMissionInitializer _startMissionInitializer;
    private MissionsUnlocker _missionsUnlocker;

    private Dictionary<Type, HeroModel> _heroModels;
    private List<HeroView> _heroViews;
    private HeroSelector _heroSelector;
    private HeroUnlocker _heroUnlocker;
    private ExperienceDistributor _experienceDistributor;

    private FightPanelInitializer _fightPanelInitializer;

    private void Start()
    {
        InitializeMissions();
        InitializeHeroes();
        InitializeUIMissionPanels();
        InitializeFinishMissionHandlers();

        _compositeDisposable.AddRange(_missionButtons.ToArray());
        _compositeDisposable.Add(_startMissionInitializer);
        _compositeDisposable.Add(_missionsUnlocker);
        _compositeDisposable.Add(_experienceDistributor);
        _compositeDisposable.Add(_heroUnlocker);
        _compositeDisposable.Add(_fightPanelInitializer);
    }

    private void OnDestroy()
    {
        _compositeDisposable.DisposeAll();
    }

    private void InitializeMissions()
    {
        MissionLoader missionLoader = new MissionLoader();
        _missionsData = missionLoader.GetMissionsData();
        MissionButtonsFactory missionButtonsFactory = new MissionButtonsFactory(_leftMissionPanel, _rightMissionPanel);
        _missionButtons = missionButtonsFactory.CreateMissionButtons(_missionsData);
        _missionButtonsViews = _missionButtonViewFactory.CreateViews(_missionButtons);
    }

    private void InitializeHeroes()
    {
        HeroLoader _heroLoader = new HeroLoader();
        _heroModels = _heroLoader.GetHeroes();
        _heroViews = _heroViewFactory.CreateViews(_heroModels);
        _heroSelector = new HeroSelector(_heroViews);
        _uiPanelsCloser.Initialize();
    }

    private void InitializeUIMissionPanels()
    {
        _startMissionInitializer = new StartMissionInitializer(_closePanelsButton, _leftMissionPanel, _rightMissionPanel, _fightPanel, _heroSelector);
        _fightPanelInitializer = new FightPanelInitializer(_fightPanel, _startMissionInitializer, _heroSelector);
        _chooseHeroMessage.Initialize(_startMissionInitializer);
        UIStateChanger uiStateChanger = new UIStateChanger(_missionButtonsViews, _fightPanelInitializer, _uiPanelsCloser, _startMissionInitializer);
    }

    private void InitializeFinishMissionHandlers()
    {
        _missionsUnlocker = new MissionsUnlocker(_missionsData);
        _heroUnlocker = new HeroUnlocker(_missionsData, _heroModels, _heroViews, _uiPanelsCloser, _startMissionInitializer);
        _experienceDistributor = new ExperienceDistributor(_missionsData, _heroModels, _heroSelector);
    }
}