using System.Collections.Generic;
using Module.FrameBase;

namespace Runtime.SkillModule.Runtime.Aoe
{
    public class AoeExecute
    {
        /// <summary>
        /// 在AoE创建的时候被执行
        /// 在执行这个函数的时候，AoeState已经完成了一次捕获，也就是已经获取了范围内的角色和子弹了，因此是可以利用aoe.GetComponent<AoeState>().characterInRange
        /// 等数据来进行对范围内角色造成伤害等处理的。
        /// 这个回调点的用法之一是“创建一个法阵，3秒后对法阵内的所有人造成伤害，如果目标在法阵创建时就已经在法阵之中，那就会受到双倍伤害”，类似这样的效果，
        /// 就是在OnCreate的时候给角色添加一个buff，OnRemoved的时候造成伤害，并判断是否有这个buff，有就是双倍伤害。
        /// </summary>
        /// <param name="aoe">施法者</param>
        public delegate void AoeOnCreate(CoreEntity aoe); 
        
        /// <summary>
        /// 在AoE被移除的时候执行，AoE被移除的渠道通常是duration<=0，如果AoE会碰撞，那么碰撞后也会发生被移除的事件
        /// 这是最常用于伤害性法术的时间点，因为我们通常需要伤害性法术，比如爆炸效果有一个延迟时间，这个延迟时间也就是aoe的duration了。比如我们要做个“4秒后会自动爆炸，
        /// 玩家也可以手动引爆，对范围内的敌人造成伤害”，那么就是OnRemoved里面写“对范围内的敌人造成伤害”的效果，而至于AoE是如何被移除的，比如用了一个技能duration直接变0了，
        /// 那根AoE本身没有任何关系。
        /// 例如船厂的爆炸桶的设计,当然爆炸桶分为引爆和自动消失
        /// </summary>
        /// <param name="aoe"></param>
        public delegate void AoeOnRemoved(CoreEntity aoe);
        
        /// <summary>
        /// 是每一个Tick执行的事件，同样需要AoeModel.tickTime > 0才可能被执行
        /// 这也是一个极为经典的回调点，他的经典在于类似《魔兽世界》中法师的暴风雪等，知道今天都还是用这个回调点来实现的，而早年的光环技能，也是通过这个回调点不断给角色上一个buff。
        /// </summary>
        /// <param name="aoe"></param>
        public delegate void AoeOnTick(CoreEntity aoe);
        
        /// <summary>
        /// 当有新的角色进入的时候触发
        /// 其中参数List<GameObject> cha就是这一帧进入这个范围的角色们（的GameObject），而在此时，aoe的AoeState.characterInRange中还没有这些角色。
        /// 当每次范围内的角色离开范围后再次进入范围，就会再次执行这个。所以假如我们需要做一个效果是“对进入范围的目标造成50点伤害，但是短时间内不会再次伤害”，
        /// 遇到有角色在这个AoE范围里“我出来了，我又进来了，我出来了，我又进来了”，就需要通过buff来辅助，其执行的函数是——没有某个buff的时候会造成伤害并添加buff，
        /// 持续时间为这句说明中的“短时间”，否则return。当然我们也可以利用AoeState.param来做类似bullet.hitRecord的事情，比如param["hitRecord"]=new List<hitRecord>()，
        /// 然后通过OnTick事件来维护这个List也是一个做法，只是相比之下，上一个buff的性能会好不少，但是如果太多这样的类似效果的话，
        /// 我们就不得不审视这个项目是否需要一个aoeHitRecord了（做法是活得，而不是因为框架如此就动不得了）
        /// </summary>
        /// <param name="aoe"></param>
        /// <param name="cha"></param>
        public delegate void AoeOnCharacterEnter(CoreEntity aoe, List<CoreEntity> cha);
        
        /// <summary>
        /// 当角色离开aoe范围的时候会执行的事情
        /// 这里的cha是这一帧刚刚离开离开AoeState.characterInRange的角色，他们已经不在characterInRange里了。
        /// 这个回调点的用法通常是光环类效果的buff移除等，但是这里有一个经典的用法——就是《魔兽世界》中卡拉赞（70级时代）艾兰的烈焰花环——“随机挑选3个玩家，在他们脚下生成烈焰花环，
        /// 如果有人进入或者离开了烈焰花环，就会对全团造成伤害”。我们看到这里有几个彻底发挥AoE性质的点:
        /// 1.首先是OnCharacterEnter和OnCharacterLeave，只要cha的长度变化，就会对全团产生伤害
        /// 2.其次是如何产生这个伤害，其问题不是调用伤害脚本，而是“谁”该收到伤害，这里有一个全团，尽管“艾兰的房间”，也就是战斗的场地是一个足够小的场景，所以可以直接丢一个aoe覆盖范围，但是我们更应该去做的是——AoE的范围内，包括了“正在副本中的所有玩家”这个range。
        /// 而这个“老狗也有几颗牙”的技能，也正是aoe绝妙之处所在——你也许发现了，在aoe和buff中，都没有角色OnMove的回调，事实上这个回调点触发频率过于频繁，是不建议存在的，它与OnTick最大的不同是，他要判断移动过了，这是一个并不如想象的那么简单的判断
        /// </summary>
        /// <param name="aoe"></param>
        /// <param name="cha"></param>
        public delegate void AoeOnCharacterLeave(CoreEntity aoe, List<CoreEntity> cha);
        
        /// <summary>
        /// 这是一个与角色对应的，子弹进入aoe范围的回调点,
        /// 例如 永劫中的达摩的F技能，给自己一个防御范围
        /// </summary>
        /// <param name="aoe"></param>
        /// <param name="bullet"></param>
        public delegate void AoeOnBulletEnter(CoreEntity aoe, List<CoreEntity> bullet);
        
        /// <summary>
        /// 与角色离开对应的是子弹离开的回调点,除了离开减速立场的子弹需要把移动速度设置回去之外，还可以做很多有意思的设计，比如角色有一个被动效果是“角色发射出子弹，
        /// 在近距离（5米内）是带火焰的，离开后火焰效果消失”，那么就是有一个由角色创建的aoe，他的tween是跟随这个caster的，这个aoe的OnBulletLeave中，
        /// 会改写bullet的外观和OnHit等，起到改变子弹性能和视觉效果的作用
        /// </summary>
        /// <param name="aoe"></param>
        /// <param name="bullet"></param>
        public delegate void AoeOnBulletLeave(CoreEntity aoe, List<CoreEntity> bullet);
    }
}