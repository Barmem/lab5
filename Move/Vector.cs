using System;
using System.Collections.Generic;
using System.Numerics;

namespace Move
{
   public class Vector
   {
       int x;
       int y;

       public Vector(int x, int y)
       {
           this.x = x;
           this.y = y;
       }

       public static Vector operator +(Vector v1, Vector v2)
       {
           return new Vector(v1.x + v2.x, v1.y + v2.y);
       }

       public override bool Equals(Object obj)
       {
           Vector p = (Vector)obj;
           return (p.x == x && p.y == y);
       }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}