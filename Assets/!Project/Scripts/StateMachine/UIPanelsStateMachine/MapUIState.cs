public class MapUIState : UIPanelsState
{
    protected IUICloser _uiCloser;

    public MapUIState(IUICloser uiCloser)
    {
        _uiCloser = uiCloser;
    }

    public override void Enter()
    {
        _uiCloser.CloseAllPanels();
    }
}