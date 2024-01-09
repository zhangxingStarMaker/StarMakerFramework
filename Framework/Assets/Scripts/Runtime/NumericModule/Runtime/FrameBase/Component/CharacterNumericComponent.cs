namespace Runtime.NumericModule.Runtime
{
    public class CharacterNumericComponent : NumericComponent
    {
        public override void OnAwake()
        {
            base.OnAwake();

            this[BattleNumericType.HpBase] = 1000;
            this[BattleNumericType.HpAdd] = 0;
            this[BattleNumericType.HpPct] = 0;
            this[BattleNumericType.HpFinalAdd] = 0;
            this[BattleNumericType.HpFinalPct] = 0;
            
            this[BattleNumericType.SpeedBase] = 5;
            this[BattleNumericType.SpeedAdd] = 0;
            this[BattleNumericType.SpeedPct] = 0;
            this[BattleNumericType.SpeedFinalAdd] = 0;
            this[BattleNumericType.SpeedFinalPct] = 0;
        }
    }
}