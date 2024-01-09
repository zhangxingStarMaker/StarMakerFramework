using Module.FrameBase;
using UnityEngine;

namespace Runtime.CharacterModule.Runtime
{
    /// <summary>
    /// 注意这是管理器
    /// </summary>
    public class PlayerSystemEntity : CoreEntity,ICoreEntityAwake
    {
        public void OnAwake()
        {
            
        }

        public void OnStart()
        {
            InitMajorPlayer();
        }

        private void InitMajorPlayer()
        {
            GameObject roleGameObject = GameObject.Find("GameCenter/Role");
        }
    }
}