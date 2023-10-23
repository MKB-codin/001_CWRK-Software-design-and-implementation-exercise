using System;
using System.Collections.Generic;
using System.Linq;
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

        List<Passanger> CruisePassangers;
        List<Port> CruisePorts;
                
        public Cruise()
        {
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
        public Passanger(string fname, string sname, int passport)
        {
            FName = fname;
            SName = sname;
            Passport = passport;
        }
        public override string ToString()
        {
            return $"{this.FName + this.SName} ({this.Passport})";
        }
    }
    class Port
    {
        public string PortName { get; set; }
        public int PortID { get; private set; }
        private static int NextPortID { get; set; } = 1;

        public List<Trip> PortTrips;

        public Port()
        {
            PortTrips = new List<Trip>();
            PortID = NextPortID;
            NextPortID++;
        }

        public void AddTrip(Trip pTrip)
        {
            PortTrips.Add(pTrip);
            Console.WriteLine($"{pTrip.TripName} Added to {this.PortName}\n");
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePassanger(Trip pTrip)
        {
            if (PortTrips.Contains(pTrip))
            {
                PortTrips.Remove(pTrip);
                Console.WriteLine($"{pTrip.TripName}  Removed from  {this.PortName}\n");
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
        public List<Passanger> TripPassangers;

        public Trip(string ptripName, int ptripID,List<Passanger> pPassangers)
        {
            TripName = ptripName;
            TripID = ptripID;
            if(ptripID == NextTripID) { NextTripID++; } 
            TripPassangers = pPassangers;
        }
        public Trip(string pTripName, List<Passanger> pPassangers)
        {
            TripName = pTripName;
            TripPassangers = pPassangers;
            TripID = NextTripID;
            NextTripID++;
        }
        public Trip(string pTripName)
        {
            TripName = pTripName;
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
