using UnityEngine;
using UnityEngine.UI;

public class ActiveMissionButtonState : MissionButtonState
{
    private Image _missionButtonImage;

    public ActiveMissionButtonState(Image missionButtonImage)
    {
        _missionButtonImage = missionButtonImage;
    }

    public override void Enter()
    {
        _missionButtonImage.raycastTarget = true;
        _missionButtonImage.gameObject.SetActive(true);
    }
}