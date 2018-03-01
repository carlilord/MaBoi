using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode
{
    public class Vehicle
    {
        public int Id { get; set; }

        public int RouteCount => Route.Count;

        public Stack<Ride> Route { get; set; }

        public int CurrentSteps { get; set; }

        public Point CurrentPosition { get; set; }

        public override string ToString()
        {
            var tmp = "" + RouteCount;

            foreach(var item in Route)
            {
                tmp += " " + item.Id;
            }

            return tmp;

        }

        public int GetDistanceToRide(Ride ride)
        {
            return Math.Abs(CurrentPosition.X - ride.ColumnStart) + Math.Abs(CurrentPosition.Y - ride.RowStart);
        }






    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {
            X = 0;
            Y = 0;
        }


    }
}
