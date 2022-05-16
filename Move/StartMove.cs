using System;
using System.Collections.Generic;

namespace Move
{
    public class StartMove : Command
    {
        MoveStartable moveStartable;
        public StartMove(MoveStartable moveStartable)
        {
            this.moveStartable = moveStartable;
        }

        public void Execute()
        {

        }
    }
}
