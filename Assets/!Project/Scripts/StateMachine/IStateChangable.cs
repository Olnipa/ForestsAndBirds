using System.Collections.Generic;

public interface IStateChangable
{
    public void SetNewState(MissionState newState);
}