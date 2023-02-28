using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class Panel : MonoBehaviour
{
    [SerializeField] private protected Button _button;
    [SerializeField] private Panel _panel;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _button.interactable = false;
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _button.interactable = true;
        _panel.gameObject.SetActive(false);
    }
}