using System;
using Xunit;
using Moq;


namespace Move.Tests;

public class UnitTest1
{
    [Fact]
    public void StartMove()
    {
        var m = new Mock<MoveStartable>();
        var v = new Mock<VelocityChangeable>();
        var obj_move = m.Object;
        var obj_vel = v.Object;
        var Velocity = new Mock<ChangeVelocityCmd>();
        var Movement = new Mock<Command>();
        var ID = new Mock<Command>();

        StartMove c = new StartMove(obj_move);

        bool isChangeVelocityOkay = true;
        bool isMovementOkay = true;
        bool isGetByIDOkay = true;

        Func<string, object[], object> strategy = (key, args) =>
        {
            if (key == "Tank.ChangeVelocity")
            {
                if(args.Length != 2)
                {
                    isChangeVelocityOkay = false;
                }

                if(!(args[0] is MoveStartable))
                {
                    isChangeVelocityOkay = false;
                }

                if(!(args[1] is string))
                {
                    isChangeVelocityOkay = false;
                }

                return Velocity.Object;
            }

            else if (key == "Tank.Movement")
            {
                if (args.Length != 1)
                {
                    isMovementOkay = false;
                }

                if (!(args[0] is MoveStartable))
                {
                    isMovementOkay = false;
                }

                return Movement.Object;
            }

            else if (key == "Queue")
            {
                var Q = new Mock<Queue>();
                return Q.Object;
            }

            else if (key == "Tank.GetByID")
            {
                if (args.Length != 1)
                {
                    isGetByIDOkay = false;
                }

                if (!(args[0] is string))
                {
                    isGetByIDOkay = false;
                }
                    
                return ID.Object;
            }

            else
            {
                throw new Exception();
            }
        };

        Assert.True(isChangeVelocityOkay);
        Assert.True(isMovementOkay);
        Assert.True(isGetByIDOkay);
    }
}