using UnityEngine;

public class ChooseHeroMessage : MonoBehaviour
{
    [SerializeField] private MissionPanelsController _missionPanelHandler;

    private void Awake()
    {
        _missionPanelHandler.HeroIsNotChosen += EnableGameObject;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _missionPanelHandler.HeroIsNotChosen -= EnableGameObject;
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    private void EnableGameObject()
    {
        gameObject.SetActive(true);
    }
}