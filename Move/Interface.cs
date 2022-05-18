using System;

namespace Move
{
    public interface MoveStartable
    {
        string ID 
        { 
            get; 
        }
        int InitialVelocity
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
    interface Fireable
    {

        public Vector InitialBulletPosition{
            get;
        }
        public int InitialBulletDirection{
            get;
        }
        public Vector initialVelocity{
            get;
        }
    }

}