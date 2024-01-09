using System.Collections.Generic;
using BuffModule.Runtime;
using Module.FrameBase;

namespace Runtime.SkillModule.Runtime.Aoe
{
    public class AoeInfoEntity
    {
        /// <summary>
        /// 数据
        /// </summary>
        public AoeModel AoeModel;

        /// <summary>
        /// 施法半径，这个可以用制定一些规则，如果是方形需要长和宽
        /// </summary>
        public float Radius;

        /// <summary>
        /// 持续时间
        /// </summary>
        public float DurationTime;

        /// <summary>
        /// 剩余时间
        /// </summary>
        public float TimeElapsed;

        /// <summary>
        /// 释放者
        /// </summary>
        public CoreEntity Caster;
        
        /// <summary>
        /// 释放时 角色的属性
        /// </summary>
        public ChaProperty CasterProperty;

        /// <summary>
        /// 捕捉到的角色
        /// </summary>
        public List<CoreEntity> CharacterListInRange;
        
        /// <summary>
        /// 捕捉到的子弹
        /// </summary>
        public List<CoreEntity> BulletListInRange;

        /// <summary>
        /// 移动信息
        /// </summary>
        public string Movement;
    }
}