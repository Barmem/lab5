using System;
using Xunit;
using Moq;

namespace Move.Tests;

public class UnitTest2
{

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

        else if (key == "GameObjects.CreateBullet")
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

    IoC.Resolve<Command>("IoC.Setup", strategy).Execute();

    c.Execute();
        

    m.VerifyAll();
    Assert.True(PosWasCalled && isPos);
    Assert.True(DirWasCalled && isDir);
    Assert.True(ActionWasCalled && isAction);
    Assert.True(StartWasCalled && isStartMovement);
    }
}