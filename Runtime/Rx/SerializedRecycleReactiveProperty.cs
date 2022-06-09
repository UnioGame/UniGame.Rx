﻿using System;

namespace UniModules.UniGame.Core.Runtime.Rx
{
    [Serializable]
    public class IntRecycleReactiveProperty : RecycleReactiveProperty<int> {}

    [Serializable]
    public class FloatRecycleReactiveProperty : RecycleReactiveProperty<float> {}

    [Serializable]
    public class BoolRecycleReactiveProperty : RecycleReactiveProperty<bool>
    {
        public BoolRecycleReactiveProperty() : base()
        {
        }

        public BoolRecycleReactiveProperty(bool defaultValue) : base(defaultValue)
        {
            
        }
    }
    
    [Serializable]
    public class StringRecycleReactiveProperty : RecycleReactiveProperty<string> {}
    
    [Serializable]
    public class DoubleRecycleReactiveProperty : RecycleReactiveProperty<double> {}
    
    [Serializable]
    public class ByteRecycleReactiveProperty : RecycleReactiveProperty<byte> {}
}

