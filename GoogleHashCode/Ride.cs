using System;

namespace GoogleHashCode
{
    public class Ride
    {
        private static object lockObject = new object();

        private bool isPickedUp = false;

        public int Id { get; set; }

        public int RowStart { get; set; }

        public int ColumnStart { get; set; }

        public int RowFinish { get; set; }

        public int ColumnFinish { get; set; }

        public int EarliestStart { get; set; }

        public int LatestFinish { get; set; }

        public bool IsPickedUp
        {
            get
            {
                return isPickedUp;
            }
            set
            {
                lock(lockObject)
                {
                    isPickedUp = value;
                }
            }
        }


        public int GetDistance()
        {
            return Math.Abs(RowStart - RowFinish) + Math.Abs(ColumnStart - ColumnFinish);
        }

        
    }
}