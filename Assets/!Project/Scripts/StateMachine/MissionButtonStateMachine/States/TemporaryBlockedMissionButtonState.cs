using UnityEngine;
using UnityEngine.UI;

public class TemporaryBlockedMissionButtonState : MissionButtonState
{
    private Animator _missionButtonAnimator;
    private Image _missionButtonImage;

    private const string _temporaryBlockedTriggerAnimator = "TempBlocked";

    public TemporaryBlockedMissionButtonState(Animator missionButtonAnimator, Image missionButtonImage)
    {
        _missionButtonAnimator = missionButtonAnimator;
        _missionButtonImage = missionButtonImage;
    }

    public override void Enter()
    {
        _missionButtonAnimator.SetTrigger(_temporaryBlockedTriggerAnimator);
        _missionButtonImage.raycastTarget = false;
    }

    public override void Exit()
    {
        _missionButtonAnimator.ResetTrigger(_temporaryBlockedTriggerAnimator);
        _missionButtonImage.raycastTarget = true;
    }
}