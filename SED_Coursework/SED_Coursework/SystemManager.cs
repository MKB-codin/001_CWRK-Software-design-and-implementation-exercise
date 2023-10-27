using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SED_Coursework
{
    class AdminSystem
    {
        private string AdminPassword { get; set; } = "pa$$w0rd";

        public bool CheckPassword(string password)
        {
            if (password == AdminPassword) { return true; }
            else { return false; }
        }
        
        public List<Cruise> Cruises;
        public List<Port> AvailablePorts; 
        public List<Passanger> Passangers; 
        public List<Trip> AvailableTrips; 
        public AdminSystem()
        {
            Cruises = new List<Cruise>();
            AvailablePorts = new List<Port>();
            Passangers = new List<Passanger>();
            AvailableTrips = new List<Trip>();
        }

        public void AddCruise(Cruise pCruise)
        {
            Cruises.Add(pCruise);
            Console.WriteLine($"{pCruise.CruiseName} added to system\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemoveCruise(Cruise pCruise)
        {
            if (Cruises.Contains(pCruise))
            {
                Cruises.Remove(pCruise);
                Console.WriteLine($"{pCruise.CruiseName} removed from system\n");
            }
            else
            {
                Console.WriteLine($"{pCruise.CruiseName} was not found\n");
            }
        }

        public void AddPort(Port pPort)
        {
            AvailablePorts.Add(pPort);
            Console.WriteLine($"{pPort.PortName} added to system\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePort(Port pPort)
        {
            if (AvailablePorts.Contains(pPort))
            {
                AvailablePorts.Remove(pPort);
                Console.WriteLine($"{pPort.PortName} removed from system\n");
            }
            else
            {
                Console.WriteLine($"{pPort.PortName} was not found\n");
            }
        }

        public void AddPassanger(Passanger pPassanger)
        {
            Passangers.Add(pPassanger);
            Console.WriteLine($"{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) added to system\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (Passangers.Contains(pPassanger))
            {
                Passangers.Remove(pPassanger);
                Console.WriteLine($"{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) removed from system\n");
            }
            else
            {
                Console.WriteLine($"{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) was not found\n");
            }
        }

        public void AddTrip(Trip pTrip)
        {
            AvailableTrips.Add(pTrip);
            Console.WriteLine($"{pTrip.TripName} added to system\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemoveTrip(Trip pTrip)
        {
            if (AvailableTrips.Contains(pTrip))
            {
                AvailableTrips.Remove(pTrip);
                Console.WriteLine($"{pTrip.TripName} removed from system\n");
            }
            else
            {
                Console.WriteLine($"{pTrip.TripName} was not found\n");
            }
        }
    }

    class Cruise
    {
        public string CruiseName { get; set; }
        public int CruiseID { get; private set; }
        private static int NextCruiseID { get; set; } = 1;
        public decimal CruiseCost { get; private set; } = 0;

        List<Passanger> CruisePassangers;
        List<Port> CruisePorts;
                
        public Cruise(string pCruiseName, decimal pCruiseCost)
        {
            CruiseName = pCruiseName;
            CruiseCost = pCruiseCost;
            CruiseID = NextCruiseID;
            NextCruiseID++;
            CruisePassangers = new List<Passanger>();
            CruisePorts = new List<Port>();
        }
        public Cruise()
        {
            CruiseName = "Sample Cruise Name";
            CruiseID = NextCruiseID;
            NextCruiseID++;
            CruisePassangers = new List<Passanger>();
            CruisePorts = new List<Port>();
        }

        public void AddPort(Port pPort)
        {
            CruisePorts.Add(pPort);
            Console.WriteLine($"{pPort.PortName} Added to {this.CruiseName}\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePort(Port pPort)
        {
            if (CruisePorts.Contains(pPort))
            {
                CruisePorts.Remove(pPort);
                Console.WriteLine($"{pPort.PortName} Removed from {this.CruiseName}\n");
            }
            else
            {
                Console.WriteLine($"{pPort.PortName} was not found\n");
            }
        }

        public void AddPassanger(Passanger pPassanger)
        {
            CruisePassangers.Add(pPassanger);
            Console.WriteLine($"{pPassanger.FName} Added to {this.CruiseName}\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (CruisePassangers.Contains(pPassanger))
            {
                CruisePassangers.Remove(pPassanger);
                Console.WriteLine($"{pPassanger.FName} Removed from {this.CruiseName}\n");
            }
            else
            {
                Console.WriteLine($"{pPassanger.FName} was not found\n");
            }
        }

        public override string ToString()
        {
            return $"{this.CruiseName} ({this.CruiseID})";
        }
    }

    class Passanger
    {
        public string FName { get; set; }
        public string SName { get; set; }
        public int Passport { get; set; }
        public Cruise AssignedCruise { get; private set; }
        List<Trip> AssignedTrips = new List<Trip>();
        List<Trip> TripsThatDontComeFree = new List<Trip>();
        public decimal CostOfTrip { get; set; } = 0;
        
        public Passanger(string fname, string sname, int passport)
        {
            FName = fname;
            SName = sname;
            Passport = passport;
            AssignedTrips = new List<Trip>();
        }

        public bool IsCruiseAssignedToPassanger()
        {
            if (this.AssignedCruise != null)
            {
                return true;
            }
            else { return false; }
        }
        public void AssignCruiseToPassanger(Cruise pCruise)
        {
            AssignedCruise = pCruise;
            Console.WriteLine($"{pCruise.ToString()} was assinged to {this.ToString()}");
        }
        public void UnAssignCruiseFromPassanger()
        {
            this.AssignedCruise = null;
        }
        public void AddToAssignedTrips(Trip pTrip)
        {
            this.AssignedTrips.Add(pTrip);
            Console.WriteLine($"{pTrip.ToString()} was assigned to {this.ToString()}");
            if (AssignedTrips.Count >= 2 )
            {
                TripsThatDontComeFree.Add(pTrip);
            }
            //Add way to remove duplicates and implement a way so they can only have valid trips ie trips that are on the ports they are going to
            // make them pay extra for going on more trips than they are allowed to.
        }
        public void RemoveAssignedTrip(Trip pTrip)
        {
            if (this.AssignedTrips.Contains(pTrip))
            {
                this.AssignedTrips.Remove(pTrip);
                Console.WriteLine($"{pTrip.ToString()} was removed from {this.ToString()}");
            }
            else
            {
                Console.WriteLine($"{pTrip.ToString()} was not found");
            }
        }
        public void CalculateCostOfTrip()
        {
            decimal cost = 0;
            try { cost += this.AssignedCruise.CruiseCost; } catch (NullReferenceException e) { cost += 0; }
            try
            {
                foreach (Trip trip in this.TripsThatDontComeFree)
                {
                    cost += trip.TripCost;
                }
            }
            catch (NullReferenceException e) { cost += 0; }
            this.CostOfTrip = cost;
        }
        public void fixCost()
        {
            if (this.CostOfTrip < 0)
            {
                this.CostOfTrip = 0;
            }
        }
        public override string ToString()
        {
            return $"{this.FName} {this.SName} ({this.Passport})";
        }
    }
    class Port
    {
        public string PortName { get; set; }
        public int PortID { get; private set; }
        private static int NextPortID { get; set; } = 1;

        public List<Trip> PortTrips;

        public Port(string pPortName)
        {
            PortName = pPortName;
            PortTrips = new List<Trip>();
            PortID = NextPortID;
            NextPortID++;
        }

        public override string ToString()
        {
            return $"ID:{PortID}, {PortName}";
        }

        public void AddTrip(Trip pTrip)
        {
            PortTrips.Add(pTrip);
            Console.WriteLine($"{pTrip.TripName} Added to {this.PortName}\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemoveTrip(Trip pTrip)
        {
            if (PortTrips.Contains(pTrip))
            {
                PortTrips.Remove(pTrip);
                Console.WriteLine($"{pTrip.TripName} Removed from  {this.PortName}\n");
            }
            else
            {
                Console.WriteLine($"{pTrip.TripName} was not found\n");
            }
        }
    }
    class Trip
    {
        public string TripName { get; set; }
        public int TripID { get; private set; }
        private int NextTripID { get; set; } = 1;
        public decimal TripCost { get; private set; } = 0;
        public List<Passanger> TripPassangers;

        public Trip(string ptripName, int ptripID,List<Passanger> pPassangers, decimal pTripCost)
        {
            TripName = ptripName;
            TripID = ptripID;
            TripCost = pTripCost;
            if(ptripID == NextTripID) { NextTripID++; } 
            TripPassangers = pPassangers;
        }
        public Trip(string pTripName, List<Passanger> pPassangers, decimal pTripCost)
        {
            TripName = pTripName;
            TripPassangers = pPassangers;
            TripCost = pTripCost;
            TripID = NextTripID;
            NextTripID++;
        }
        public Trip(string pTripName, decimal pTripCost)
        {
            TripName = pTripName;
            TripCost = pTripCost;
            TripPassangers = new List<Passanger>();
            TripID = NextTripID;
            NextTripID++;
            
        }
        public Trip()
        {
            TripName = "Sample Trip Name";
            TripPassangers = new List<Passanger>();
            TripID = NextTripID;
            NextTripID++;
        }

        public override string ToString()
        {
            return $"ID:{TripID}, {TripName}";
        }

        public void AddPassanger(Passanger pPassanger)
        {
            TripPassangers.Add(pPassanger);
            Console.WriteLine($"{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) added to {this.TripName}\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (TripPassangers.Contains(pPassanger))
            {
                TripPassangers.Remove(pPassanger);
                Console.WriteLine($"{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) removed from {this.TripName}\n");
            }
            else
            {
                Console.WriteLine($"{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) was not found\n");
            }
        }
    }
}
