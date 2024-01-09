namespace Runtime.SkillModule.Runtime.Aoe
{
    public class AoeModel
    {
        public uint ID;
        
        /// <summary>
        /// 逻辑标签
        /// </summary>
        public string[] Tags;
        
        /// <summary>
        /// 资源路径
        /// </summary>
        public string PrefabPath;

        /// <summary>
        /// 执行多久后调用一次回调点
        /// </summary>
        public float TickTimes;
        
        
    }
}