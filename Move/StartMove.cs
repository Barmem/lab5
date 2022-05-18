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
            var obj = IoC.Resolve<MoveStartable>("GameObject.GetByID", moveStartable.ID);
            IoC.Resolve<Command>("GameObject.ChangeVelocity", obj, moveStartable.InitialVelocity);
            var cmd = IoC.Resolve<Command>("GameObject.Movement", obj);
            IoC.Resolve<Queue>("Queue").Add(cmd);
        }
    }
}
