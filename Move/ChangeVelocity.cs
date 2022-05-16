using System;

namespace Move
{
    public class ChangeVelocityCmd : Command
    {
        VelocityChangeable velocity;
        int initialVelocity;
        public ChangeVelocityCmd(VelocityChangeable velocity, int initialVelocity)
        {
            this.velocity = velocity;
            this.initialVelocity = initialVelocity;
        }
        public void Execute()
        {
            
        }
    }
}