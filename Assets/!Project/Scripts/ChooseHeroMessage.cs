using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChooseHeroMessage : MonoBehaviour
{
    [SerializeField] private float _secondsToCloseMessage = 2f;
    [SerializeField] MissionPanelHandler _missionPanelHandler;

    private Animator _animator;
    private Coroutine _closeMessageCoroutine;

    private const string disappearAnimation = "Close";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _missionPanelHandler.HeroIsNotChosen += EnableGameObject;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _closeMessageCoroutine = StartCoroutine(CloseMessageJob());
    }

    private void EnableGameObject()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator CloseMessageJob()
    {
        yield return new WaitForSeconds(_secondsToCloseMessage);
        _animator.SetTrigger(disappearAnimation);
    }

    private void OnDisable()
    {
        if (_closeMessageCoroutine == null)
            return;

        StopCoroutine(_closeMessageCoroutine);
    }

    private void OnDestroy()
    {
        _missionPanelHandler.HeroIsNotChosen -= EnableGameObject;
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
