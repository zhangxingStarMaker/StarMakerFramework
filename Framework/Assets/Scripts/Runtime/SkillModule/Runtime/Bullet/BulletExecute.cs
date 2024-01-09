using Module.FrameBase;

namespace Runtime.SkillModule.Runtime.Bullet
{
    public class BulletExecute
    {
        /// <summary>
        /// 这个回调点的使用率其实是非常低的，但并不是没有，比如我们需要让“子弹为角色添加buff”，这时候就需要走这个回调点。
        /// “子弹为角色添加buff”这个工作，通常是希望角色同步子弹信息才有的，比如我们发射出一个飞弹（子弹），这时候可以再次使用飞弹技能来引爆这个飞弹，
        /// 那角色就得知道飞弹是否存在，因此就得知道具体飞弹是哪个GameObject，此时，我们就在飞弹OnCreate时，给角色添加一个buff，buff的param中存飞弹的信息；
        /// 判断的时候只要判断这个buff的这个参数指向的GameObject是否存在以及他的状态就行了
        /// </summary>
        /// <param name="bullet"></param>
        public delegate void BulletOnCreate(CoreEntity bullet);
        
        /// <summary>
        /// 子弹命中目标的回调，通常玩家理解的“子弹效果”就是写在这里面的，确切地说，是子弹效果的大约70%写在这里
        /// </summary>
        /// <param name="bullet"></param>
        /// <param name="target"></param>
        public delegate void BulletOnHit(CoreEntity bullet, CoreEntity target);
        
        /// <summary>
        /// 之所以说OnHit只是玩家理解的子弹效果的70%，是因为30%在这里，就是子弹被移除的时候，我们通常会忽略这个时间点——它发生在子弹“自然消失”的时候，比如撞到了墙壁或者生命周期终止
        /// 比如我们的子弹效果是“命中目标之后分裂出4个小的子弹向子弹命中的反方向飞去”，那么这个效果在OnHit的时候，需要写的脚本里就要包括造成伤害和产生4个BulletLauncher发射小子弹。
        /// 而这个效果本身，策划心目中的理解也是“如果碰到墙壁，肯定也得分出来4个小子弹”，因此我们就得在OnRemoved里面也写这个效果（只是伤害的部分没有了），
        /// 但是因为OnRemoved会被碰撞和生命周期走完触发，所以我们还要判断bullet.GetComponent<BulletState>().duration是否还>0，如果是，说明是碰撞到墙壁的，就该执行，
        /// 否则是自然的消失了，就不会创建这4个BulletLauncher了。
        /// </summary>
        /// <param name="bullet"></param>
        public delegate void BulletOnRemoved(CoreEntity bullet);
    }
}