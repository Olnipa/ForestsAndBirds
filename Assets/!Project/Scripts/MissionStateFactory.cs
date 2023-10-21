using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionStateFactory
{
    private Animator _animator;
    private Image _image;

    private Color _activeButtonColor;
    private Color _temporaryBlockedButtonColor;
    private Color _completedButtonColor;

    public MissionStateFactory(Animator buttonAnimator, Image buttonImage, Color activeColor, Color temporaryBlockedColor, Color completedColor)
    {
        _animator = buttonAnimator;
        _image = buttonImage;
        _activeButtonColor = activeColor;
        _temporaryBlockedButtonColor = temporaryBlockedColor;
        _completedButtonColor = completedColor;
    }

    public MissionButtonState GetState(MissionState state)
    {
        switch (state)
        {
            case MissionState.Active:
                return new ActiveMissionButtonState(_animator, _image, _activeButtonColor);
            case MissionState.Blocked:
                return new BlockedMissionButtonState(_image); 
            case MissionState.TemporaryBlocked:
                return new TemporaryBlockedMissionButtonState(_animator, _image, _temporaryBlockedButtonColor); 
            case MissionState.Completed:
                return new CompletedMissionButtonState(_animator, _image, _completedButtonColor); 
            default:
                throw new ArgumentException($"Unsupported Default State {state}");
        }
    }
}
