using Module.FrameBase;
using Runtime.CharacterModule.Runtime.Base;
using Runtime.NumericModule.Runtime;

namespace Runtime.CharacterModule.Runtime
{
    public class UnitCharacterEntity : UnitCoreEntity,ICoreEntityAwake
    {
        private CharacterNumericComponent _numericComponent;

        public CharacterNumericComponent NumericComponent => _numericComponent;

        public void OnAwake()
        {
            _numericComponent = AddComponent<CharacterNumericComponent>();
        }
    }
}