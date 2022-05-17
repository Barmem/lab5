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
            var obj = IoC.resolve<MoveStartable>("Tank.GetByID", moveStartable.ID);
            IoC.resolve<Command>("Tank.ChangeVelocity", obj, moveStartable.initialVelocity);
            var cmd = IoC.resolve<Command>("Tank.Movement", obj);
            IoC.resolve<Queue>("Queue").Add(cmd);
        }
    }
}
