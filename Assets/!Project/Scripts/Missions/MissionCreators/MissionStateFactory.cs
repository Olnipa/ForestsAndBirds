using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionStateFactory
{
    private Animator _animator;
    private Image _image;

    public MissionStateFactory(Animator buttonAnimator, Image buttonImage)
    {
        _animator = buttonAnimator;
        _image = buttonImage;
    }

    public MissionButtonState CreateState(MissionState state)
    {
        switch (state)
        {
            case MissionState.Active:
                return new ActiveMissionButtonState(_image);
            case MissionState.Blocked:
                return new BlockedMissionButtonState(_image); 
            case MissionState.TemporaryBlocked:
                return new TemporaryBlockedMissionButtonState(_animator, _image); 
            case MissionState.Completed:
                return new CompletedMissionButtonState(_animator, _image); 
            default:
                throw new ArgumentException($"Unsupported Default State {state}");
        }
    }
}
