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
            bool IsMoveSetOkay = true;

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

                else if (key == "GameObject.Move.Set")
                {
                    if (args.Length != 2)
                    {
                        IsMoveSetOkay = false;
                    }

                    if (!(args[0] is StopMovable))
                    {
                        IsMoveSetOkay = false;
                    }

                    if (!(args[1] is EmptyCommand))
                    {
                        IsMoveSetOkay = false;
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
            Assert.True(IsMoveSetOkay);
        }

    [Fact]

    public void EmptOkay()
    {
        bool IsEmptyCmdOkay = true;
        var EmptyCMD = new Mock<EmptyCommand>();
        if (EmptyCMD == null)
            {
                IsEmptyCmdOkay = false;
            }
        Assert.True(IsEmptyCmdOkay);

    }

    [Fact]
    public void Vectors()
        {
        Vector v1 = new Vector(1, 2);
        Vector v2 = new Vector(1, 2);

        var isEqual = v1.Equals(v2);

        Assert.True(isEqual);
        }

    [Fact]
    public void FireTest()
    {
        //Arrange
        var m = new Mock<Fireable>();
    
        var obj = m.Object;

        m.Setup(m => m.InitialBulletPosition).Returns(new Vector (10, 5)).Verifiable();
        m.Setup(m => m.InitialBulletDirection).Returns(new Vector (5, 10)).Verifiable();
        m.Setup(m => m.InitialVelocity).Returns(new Vector (10, 10)).Verifiable();


    Fire c = new Fire(obj);

    bool isAction = true;

    bool ActionWasCalled = false;

    var Action = new Mock<UObject>(); 

    bool isPos = true;

    bool PosWasCalled = false;

    var Pos = new Mock<Command>();
    
    bool isDir = true;

    bool DirWasCalled = false;

    var Dir = new Mock<Command>();

    bool isStartMovement = true;

    bool StartWasCalled = false;

    var StartM = new Mock<Command>();

    var GameObjects = new Mock<UObject>();

    Func<string, object[], object> strategy = (key, args) => {
        
        if (key == "GameObject.SetPosition")
        {
            PosWasCalled = true;

            if(args.Length != 2) 
            isPos = false;
            if(!(args[0] is UObject))
            isPos = false;
            if(!(args[1] is Vector))
            isPos = false;
            
            return Pos.Object;
        }

        else if (key == "GameObject.SetDirection")
        {
            
            DirWasCalled = true;

            if(args.Length != 2) 
            isDir = false;
            if(!(args[0] is UObject))
            isDir = false;
            if(!(args[1] is Vector))
            isDir = false;
            
            return Dir.Object;
        }

        else if (key == "GameObject.CreateBullet")
        {

            return GameObjects.Object;
            
        }

        else if (key == "Action")
        {
            ActionWasCalled = true;

            if(args.Length != 3) 
            isAction = false;
            if(!(args[0] is UObject))
            isAction = false;
            if(!(args[1] is string)||(string)args[1] != "Move")
            isAction = false;
            if(!(args[2] is Vector))
            isAction = false;
            
            return Action.Object;

        }

        else if (key =="GameObject.StartMovement")
        {

            StartWasCalled = true;

            if(args.Length != 1) 
            isStartMovement = false;
            if(!(args[0] is UObject))
            isStartMovement = false;
            
            return StartM.Object;
            
        }

        else if ("IoC.Setup" == key)
        {
            var newStrategy = (Func<string,object[],object>) args[0];

            return new IoC.IoCSetupCommand(newStrategy);
        }
        else
        {
            throw new Exception();
        }
    };
    var defStrat = IoC.Resolve<Func<string, object[], object>>("IoC.Strategy");
    IoC.Resolve<Command>("IoC.Setup", strategy).Execute();
    c.Execute();
        

    m.VerifyAll();
    Assert.True(PosWasCalled && isPos);
    Assert.True(DirWasCalled && isDir);
    Assert.True(ActionWasCalled && isAction);
    Assert.True(StartWasCalled && isStartMovement);
    IoC.Resolve<Command>("IoC.Setup", defStrat).Execute();
    }
}
}