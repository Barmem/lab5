using System;

namespace Move
{
    public interface MoveStartable
    {
        string ID 
        { 
            get; 
        }
        int initialVelocity
        {
            get;
        }
    }

    public interface VelocityChangeable
    {
        int Velocity
        {
            set;
            get;
        }
    }

    public interface Movable
    {
        public Vector Position
        {
            set;
            get;
        }

        public Vector Velocity
        {
            get;
        }
    }
}