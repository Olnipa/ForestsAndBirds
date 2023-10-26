using System;
using System.Collections.Generic;

public class HeroUnlocker : IDisposable
{
    private List<MissionData> _missionsData;
    private UIPanelsCloser _uiPanelsCloser;
    private List<HeroView> _heroViews;
    private Dictionary<Type, HeroModel> _heroModels;
    private StartMissionInitializer _startMissionInitializer;

    public HeroUnlocker(List<MissionData> missionsData, Dictionary<Type, HeroModel> heroModels, List<HeroView> heroesView, 
        UIPanelsCloser uiPanelsCloser, StartMissionInitializer startMissionInitializer)
    {
        _missionsData = missionsData;
        _heroModels = heroModels;
        _heroViews = heroesView;
        _uiPanelsCloser = uiPanelsCloser;
        _startMissionInitializer = startMissionInitializer;

        _uiPanelsCloser.AllPanelsClosed += EnablePossibilityToChooseHero;
        _startMissionInitializer.StartButtonClickConfirmed += DisablePossibilityToChooseHero;

        foreach (MissionData missionData in _missionsData)
        {
            missionData.Completed += TryUnlockHero;
        }
    }

    public void Dispose()
    {
        _uiPanelsCloser.AllPanelsClosed -= EnablePossibilityToChooseHero;
        _startMissionInitializer.StartButtonClickConfirmed -= DisablePossibilityToChooseHero;
        
        foreach (MissionData missionData in _missionsData)
        {
            missionData.Completed -= TryUnlockHero;
        }
    }

    public void DisablePossibilityToChooseHero(MissionData missionData)
    {
        foreach (HeroView hero in _heroViews)
        {
            hero.SwitchRayCastTarget(false);
        }
    }

    public void EnablePossibilityToChooseHero()
    {
        foreach (HeroView hero in _heroViews)
        {
            hero.SwitchRayCastTarget(true);
        }
    }

    private void TryUnlockHero(MissionData missionData)
    {
        if (missionData.HeroesToUnlock == null)
            return;

        foreach (Type heroType in missionData.HeroesToUnlock)
        {
            if (_heroModels.TryGetValue(heroType, out HeroModel hero) && hero.IsUnlocked)
                continue;

            hero.UnLock();
            break;
        }
    }
}