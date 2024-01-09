using Module.FrameBase;
using Runtime.SkillModule.Runtime;
using UnityEngine;

namespace BuffModule.Runtime
{
    public abstract class BuffExecute
    {
        public abstract void OnExecute(BuffInfoEntity buffInfoEntity,DamageInfo damageInfo = null);
    }

    /// <summary>
    /// 回调发生在创建时，也就是刚刚挂上此Buff的时候
    /// 这个回调点在大多游戏中，核心的作用和视觉管理更接近，比如我们设计一个“猎人印记”的技能，它的效果是给目标添加一个标记，使之受到的伤害提高。
    /// 在OnOccur的时候，我们执行一个脚本，为角色的指定绑点添加一个视觉特效，这表示角色被标记了。当然为什么播放视觉特效是在OnOccur而非一个Buff的标准属性呢？
    /// 因为绝大多数的buff完全是不存在视觉特效，甚至因为UI上连Icon都没有，以至于玩家根本不知道他们的存在，
    /// 但实战中，尤其是大型项目比如MMORPG、Moba中，玩家真正能够感知到的buff，占游戏所有设计出来的buff的10%都不到，如果可见buff占比超过50%，
    /// 那说明这个游戏的“花头”实在太少了，做内容设计（比如技能设计）的策划要好好加油了。
    /// 而除了视觉管理，也有一些很有意思的效果会用到这个。比如我们设计一个角色受到伤害的时候会狂暴，狂暴最多100层，每层狂暴提高1%伤害，当狂暴大于30层的时候，
    /// 角色进入“忘我状态”，受到伤害提高100%，但是攻击必定暴击。在这个效果里面，涉及到了3个buff，第一个buff是受到伤害的时候叠加这个狂暴buff，
    /// 狂暴buff的属性变化中有攻击力提升，他的OnOccur里面就会计数——如果参数buff.stack>=30，那么就自己给自己添加一层“忘我状态”这个buff，这个buff最大层数只有1，
    /// 持续时间是永久的，因此Occur的时候不论出发多少次，都是1层；而当buff.stack<30的时候，就会删除这层 "忘我状态”，从而做到这个效果。
    /// </summary>
    public delegate void BuffOnOccur(BuffInfoEntity buffInfoEntity, int modStack);

    /// <summary>
    /// 在一个buff因为生命周期结束，或者层数<=0的时候，他要被移除掉之前，会执行这个函数,
    /// 这个回调点并不会在角色被击败的时候触发。他的使用范围除了移除掉OnOccur添加的视觉特效之外，还有很多逻辑作用，比如我们设计“在角色身上绑一个定时炸弹，10秒后爆炸，
    /// 对角色和身边所有的友军造成伤害”，这就是一个持续10秒的buff，在开始的时候为角色身上某个绑点添加一个炸弹的视觉特效，然后在OnRemoved中，将这个特效移除掉，
    /// 同时产生一个AoE，就是爆炸的AoE，对AoE范围内的所有符合条件（buff.carrier同阵营的）造成伤害。
    /// 最终这个buff被移除的时候是否需要添加一个新的buff取决于策划需求
    /// </summary>
    /// <param name="buffInfoEntity"></param>
    /// <param name="dispeller">如果buff被提前终止，需要传入执行的对象</param>
    public delegate bool BuffOnRemoved(BuffInfoEntity buffInfoEntity,CoreEntity dispeller = null);

    /// <summary>
    /// 间歇性的执行效果，
    /// 这是最常见的buff效果“每一跳”的回调点，我们通常所说的“间歇性效果”，比如“灼烧：每3秒对目标造成50点火焰伤害”等，其执行伤害的点都在这个OnTick
    /// 在我们写OnTick的回调脚本的时候，我们也会用到buff中一些记录用的参数，比如ticked（执行过多少次OnTick了），为什么需要这么一个参数。因为比如在《魔兽世界》中，
    /// 有一个叫“痛苦诅咒”的技能，它产生一个间歇性伤害（DoT），这个间歇性伤害每2秒对角色造成一次暗影伤害，造成的伤害数值会逐渐变大，比如第一次是100，第二次就是120，
    /// 第三次150……类似这样的提高，直到一个最高峰值，在这里，我们就要依赖ticked来算出当前的伤害值。
    /// </summary>
    /// <param name="buffInfoEntity"></param>
    public delegate void BuffOnTick(BuffInfoEntity buffInfoEntity);

    /// <summary>
    /// 这是在角色释放技能的时候发生的回调
    /// 在这里我们写一些逻辑最后返回一个要释放出去的“技能效果”，从而达成一个Hack了技能效果的过程。
    /// 这个回调点的具体使用情景非常多，我们这里就随意举2个例子，首先我们确定我们有一个技能是翻滚——这在顶视角射击游戏很常见，
    /// 就是角色往一个方向翻滚，可以躲避子弹，不仅有位移，还有位移过程中也会有无敌时间，它的技能时间点大致是这样的:
    /// 1: 0秒时，角色开始做翻滚动画，同时停止角色的移动、转身、使用技能权限（这并不等于角色不能动了，只是不接受移动、旋转、放技能指令了）。
    /// 2: 0.1秒时，角色开始向指定方向发生位移，翻滚肯定得往一个方向滚出去才行。
    /// 3: 0.4秒时，角色获得一个0.2秒的无敌时间（不会被子弹命中），这里是一个短暂无敌，更有助于玩家躲避子弹。
    /// 基于这样一个效果的技能，策划设计了一些“被动技能”、“装备效果”、“天赋点数”等等的元素，去让这个技能要发生一点变化，比如我们说“随便举2个例子”：
    /// 1:角色翻滚的无敌时间变长：实际上是角色现在从0.3秒开始获得一个持续0.4秒的无敌时间，因此，我们只需要把传递进来的skillEffect的第三个节点的内容修改一下，返回出去就完事儿了
    /// 2:翻滚时，会在地上留下一个地雷：这其实是个非常酷的感觉，所谓“离别礼物”嘛，那他如何实现？其实就是在2和3之间插入一个——0.1秒时，
    /// 创建一个aoe（如果地雷决定用aoe实现的话，用什么实现是看具体想要什么效果定的），就这样，这个效果也就轻松实现了.
    /// 举例：例如LOL中剑魔开大招的时候给自己上一个buff，会减少Q技能的冷却时间。
    /// </summary>
    /// <param name="buffInfoEntity">执行的buffInfo</param>
    /// <param name="skillInfoEntity">当前释放的技能</param>
    /// <param name="effect">此技能效果</param>
    public delegate void BuffOnCast(BuffInfoEntity buffInfoEntity, SkillInfoEntity skillInfoEntity,GameObject effect);

    /// <summary>
    /// 当技能命中时触发的buff，命中指的是碰撞盒产生了碰撞
    /// 在这里第二个参数DamageInfo就是我们在整个伤害流程中不断传递的那个信息，因此需要ref有进有出，而target则是被攻击的目标的GameObject，
    /// 基于target和damageInfo以及buff本身，我们可以做很多逻辑来实现很多效果，比如：攻击时有30%的几率触发额外一次攻击：这里就是OnHit中投个[0,100)的随机数，
    /// 如果结果<30，就创建一个DamageInfo来作为“额外一次攻击”。再比如：如果攻击的目标是异性，则伤害提高30%，
    /// 就是判断buff.carrier（buff的携带者）与target是否为异性（有类似gender的属性决定的），如果是就把DamageInfo.damage全都乘以1.3。
    /// 例如：EZ大招每碰到一个角色，伤害就会减弱一次。
    /// </summary>
    /// <param name="buffInfoEntity"></param>
    /// <param name="damageInfo"></param>
    /// <param name="targetEntity"></param>
    public delegate void BuffOnHit(BuffInfoEntity buffInfoEntity, ref DamageInfo damageInfo, CoreEntity targetEntity);

    /// <summary>
    /// 与OnHit相呼应的是，这是受到攻击时候触发的逻辑，如果说OnHit是主动攻击的效果，那么BeHurt就是被动挨打时候的效果
    /// 在我们实际的游戏设计中，经常有类似“受到伤害降低20%”、“受到的火焰伤害转化为治疗”、“反弹受到的直接伤害的20%给攻击者”、“如果受到暴击就会恢复生命值”之类的设定
    /// ，这些往往都是在BeHurt中实现的.
    /// 例如 永劫里面太白的防御技能，当添加此技能的buff后，可以减伤。
    /// </summary>
    /// <param name="buffInfoEntity"></param>
    /// <param name="damageInfo"></param>
    /// <param name="attackerEntity"></param>
    public delegate void BuffOnHurt(BuffInfoEntity buffInfoEntity, ref DamageInfo damageInfo,
        CoreEntity attackerEntity);

    /// <summary>
    /// 在确定会击败对手的时候执行的回调
    /// 尽管看起来在这里DamageInfo已经没有意义了，因为无力回天了，但我们依然可以访问到他，
    /// 是为了实现一些类似“如果最后一击伤害大于目标生命的30%，会获得额外50%的经验”之类的效果。
    /// 在这个回调点实现的主要效果比如“击败任何敌人后都会获得1个金币”、“击败敌人时任务进度提升”、“击败敌人时会发生爆炸对周围敌人产生伤害”等等效果。
    /// </summary>
    /// <param name="buffInfoEntity"></param>
    /// <param name="damageInfo"></param>
    /// <param name="targetEntity"></param>
    public delegate void BuffOnKill(BuffInfoEntity buffInfoEntity, DamageInfo damageInfo, CoreEntity targetEntity);

    /// <summary>
    /// 在确定会被击败之后执行的回调
    /// 这里通常会去实现的效果类似“角色被击败时会发生爆炸”、“角色被诅咒了，如果角色被击败，这个诅咒将扩散到附近的友方身上”、“击败这个目标可以获得一枚勋章”之类的效果。
    /// </summary>
    /// <param name="buffInfoEntity"></param>
    /// <param name="damageInfo"></param>
    /// <param name="attackerEntity"></param>
    public delegate void OnBeKilled(BuffInfoEntity buffInfoEntity, DamageInfo damageInfo, CoreEntity attackerEntity);

}