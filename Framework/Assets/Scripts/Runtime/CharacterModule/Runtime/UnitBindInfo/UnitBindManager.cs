using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.CharacterModule.Runtime
{
    public class UnitBindManager : MonoBehaviour
    {
        public Dictionary<UnitBindPointType, UnitBindPoint> UnitBindPointDic =
            new Dictionary<UnitBindPointType, UnitBindPoint>();

        private void Awake()
        {
            //查找所有带BindPoint的点
        }
    }
}