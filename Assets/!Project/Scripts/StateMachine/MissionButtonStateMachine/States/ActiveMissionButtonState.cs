using UnityEngine;
using UnityEngine.UI;

public class ActiveMissionButtonState : MissionButtonState
{
    private Color _activeButtonColor;
    private Animator _missionButtonAnmator;
    private Image _missionButtonImage;

    public ActiveMissionButtonState(Animator missionButtonAnmator, Image missionButtonImage, Color activeButtonColor)
    {
        _missionButtonAnmator = missionButtonAnmator;
        _missionButtonImage = missionButtonImage;
        _activeButtonColor = activeButtonColor;
    }

    public override void Enter()
    {
        _missionButtonImage.gameObject.SetActive(true);
        _missionButtonImage.color = _activeButtonColor;
        _missionButtonImage.raycastTarget = true;

        if (_missionButtonAnmator != null)
            _missionButtonAnmator.enabled = true;
    }
}