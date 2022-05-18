using System;

namespace Move
{
    public interface UObject
    {
        object this[string key]
        {
            get;
            set;
        }
    }
}