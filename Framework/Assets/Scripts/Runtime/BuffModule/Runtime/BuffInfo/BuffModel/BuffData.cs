using Module.ObjectPool;

namespace BuffModule.Runtime
{
    public class BuffData : IClearObj
    {
        public BuffItem BuffItem;
        
        public BuffExecute OnOccur;
        
        /// <summary>
        /// 
        /// </summary>
        public BuffExecute OnHit;
        public BuffExecute OnBeHurt;
        public BuffExecute OnKill;
        public BuffExecute OnBeKill;
        public BuffExecute OnTick;
        public BuffExecute OnRemove;
        
        public void Clear()
        {
            OnOccur = null;
            OnHit = null;
            OnBeHurt = null;
            OnKill = null;
            OnBeKill = null;
            OnTick = null;
            OnRemove = null;
        }
    }
}