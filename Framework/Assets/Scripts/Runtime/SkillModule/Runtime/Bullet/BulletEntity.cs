using Module.FrameBase;
using UnityEngine;

namespace Runtime.SkillModule.Runtime.Bullet
{
    public class BulletEntity
    {
        /// <summary>
        /// 创建Bullet的入口
        /// </summary>
        /// <param name="bulletModel"></param>
        /// <param name="casterCoreEntity">创建子弹的负责人是谁，可以是null。创建的子弹的caster就是这个GameObject，
        /// 而propWhileCast则是读取此时caster下ChaState.property而来的，如果caster是null，propWhileCast则默认是一个全为0的属性，当然可以通过脚本来修改它</param>
        /// <param name="firePos"></param>
        /// <param name="fireDegree"></param>
        /// <param name="speed">子弹的飞行速度，这是子弹最基础的飞行速度，他乘以tween返回的移动力，就是最终子弹的移动速度了。</param>
        /// <param name="duration">这个子弹的生命周期，不同发射器发出来的同样的子弹，他的生命周期很可能是不同的，所以生命周期并不属于子弹本身，而是发射器决定的。</param>
        /// <param name="canHitAfterCreated">多久之后可以第一次碰撞目标，这也不是子弹的属性，而是发射器决定了子弹的这个“性能”。</param>
        public void BulletLauncher(BulletModel bulletModel,CoreEntity casterCoreEntity,Vector3 firePos,Vector3 fireDegree,float speed,float duration,bool canHitAfterCreated)
        {
            
        }
    }
}