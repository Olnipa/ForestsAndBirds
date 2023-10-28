using UnityEngine;
using UnityEngine.UI;

public class CompletedMissionButtonState : MissionButtonState
{
    private Animator _missionButtonAnimator;
    private Image _missionButtonImage;

    private const string _completedTriggerAnimator = "Completed";

    public CompletedMissionButtonState(Animator missionButtonAnimator, Image missionButtonImage)
    {
        _missionButtonAnimator = missionButtonAnimator;
        _missionButtonImage = missionButtonImage;
    }

    public override void Enter()
    {
        _missionButtonAnimator.SetTrigger(_completedTriggerAnimator);
        _missionButtonImage.raycastTarget = false;
    }

    public override void Exit()
    {
        _missionButtonAnimator.ResetTrigger(_completedTriggerAnimator);
        _missionButtonImage.raycastTarget = true;
    }
}
