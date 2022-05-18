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
            var obj = IoC.Resolve<MoveStartable>("Tank.GetByID", moveStartable.ID);
            IoC.Resolve<Command>("Tank.ChangeVelocity", obj, moveStartable.initialVelocity);
            var cmd = IoC.Resolve<Command>("Tank.Movement", obj);
            IoC.Resolve<Queue>("Queue").Add(cmd);
        }
    }
}
