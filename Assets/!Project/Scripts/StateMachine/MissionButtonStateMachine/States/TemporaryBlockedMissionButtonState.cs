using UnityEngine;
using UnityEngine.UI;

public class TemporaryBlockedMissionButtonState : MissionButtonState
{
    private Color _temporaryBlockedButtonColor;
    private Animator _missionButtonAnmator;
    private Image _missionButtonImage;

    public TemporaryBlockedMissionButtonState(Animator missionButtonAnmator, Image missionButtonImage, Color temporaryBlockedButtonColor)
    {
        _missionButtonAnmator = missionButtonAnmator;
        _missionButtonImage = missionButtonImage;
        _temporaryBlockedButtonColor = temporaryBlockedButtonColor;
    }

    public override void Enter()
    {
        if (_missionButtonAnmator != null)
            _missionButtonAnmator.enabled = false;

        _missionButtonImage.gameObject.SetActive(true);
        _missionButtonImage.color = _temporaryBlockedButtonColor;
        _missionButtonImage.raycastTarget = false;
    }

    public override void Exit()
    {

    }
}
