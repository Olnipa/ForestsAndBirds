using UnityEngine;
using UnityEngine.UI;

public class CompletedMissionButtonState : MissionButtonState
{
    private Color _completedButtonColor;
    private Animator _missionButtonAnmator;
    private Image _missionButtonImage;

    public CompletedMissionButtonState(Animator missionButtonAnmator, Image missionButtonImage, Color completedButtonColor)
    {
        _missionButtonAnmator = missionButtonAnmator;
        _missionButtonImage = missionButtonImage;
        _completedButtonColor = completedButtonColor;
    }

    public override void Enter()
    {
        if (_missionButtonAnmator != null)
            _missionButtonAnmator.enabled = false;

        _missionButtonImage.gameObject.SetActive(true);
        _missionButtonImage.color = _completedButtonColor;
        _missionButtonImage.raycastTarget = false;
    }

    public override void Exit()
    {

    }
}
