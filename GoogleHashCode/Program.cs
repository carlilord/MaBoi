using System;
using System.Collections.Generic;

namespace GoogleHashCode
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            

            var x = FileHelper.ReadInput();

            var firstLine = x[0].Split(' ');

            var rides = new List<Ride>();
            
            for(int i = 1; i < x.Length; i++)
            {
                var curRide = x[i].Split(' ');
                rides.Add(new Ride
                {
                    RowStart = int.Parse(curRide[0]),
                    ColumnStart = int.Parse(curRide[1]),
                    RowFinish = int.Parse(curRide[2]),
                    ColumnFinish = int.Parse(curRide[3]),
                    EarliestStart = int.Parse(curRide[4]),
                    LatestFinish = int.Parse(curRide[5]),
                    Id = i-1
                    
                });
            }

            var manager = new RouteManager
            {
                RowCount = int.Parse(firstLine[0]),
                ColumnCount = int.Parse(firstLine[1]),
                VehiclesCount = int.Parse(firstLine[2]),
                RidesCount = int.Parse(firstLine[3]),
                RideBonus = int.Parse(firstLine[4]),
                MaxStep = int.Parse(firstLine[5]),
                Rides = rides
            };

            manager.EasyCalculation().Wait();

            string tmp = "";

            foreach (var item in manager.Vehicles)
            {
                Console.WriteLine(item);
                tmp += item + Environment.NewLine;


            }
            
            FileHelper.WriteOutput(tmp);

        }


    }
}
