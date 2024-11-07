public class MinionStructs
{
    /// <summary>
    /// csv에서 읽어온 카드 데이터 저장
    /// </summary>
    public struct CardData
    {
        public string cid;
        public string name;
        public MinionEnums.MINION_TYPE type;
        public MinionEnums.MINION_GRADE grade;
        public int loyaltyRate;
        public int stamina;
        public int attack;
        public int defence;
        public int produceSpeed;
        public int productYield;
        public int goodsProbability;
    }
}