using UnityEngine;

public class ChooseHeroMessage : MonoBehaviour
{
    private StartMissionInitializer _panelsController;

    public void Initialize(StartMissionInitializer panelsController)
    {
        _panelsController = panelsController;
        _panelsController.HeroIsNotChosen += EnableGameObject;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _panelsController.HeroIsNotChosen -= EnableGameObject;
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