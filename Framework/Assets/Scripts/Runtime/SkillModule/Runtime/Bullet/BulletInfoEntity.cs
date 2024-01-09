using BuffModule.Runtime;
using Module.FrameBase;

namespace Runtime.SkillModule.Runtime.Bullet
{
    public class BulletInfoEntity : CoreEntity,ICoreEntityAwake
    {
        public BulletModel BulletModel;
        
        /// <summary>
        /// 施法者
        /// </summary>
        public CoreEntity Caster;

        /// <summary>
        /// 子弹的施法者在创建子弹时候的角色属性，因为在《魔兽世界》中有个类似设定就是命中时候造成的伤害和释放的时候角色属性有关，
        /// 技能放出去（利用子弹发射到命中的时间差）以后换装备，并不会影响技能的最后效果，于是我们也记录一个这个属性在子弹中，
        /// 作为创建时候的施法者属性。但是依然，这解决不了子弹命中时候，角色身上的某些buff已经被移除了，以至于一些效果无法生效，
        /// 因此是否需要记录一个List<BuffObj> buffsWhileCast，就看游戏具体需要了，但是通常来说，就连propWhileCast，都是不需要记录的，
        /// 因为在设计层，这个几乎没有必要。当然既然这里说的是实现层，实现层不可以否定设计，不能提出“设计不合理”（这是原则），因此我们这里只说要去做的时候的实现方式。
        /// </summary>
        public ChaProperty PropWhileCast;

        /// <summary>
        /// 子弹还能命中几个目标，每次命中（碰撞到）一个目标-1。
        /// </summary>
        public uint Hp;

        /// <summary>
        /// 子弹也有很多的时间参数，duration是生命周期，和buff的duration一样概念；timeElapsed也是记录子弹存在了多久了，因为duration可能被改动，但是这个只能持续增加，
        /// 如果我们要做一个技能效果是“子弹随着飞行距离越远伤害越大”，则完全可以直接用这个时间作为参数（毕竟时间乘以速度=距离）；
        /// canHitAfterCreated是一个特殊的值，如果这个值>0，子弹就不会碰撞任何单位（但不包括地形），因为我们有些时候比如设计了一个“子母弹：
        /// 打中目标后分裂出3个小子弹”，这时候分裂出的子弹立即就会碰撞到母弹碰撞（命中）的目标，因此需要加一个时间，确保短时间子弹不会碰到目标，
        /// 可以“让子弹飞一会”了。当子弹的duration为0的时候，子弹会被移除，此时会进入OnRemoved回调点，如果子弹的model.removeOnObstacled是false的，
        /// 那么就只有duration<=0才会触发OnRemoved了。
        /// </summary>
        public float Time;

        /// <summary>
        /// 子弹的移动信息,这里的核心是tween；fireDegree和useFireDegreeForever是创建时候的参数，用于传递做tween的参数；
        /// velocity是当前一帧的移动速度和方向信息，也是给tween函数（或者其他回调点）使用的只读参数；
        /// moveType和smoothMove其实是创建时候model里面的moveType和removeOnObstacled赋值而来，因为这两项变化频率会非常高，
        /// 还有“还原”的需求，所以尽量不动model的值，而直接用这个——比如一个手雷，他在飞起来的时候moveType应该是fly的，落地的时候是ground的；
        /// followingTarget则是子弹跟踪的一个目标，比如“跟踪导弹”、“回力标”的tween就会用到这个作为参数。在这里，Tween是一个策划编写的脚本函数
        /// </summary>
        public string Movement;

        /// <summary>
        /// 弹的命中纪录，正如我们上面说的，一个子弹击中一个目标后，一定时间内是不能再击中这个角色的，要做到这个效果，我们得有一条数据，
        /// 纪录击中了谁，多久之后才能再次击中，然后每帧减少这个时间，当这个时间<=0之后清理数据，就可以再次击中目标了
        /// </summary>
        public CoreEntity[] HitRecordsEntities;
        
        
        public void OnAwake()
        {
            
        }
    }
}