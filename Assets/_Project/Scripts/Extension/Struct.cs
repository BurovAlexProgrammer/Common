using System;
using UnityEngine;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static int RoundInt(this float value) => Mathf.RoundToInt(value);

        public static int MultiplyInt(this float value, int multi) => Mathf.RoundToInt(value * multi);
        
        public static int ToMillisecs(this float value) => Mathf.RoundToInt(value * 1000);
    }
    
    
}