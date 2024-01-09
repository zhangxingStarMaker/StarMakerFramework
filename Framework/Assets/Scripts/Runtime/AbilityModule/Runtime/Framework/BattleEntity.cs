using Module.FrameBase;
using Runtime.CharacterModule.Runtime;
using UnityEngine;

namespace Runtime.AbilityModule.Runtime.Framework
{
    public class BattleEntity : CoreEntity,ICoreEntityAwake
    {
        private PlayerSystemEntity _playerSystemEntity;
        public void OnAwake()
        {
            _playerSystemEntity = AddComponent<PlayerSystemEntity>();
        }

        /// <summary>
        /// 战斗开始的地方，一些初始化工作放在这
        /// </summary>
        public void OnStart()
        {
            _playerSystemEntity?.OnStart();
        }
    }
}