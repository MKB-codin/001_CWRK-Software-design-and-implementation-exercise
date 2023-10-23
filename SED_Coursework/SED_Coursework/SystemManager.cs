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
            Console.WriteLine($"{pCruise.CruiseName} Added");
        }
        public void RemoveCruise(Cruise pCruise)
        {
            if (Cruises.Contains(pCruise))
            {
                Cruises.Remove(pCruise);
                Console.WriteLine($"{pCruise.CruiseName} Removed");
            }
            else
            {
                Console.WriteLine($"{pCruise.CruiseName} was not found");
            }
        }

        public void AddPort(Port pPort)
        {
            AvailablePorts.Add(pPort);
            Console.WriteLine($"{pPort.PortName} Added");
        }
        public void RemovePort(Port pPort)
        {
            if (AvailablePorts.Contains(pPort))
            {
                AvailablePorts.Remove(pPort);
                Console.WriteLine($"{pPort.PortName} Removed");
            }
            else
            {
                Console.WriteLine($"{pPort.PortName} was not found");
            }
        }

        public void AddTrip(Trip pTrip)
        {
            AvailableTrips.Add(pTrip);
            Console.WriteLine($"{pTrip.TripName} Added");
        }
        public void RemoveTrip(Trip pTrip)
        {
            if (AvailableTrips.Contains(pTrip))
            {
                AvailableTrips.Remove(pTrip);
                Console.WriteLine($"{pTrip.TripName} Removed");
            }
            else
            {
                Console.WriteLine($"{pTrip.TripName} was not found");
            }
        }
    }

    class Cruise
    {
        public string CruiseName { get; set; }
        public int CruiseID { get; private set; }
        private static int NextCruiseID { get; set; } = 1;

        List<Passanger> Passangers;
        List<Port> Ports;
                
        public Cruise()
        {
            CruiseID = NextCruiseID;
            NextCruiseID++;
            Passangers = new List<Passanger>();
            Ports = new List<Port>();
        }

        public void AddPort(Port pPort)
        {
            Ports.Add(pPort);
            Console.WriteLine($"{pPort.PortName} Added to {this.CruiseName}");
        }
        public void RemovePort(Port pPort)
        {
            if (Ports.Contains(pPort))
            {
                Ports.Remove(pPort);
                Console.WriteLine($"{pPort.PortName} Removed from {this.CruiseName}");
            }
            else
            {
                Console.WriteLine($"{pPort.PortName} was not found");
            }
        }

        public void AddPassanger(Passanger pPassanger)
        {
            Passangers.Add(pPassanger);
            Console.WriteLine($"{pPassanger.Name} Added to {this.CruiseName}");
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (Passangers.Contains(pPassanger))
            {
                Passangers.Remove(pPassanger);
                Console.WriteLine($"{pPassanger.Name} Removed from {this.CruiseName}");
            }
            else
            {
                Console.WriteLine($"{pPassanger.Name} was not found");
            }
        }

        public override string ToString()
        {
            return $"{this.CruiseName} ({this.CruiseID})";
        }
    }

    class Passanger
    {
        public string Name { get; set; }
        public int Passport { get; set; }
        public Passanger(string name, int passport, List<Trip> trips)
        {
            Name = name;
            Passport = passport;
        }
    }
    class Port
    {
        public string PortName { get; set; }
        public int PortID { get; private set; }
        private static int NextPortID { get; set; } = 1;

        public List<Trip> Trips;

        public Port()
        {
            Trips = new List<Trip>();
            PortID = NextPortID;
            NextPortID++;
        }

        public void AddTrip(Trip pTrip)
        {
            Trips.Add(pTrip);
            Console.WriteLine($"{pTrip.TripName} Added to {this.PortName}");
        }
        public void RemovePassanger(Trip pTrip)
        {
            if (Trips.Contains(pTrip))
            {
                Trips.Remove(pTrip);
                Console.WriteLine($"{pTrip.TripName}  Removed from  {this.PortName}");
            }
            else
            {
                Console.WriteLine($"{pTrip.TripName} was not found");
            }
        }
    }
    class Trip
    {
        public string TripName { get; set; }
        public int TripID { get; private set; }
        private int NextTripID { get; set; } = 1;
        public List<Passanger> Passangers;

        public Trip(string ptripName, int ptripID,List<Passanger> pPassangers)
        {
            TripName = ptripName;
            TripID = ptripID;
            if(ptripID == NextTripID) { NextTripID++; } 
            Passangers = pPassangers;
        }
        public Trip(string pTripName, List<Passanger> pPassangers)
        {
            TripName = pTripName;
            Passangers = pPassangers;
            TripID = NextTripID;
            NextTripID++;
        }
        public Trip()
        {
            TripName = "Sample Trip Name";
            Passangers = new List<Passanger>();
            TripID = NextTripID;
            NextTripID++;
            
        }

        public void AddPassanger(Passanger pPassanger)
        {
            Passangers.Add(pPassanger);
            Console.WriteLine($"{pPassanger.Name} Added to {this.TripName}");
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (Passangers.Contains(pPassanger))
            {
                Passangers.Remove(pPassanger);
                Console.WriteLine($"{pPassanger.Name} Removed from {this.TripName}");
            }
            else
            {
                Console.WriteLine($"{pPassanger.Name} was not found");
            }
        }
    }
}
