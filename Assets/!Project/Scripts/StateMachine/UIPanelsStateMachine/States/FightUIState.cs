public class FightUIState : UIPanelsState
{
    protected FightPanel _fightPanel;

    public FightUIState(FightPanel fightPanel)
    {
        _fightPanel = fightPanel;
    }

    public override void Enter()
    {
        _fightPanel.EnablePanel();
    }

    public override void Exit()
    {
        _fightPanel.DisablePanel();
    }
}
