using System;
using Xunit;
using Moq;


namespace Move.Tests
{
    public class MoveTests
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

        [Fact]
        public void Move()
        {
            var m = new Mock<Movable>();
            m.Setup(m => m.Velocity).Returns(new Vector(5,2)).Verifiable();
            m.Setup(m => m.Position).Returns(new Vector(6,6)).Verifiable();
            m.SetupSet(m => m.Position = It.Is<Vector> (p => p.Equals(new Vector(11,8)))).Verifiable();
            var c = new Move(m.Object);
            c.Execute();
            m.VerifyAll();
        }

        [Fact]
        public void StopMove()
        {

            bool IsGetByIDOkay = true;
            bool IsVelocityRemoveOkay = true;
            bool IsEmptyCommandSetOkay = true;

            var ID = new Mock<Command>();
            var Movement = new Mock<Command>();

            Func<string, object[], object> strategy = (key, args) =>
            {
                if (key == "GameObject.GetByID")
                {
                    if (args.Length != 1)
                    {
                        IsGetByIDOkay = false;
                    }

                    if(!(args[0] is string))
                    {
                        IsGetByIDOkay = false;
                    }

                    return ID.Object;
                }

                else if (key == "GameObject.Velocity.Remove")
                {
                    if (args.Length != 1)
                    {
                        IsVelocityRemoveOkay = false;
                    }

                    if (!(args[0] is StopMovable))
                    {
                        IsVelocityRemoveOkay = false;
                    }

                    return Movement.Object;
                }

                else if (key == "GameObject.EmptyCommand.Set")
                {
                    if (args.Length != 2)
                    {
                        IsEmptyCommandSetOkay = false;
                    }

                    if (!(args[0] is StopMovable))
                    {
                        IsEmptyCommandSetOkay = false;
                    }

                    if (!(args[1] is EmptyCommand))
                    {
                        IsEmptyCommandSetOkay = false;
                    }

                    return new EmptyCommand();
                }
                else
                {
                    throw new Exception();
                }
            };

            Assert.True(IsGetByIDOkay);
            Assert.True(IsVelocityRemoveOkay);
            Assert.True(IsEmptyCommandSetOkay);
        }
    }

}