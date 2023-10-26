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

    private void Start()
    {
        MissionLoader missionLoader = new MissionLoader();
        List<MissionData> missionsData = missionLoader.GetMissionsData();

        HeroLoader _heroLoader = new HeroLoader();
        Dictionary<Type, HeroModel> heroModels = _heroLoader.GetHeroes();
        List<HeroView> heroViews = _heroViewFactory.CreateViews(heroModels);
        HeroSelector heroSelector = new HeroSelector(heroViews);
        _uiPanelsCloser.Initialize();

        StartMissionInitializer missionPanelsController = new StartMissionInitializer(_closePanelsButton, _leftMissionPanel, _rightMissionPanel, _fightPanel, heroSelector);
        MissionsUnlocker missionsUnlocker = new MissionsUnlocker(missionsData);
        _chooseHeroMessage.Initialize(missionPanelsController);

        StartMissionInitializer startMissionInitializer = new StartMissionInitializer(_closePanelsButton, _leftMissionPanel, _rightMissionPanel, _fightPanel, heroSelector);
        HeroUnlocker heroUnlocker = new HeroUnlocker(missionsData, heroModels, heroViews, _uiPanelsCloser, startMissionInitializer);

        ExperienceDistributor experienceDistributor = new ExperienceDistributor(missionsData, heroModels, heroSelector);
        
        MissionButtonsModelFactory missionButtonsFactory = new MissionButtonsModelFactory(_leftMissionPanel, _rightMissionPanel);
        List<MissionButtonModel> missionButtonModels = missionButtonsFactory.CreateMissionButtonModels(missionsData);
        List<MissionButtonView> missionButtonViews = _missionButtonViewFactory.CreateViews(missionButtonModels);

        FightPanelInitializer fightPanelInitializer = new FightPanelInitializer(_fightPanel, missionPanelsController, heroSelector);
        UIStateChanger uiStateChanger = new UIStateChanger(missionButtonViews, fightPanelInitializer, _uiPanelsCloser, startMissionInitializer);

        _compositeDisposable.AddRange(missionButtonModels.ToArray());
        _compositeDisposable.Add(missionPanelsController);
        _compositeDisposable.Add(missionsUnlocker);
        _compositeDisposable.Add(experienceDistributor);
        _compositeDisposable.Add(heroUnlocker);
        _compositeDisposable.Add(fightPanelInitializer);
        _compositeDisposable.Add(startMissionInitializer);
    }

    private void OnDestroy()
    {
        _compositeDisposable.DisposeAll();
    }
}