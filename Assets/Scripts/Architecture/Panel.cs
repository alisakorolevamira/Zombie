using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class Panel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Button[] _buttons;

    public virtual void Initialize() { }
   
    public virtual void OnStart() 
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _buttons = GetComponentsInChildren<Button>();

        Close();
    }

    public virtual void Open()
    {
        _canvasGroup.alpha = 1;

        if (_buttons != null)
        {
            foreach (var button in _buttons)
            {
                button.interactable = true;
            }
        }
    }

    public virtual void Close()
    {
        _canvasGroup.alpha = 0;

        if (_buttons != null)
        {
            foreach (var button in _buttons)
            {
                button.interactable = false;
            }
        }
    }

    public Panel GetFromPrefab(Panel prefab)
    {
        var panel = Instantiate(prefab, transform, false);
        return panel;
    }
}
