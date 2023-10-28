using System.Collections.Generic;
using UnityEngine;

public class MissionButtonsViewFactory : MonoBehaviour, IViewFactory<List<MissionButton>, List<MissionButtonView>>
{
    [SerializeField] private GameObject _missionButtonPrefab;
    [SerializeField] private Transform _missionContainer;

    public List<MissionButtonView> CreateViews(List<MissionButton> _missionsButtons)
    {
        List<MissionButtonView> missionButtonViews = new List<MissionButtonView>();

        foreach (MissionButton missionButtonModel in _missionsButtons)
        {
            GameObject missionButtonGameObject = Instantiate(_missionButtonPrefab, _missionContainer);

            MissionButtonView missionButtonView = missionButtonGameObject.GetComponent<MissionButtonView>();
            missionButtonView.InitializeView(missionButtonModel);
            missionButtonViews.Add(missionButtonView);
        }

        return missionButtonViews;
    }
}