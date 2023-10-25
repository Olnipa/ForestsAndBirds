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

    [SerializeField] private HeroViewFactory _heroViewFactory;
    [SerializeField] private MissionButtonViewFactory _missionButtonViewFactory;

    [SerializeField] private HeroesController _heroesController;

    private MissionPanelsController _missionPanelsController;
    private MissionsUnlocker _missionsController = new MissionsUnlocker();
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    private void Start()
    {
        MissionLoader missionLoader = new MissionLoader();
        List<MissionData> missionsData = missionLoader.GetMissionsData();
        
        HeroLoader _heroLoader = new HeroLoader();
        Dictionary<Type, HeroModel> heroModels = _heroLoader.GetHeroes();

        MissionButtonsModelFactory missionButtonsFactory = new MissionButtonsModelFactory(_leftMissionPanel, _rightMissionPanel);
        List<MissionButtonModel> missionButtonModels = missionButtonsFactory.CreateMissionButtonModels(missionsData);
        List<MissionButtonView> missionButtonViews = _missionButtonViewFactory.CreateViews(missionButtonModels);

        _missionPanelsController = new MissionPanelsController(_closePanelsButton, _leftMissionPanel, _rightMissionPanel, _fightPanel); 
        UIStateChanger uiStateChanger = new UIStateChanger(missionButtonViews, _missionPanelsController);

        List<HeroView> heroViews = _heroViewFactory.CreateViews(heroModels);
        HeroesController _heroesController = new HeroesController(heroModels, heroViews);

        _missionsController.Initialize(missionsData, _heroesController);
        _missionPanelsController.Initialize(_heroesController);
  
        _compositeDisposable.AddRange(missionButtonModels.ToArray());
        _compositeDisposable.Add(_missionPanelsController);
        _compositeDisposable.Add(_missionsController);
    }

    private void OnDestroy()
    {
        _compositeDisposable.DisposeAll();
    }
}