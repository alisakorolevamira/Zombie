using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MergdeCard : Card
{
    [SerializeField] private SitizensSpawner _spawner;
    private readonly int _requiredNumberOfSitizens = 2;

    public event UnityAction OnClicked;
    public override event UnityAction<int> CardBought;

    private protected override void OnEnable()
    {
        base.OnEnable();

        _spawner.NumberOfSitizensChanged += ChangeColor;
    }

    private protected override void OnDisable()
    {
        base.OnDisable();

        _spawner.NumberOfSitizensChanged -= ChangeColor;
    }

    private protected override void OnButtonClicked()
    {
        var playersMoneyInteractor = Game.GetInteractor<PlayersMoneyInteractor>();

        if (playersMoneyInteractor.Money() >= _price && _spawner.Sitizens.Count >= _requiredNumberOfSitizens)
        {
            OnClicked?.Invoke();
            CardBought?.Invoke(_price);
            base.OnButtonClicked();
        }
    }

    private protected override void ChangeColor()
    {
        var playersMoneyInteractor = Game.GetInteractor<PlayersMoneyInteractor>();

        if (playersMoneyInteractor.Money() >= _price && _spawner.Sitizens.Count >= _requiredNumberOfSitizens)
        {
            _image.DOColor(_currentColor, _timeOfChangingColor);
        }

        else
        {
            _image.DOColor(Color.white, _timeOfChangingColor);
        }
    }
}
