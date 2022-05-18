using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Move
{
    public class IoC
    {
        public static T Resolve<T>(string key, params object[] args)
        {
            return (T)strategy(key,args);
        }

        public class IoCSetupCommand : Command
        {
            Func <string, object[], object> newStrategy;

            public IoCSetupCommand (Func<string, object[], object> newStrategy)
            {
                this.newStrategy = newStrategy;
            }
            public void Execute()
            {
                strategy = newStrategy;
            }
        }

        private static Func<string, object[], object> strategy = (key, args) => 
        {
            if ("IoC.Setup" == key)
            {
                var newStrategy = (Func<string, object[], object>)args[0];
                return new IoCSetupCommand(newStrategy);
            }
            else if ("IoC.Strategy" == key)
            {
                if(strategy == null){
                    throw new Exception();
                }
                return strategy;
            }
            else
            {
                throw new Exception();
            }
        };
    }
}