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
            var obj = IoC.Resolve<StopMovable>("GameObject.GetByID", endable.ID);
            IoC.Resolve<Command>("GameObject.Velocity.Remove", obj).Execute();
            IoC.Resolve<Command>("GameObject.MoveCommand.Set", obj, new EmptyCommand()).Execute();
        }
    }
}