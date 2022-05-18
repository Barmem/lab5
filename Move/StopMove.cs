using System;

namespace Move
{
    public class StopMove : Command
    {
        StopMovable endable;
    

        public StopMove(StopMovable endable)
        {   
            this.endable = endable;
        }

        public void Execute()
        {
            
        }
    }
}