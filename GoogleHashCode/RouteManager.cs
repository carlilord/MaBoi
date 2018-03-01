using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GoogleHashCode
{
    public class RouteManager
    {
        private static object lockObject = new object();
        private BlockingCollection<Ride> availableRides;

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }
    
        public int VehiclesCount { get; set; }

        public int RidesCount { get; set; }

        public int RideBonus { get; set; }

        public int MaxStep { get; set; }

        public List<Ride> Rides { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        private BlockingCollection<Ride> AvailableRides
        {
            get { return availableRides; }
            set
            {
                lock(lockObject)
                {
                    availableRides = value;
                }
            }
        }

        public RouteManager()
        {
            Vehicles = new List<Vehicle>();
        }

        private void InitVehicles()
        {
            for (int i = 0; i < VehiclesCount; i++)
            {
                Vehicles.Add(new Vehicle
                {
                    Id = i,
                    CurrentPosition = new Point(),


                    Route = new Stack<Ride>()
                });
            }
        }

        public async Task EasyCalculation()
        {
            AvailableRides = new BlockingCollection<Ride>();
            Rides.ForEach(x => AvailableRides.Add(x));
            InitVehicles();

            List<Task> taskList = new List<Task>();
            int idealRange = VehiclesCount / 10; // ToDo
            for (int i = 0; i < Vehicles.Count; i++)
            {
                if(idealRange + i > VehiclesCount)
                {
                    idealRange = VehiclesCount - i;
                }

                var vehicleSeperation = Vehicles.GetRange(i, idealRange);
                i += idealRange;

                taskList.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; i < vehicleSeperation.Count && FindBestRides(vehicleSeperation[j]); j++)
                    {
                        if (j == VehiclesCount - 1)
                        {
                            j = 0;
                        }
                    }
                }));
            }

            await Task.WhenAll(taskList);
            //for(int i = 0; i < VehiclesCount && FindBestRides(Vehicles[i]); i++)
            //{
            //    if(i == VehiclesCount -1)
            //    {
            //        i = 0;
            //    }
            //}
        }

        private bool FindBestRides(Vehicle vehicle)
        {
            

            AvailableRides = AvailableRides.Where(x => !x.IsPickedUp).ToList();

            if(AvailableRides.Count() <= 0 || AvailableRides.Min(x => x.GetDistance()) > Math.Abs(vehicle.CurrentSteps - MaxStep))
            {
                return false;
            }

            if(vehicle.CurrentSteps >= MaxStep)
            {
                return true;
            }
            
            var bestRide = FindMin(AvailableRides, vehicle);
            bestRide.IsPickedUp = true;
            vehicle.CurrentSteps += (bestRide.EarliestStart - vehicle.CurrentSteps) > 0 ? (bestRide.EarliestStart - vehicle.CurrentSteps) : 0;
            vehicle.CurrentSteps += bestRide.GetDistance();
            vehicle.Route.Push(bestRide);

            return true;                
        }

        private Ride FindMin(List<Ride> rides, Vehicle vehicle)
        {
            Ride minElement = null;

            foreach (var item in rides)
            {
                if(minElement == null || GetWeightedValue(minElement, vehicle) > GetWeightedValue(item, vehicle))
                {
                    minElement = item;
                }
            }

            return minElement;
        }

        private double GetWeightedValue(Ride r, Vehicle vehicle)
        {
            var bonus = (r.EarliestStart - vehicle.CurrentSteps) > 0 ? (r.EarliestStart - vehicle.CurrentSteps) : 0;
            return r.GetDistance() * 0.65 + vehicle.GetDistanceToRide(r) * 0.35 + bonus * 0.175;
        }
    }


}
