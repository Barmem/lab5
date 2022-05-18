using System;
using Xunit;
using Moq;


namespace Move.Tests
{
public class UnitTest1
{
    [Fact]
    public void StartMove()
    {
        bool IsChangeVelocityOkay = true;
        bool IsMovementOkay = true;
        bool IsGetByIDOkay = true;
       
        var m = new Mock<MoveStartable>();
        var v = new Mock<VelocityChangeable>();
        var obj_move = m.Object;
        var obj_vel = v.Object;
        var Velocity = new Mock<ChangeVelocityCmd>();
        var Movement = new Mock<Command>();
        var ID = new Mock<Command>();

        StartMove c = new StartMove(obj_move);

        Func<string, object[], object> strategy = (key, args) =>
        {
            if (key == "GameObject.ChangeVelocity")
            {
                if(args.Length != 2)
                {
                    IsChangeVelocityOkay = false;
                }

                if(!(args[0] is MoveStartable))
                {
                    IsChangeVelocityOkay = false;
                }

                if(!(args[1] is string))
                {
                    IsChangeVelocityOkay = false;
                }

                return Velocity.Object;
            }

            else if (key == "GameObject.Movement")
            {
                if (args.Length != 1)
                {
                    IsMovementOkay = false;
                }

                if (!(args[0] is MoveStartable))
                {
                    IsMovementOkay = false;
                }

                return Movement.Object;
            }

            else if (key == "Queue")
            {
                var Q = new Mock<Queue>();
                return Q.Object;
            }

            else if (key == "GameObject.GetByID")
            {
                if (args.Length != 1)
                {
                    IsGetByIDOkay = false;
                }

                if (!(args[0] is string))
                {
                    IsGetByIDOkay = false;
                }
                    
                return ID.Object;
            }

            else
            {
                throw new Exception();
            }
        };

        Assert.True(IsChangeVelocityOkay);
        Assert.True(IsMovementOkay);
        Assert.True(IsGetByIDOkay);
    }
}
}