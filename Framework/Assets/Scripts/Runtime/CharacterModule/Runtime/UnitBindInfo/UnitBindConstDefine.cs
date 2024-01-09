using UnityEngine;

namespace Runtime.CharacterModule.Runtime
{
    public enum UnitBindPointType
    {
        None = 0,
        Body = 1,
        RightFoot = 2,
        LeftFoot = 3,
    }
    
    public enum UnitBindPointType1
    {
        [InspectorName("wu")]
        None,
        [InspectorName("yu")]
        Body,
    }
}