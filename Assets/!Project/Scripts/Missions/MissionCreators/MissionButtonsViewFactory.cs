using System.Collections.Generic;
using UnityEngine;

public class MissionButtonsViewFactory : MonoBehaviour, IViewFactory<List<MissionButtonModel>, List<MissionButtonView>>
{
    [SerializeField] private GameObject _missionButtonPrefab;
    [SerializeField] private Transform _missionContainer;

    public List<MissionButtonView> CreateViews(List<MissionButtonModel> _missionsButtonsModel)
    {
        List<MissionButtonView> missionButtonViews = new List<MissionButtonView>();

        foreach (MissionButtonModel missionButtonModel in _missionsButtonsModel)
        {
            GameObject missionButtonGameObject = Instantiate(_missionButtonPrefab, _missionContainer);

            MissionButtonView missionButtonView = missionButtonGameObject.GetComponent<MissionButtonView>();
            missionButtonView.InitializeView(missionButtonModel);
            missionButtonViews.Add(missionButtonView);
        }

        return missionButtonViews;
    }
}