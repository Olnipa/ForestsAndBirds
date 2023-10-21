using UnityEngine;
using UnityEngine.UI;

public class CloseButtonSwitcher : MonoBehaviour
{
    [SerializeField] private bool _isEnable;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        if (_isEnable)
            _closeButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (_isEnable)
            _closeButton.gameObject.SetActive(false);
    }
}