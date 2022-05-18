using System.Numerics;
namespace Move
{

    public class Fire : Command
    {
        Fireable fireable;
        public Fire(Fireable fireable){
            this.fireable = fireable;
        }

        public void Execute(){
            var obj = IoC.Resolve<UObject>("GameObject.CreateBullet");
            IoC.Resolve<Command>("GameObject.SetPosition", 
                                obj, 
                                fireable.InitialBulletPosition
                                ).Execute();
            IoC.Resolve<Command>("GameObject.SetDirection", 
                                obj, 
                                fireable.InitialBulletDirection
                                ).Execute(); 
            var action = IoC.Resolve<UObject>("Action", 
                                obj, 
                                "Move", 
                                fireable.InitialVelocity);
            IoC.Resolve<Command>("GameObject.StartMovement", action).Execute();
        }
    }
}