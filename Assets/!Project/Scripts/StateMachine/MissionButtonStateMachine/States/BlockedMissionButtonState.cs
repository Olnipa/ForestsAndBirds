using UnityEngine.UI;

public class BlockedMissionButtonState : MissionButtonState
{
    private Image _missionButtonImage;

    public BlockedMissionButtonState(Image missionButtonImage)
    {
        _missionButtonImage = missionButtonImage;
    }

    public override void Enter()
    {
        _missionButtonImage.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        _missionButtonImage.gameObject.SetActive(true);
    }
}
