using Scripts.Architecture.Services;
using Scripts.Progress;

public class CardsPricesProgressService : ICardsPricesProgressService
{
    private const string AddSitizenCardPrice = "AddSitizenCardPrice";
    private const string MergeCardPrice = "MergeCardPrice";
    private const string AddSpeedCardPrice = "AddSpeedCardPrice";
    private const string DoubleRewardCardPrice = "DoubleRewardCardPrice";
    private readonly int _defaultAddSitizenCardPrice = 10;
    private readonly int _defaultMergeCardPrice = 50;
    private readonly int _defaultAddSpeedCardPrice = 40;
    private readonly int _defaultDoubleRewardCardPrice = 100;
    private readonly ISaveLoadService _saveLoadService;

    public CardsPricesProgress Progress { get; private set; }

    public CardsPricesProgressService(ISaveLoadService saveLoadService)
    {
        _saveLoadService = saveLoadService;
    }

    public void LoadProgress()
    {
        int addSitizenCardPrice = _saveLoadService.LoadProgress(AddSitizenCardPrice, _defaultAddSitizenCardPrice);
        int mergeCardPrice = _saveLoadService.LoadProgress(MergeCardPrice, _defaultMergeCardPrice);
        int addSpeedCardPrice = _saveLoadService.LoadProgress(AddSpeedCardPrice, _defaultAddSpeedCardPrice);
        int doubleRewardCardPrice = _saveLoadService.LoadProgress(DoubleRewardCardPrice, _defaultDoubleRewardCardPrice);

        Progress = new CardsPricesProgress(addSitizenCardPrice, mergeCardPrice, addSpeedCardPrice, doubleRewardCardPrice);
    }

    public void SaveProgress()
    {
        _saveLoadService.SaveProgress(AddSitizenCardPrice, Progress.AddSitizenCardPrice);
        _saveLoadService.SaveProgress(MergeCardPrice, Progress.MergeCardPrice);
        _saveLoadService.SaveProgress(AddSpeedCardPrice, Progress.AddSpeedCardPrice);
        _saveLoadService.SaveProgress(DoubleRewardCardPrice, Progress.DoubleRewardCardPrice);
    }

    public void ResetProgress()
    {
        _saveLoadService.ResetProgress(AddSitizenCardPrice);
        _saveLoadService.ResetProgress(MergeCardPrice);
        _saveLoadService.ResetProgress(AddSpeedCardPrice);
        _saveLoadService.ResetProgress(DoubleRewardCardPrice);
    }
}
