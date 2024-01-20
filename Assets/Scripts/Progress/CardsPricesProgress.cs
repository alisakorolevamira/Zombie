namespace Scripts.Progress
{
    public class CardsPricesProgress
    {
        public int AddSitizenCardPrice;
        public int MergeCardPrice;
        public int AddSpeedCardPrice;
        public int DoubleRewardCardPrice;

        public CardsPricesProgress(int addSitizenCardPrice, int mergeCardPrice, int addSpeedCardPrice, int doubleRewardCardPrice)
        {
            AddSitizenCardPrice = addSitizenCardPrice;
            MergeCardPrice = mergeCardPrice;
            AddSpeedCardPrice = addSpeedCardPrice;
            DoubleRewardCardPrice = doubleRewardCardPrice;
        }
    }
}
