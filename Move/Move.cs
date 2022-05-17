using System;

namespace Move
{
    public class Move : Command
    {
        Movable obj;

        public Move(Movable obj)
        {
            this.obj = obj;
        }

        public void Execute()
        {
            
        }
    }
}