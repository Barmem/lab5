using System.Numerics;
namespace Move
{

    public class Fire : Command
    {
        Fireable fireable;
        public FireCommand(Fireable fireable){
            this.fireable = fireable;
        }

        public void Execute(){
            var obj = IoC.Resolve<UObject>("GameObject.CreateBullet");
            IoC.Resolve<Command>("GameObject.SetInitialPosition", 
                                obj, 
                                fireable.InitialBulletPosition
                                ).Execute();
            IoC.Resolve<Command>("GameObject.SetInitialPosition", 
                                obj, 
                                fireable.InitialBulletDirection
                                ).Execute(); 
            IoC.Resolve<UObject>("Action", 
            obj, 
            "Move", 
            fireable.initialVelocity);
            IoC.Resolve<Command>("GameObject.StartMovement", action).Execute();
        }
    }
}