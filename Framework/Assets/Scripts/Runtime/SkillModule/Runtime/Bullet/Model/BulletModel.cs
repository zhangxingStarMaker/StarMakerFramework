namespace Runtime.SkillModule.Runtime.Bullet
{
    public class BulletModel
    {
        /// <summary>
        /// 资源路径
        /// </summary>
        public string PrefabPath;

        /// <summary>
        /// 碰撞半径，如果是3d游戏中，可以用胶囊体来处理
        /// </summary>
        public float Radius;

        /// <summary>
        /// 一个子弹名字多少次后才会销毁，可以理解为字段的“贯穿力”，
        /// 例如LOL中EZ的大招，可以命中多次，再如EZ的Q技能，命中一次就消失
        /// </summary>
        public uint HitTimes;

        /// <summary>
        /// 因为可以命中多次，同时因为碰撞判定每一帧都在发生，由此我们需要一个延迟——当第一次命中一个角色后，
        /// 得经过多久才能第二次命中，这中间的时候碰撞不到这个角色。我们可以想象有一个体型非常大的敌人，我们有一发子弹贯穿力很好，
        /// 可以命中100次才消失，那么他在路线上可以命中这个敌人多少次，也会取决于这个延迟的值，如果我们希望“伤害段数”非常高非常爽，就把这个值变小就可以。
        /// </summary>
        public float SameTargetDelay;

        /// <summary>
        /// 子弹也有异动类型，地面或者飞行，这决定了他和地形的碰撞关系,也可以作为静止不动的子弹：例如王者中王昭君放一个1技能，地上出现一个圈圈，多少秒内碰撞多少次。
        /// </summary>
        public BulletMoveType MoveType;

        /// <summary>
        /// 碰撞到哪些就会消失，例如碰撞到地面
        /// </summary>
        public string[] RemoveOnObstacledTag;

        /// <summary>
        /// 这里需要定义一个state分清可击中对象，
        /// 子弹碰撞是否会发生在友军或者敌军身上，所谓友军，是双方的ChaState.side值相等，敌方则是不相等的。
        /// 子弹的发射者(caster)的side（如果没有caster就会被当做-1阵营，可以命中任何角色）与命中目标的side形成了敌我判断逻辑中的“双方”。
        /// </summary>
        public string HitType;
    }
}