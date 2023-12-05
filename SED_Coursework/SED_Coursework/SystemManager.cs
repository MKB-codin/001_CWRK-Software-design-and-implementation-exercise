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
        public bool debug = false;
        private string AdminPassword { get; set; } = "pa$$w0rd";

        public bool CheckPassword(string password)
        {
            if (password == AdminPassword) { return true; }
            else { return false; }
        }
        
        public List<Cruise> Cruises;
        public List<Port> AvailablePorts; 
        public List<Passenger> Passengers; 
        public List<Trip> AvailableTrips; 
        public List<CruisePortDockManager> CruisePortDockManagers;
        public List<CPD_PassengerTripManager> CPD_PassTripManagers;
        public AdminSystem()
        {
            Cruises = new List<Cruise>();
            AvailablePorts = new List<Port>();
            Passengers = new List<Passenger>();
            AvailableTrips = new List<Trip>();
            CruisePortDockManagers = new List<CruisePortDockManager>();
            CPD_PassTripManagers = new List<CPD_PassengerTripManager>();
    }


        public void AddC_P_DManager(CruisePortDockManager cruisePortDockManager)
        {
            if (CruisePortDockManagers.Contains(cruisePortDockManager) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n The link between {cruisePortDockManager.Cruise.ToString()} and {cruisePortDockManager.Port.ToString()} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                CruisePortDockManagers.Add(cruisePortDockManager);
            }
        }
        public void AddCPD_PassTripManager(CPD_PassengerTripManager cPD_PassengerTripManager)
        {
            if (CPD_PassTripManagers.Contains(cPD_PassengerTripManager) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n The link between {cPD_PassengerTripManager._Passenger.ToString()} and {cPD_PassengerTripManager._PortDockManager.Port.ToString()} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                CPD_PassTripManagers.Add(cPD_PassengerTripManager);
            }
        }
        public void AddCruise(Cruise pCruise)
        {
            if (Cruises.Contains(pCruise) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pCruise.CruiseName} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Cruises.Add(pCruise);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pCruise.CruiseName} added to system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemoveCruise(Cruise pCruise)
        {
            if (Cruises.Contains(pCruise))
            {
                Cruises.Remove(pCruise);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pCruise.CruiseName} removed from system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pCruise.CruiseName} was not found\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void AddPort(Port pPort)
        {
            if (AvailablePorts.Contains(pPort) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPort.PortName} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                AvailablePorts.Add(pPort);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pPort.PortName} added to system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemovePort(Port pPort)
        {
            if (AvailablePorts.Contains(pPort) )
            {
                AvailablePorts.Remove(pPort);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pPort.PortName} removed from system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pPort.PortName} was not found\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void AddPassenger(Passenger pPassenger)
        {
            if (Passengers.Contains(pPassenger) || Passengers.FirstOrDefault(o => o.Passport == pPassenger.Passport) != null && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nPassenger with passport id : ({pPassenger.Passport}) already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Passengers.Add(pPassenger);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pPassenger.FName + pPassenger.SName} ({pPassenger.Passport}) added to system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemovePassenger(Passenger pPassenger)
        {
            if (Passengers.Contains(pPassenger))
            {
                Passengers.Remove(pPassenger);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pPassenger.FName + pPassenger.SName} ({pPassenger.Passport}) removed from system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pPassenger.FName + pPassenger.SName} ({pPassenger.Passport}) was not found\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void AddTrip(Trip pTrip)
        {
            if (AvailableTrips.Contains(pTrip) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pTrip.TripName} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                AvailableTrips.Add(pTrip);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pTrip.TripName} added to system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemoveTrip(Trip pTrip)
        {
            if (AvailableTrips.Contains(pTrip))
            {
                AvailableTrips.Remove(pTrip);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{pTrip.TripName} removed from system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{pTrip.TripName} not found on system\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }

    class Cruise
    {
        public bool debug = false;
        public string CruiseName { get; set; }
        public int CruiseID { get; private set; }
        private static int NextCruiseID { get; set; } = 0;
        public decimal CruiseCost { get; private set; } = 0;

        public List<Passenger> CruisePassengers { get; private set; }
        public List<Port> CruisePorts{ get; private set; }

        public Cruise(int id, string pCruiseName, decimal pCruiseCost)
        {
            CruiseName = pCruiseName;
            CruiseCost = pCruiseCost;
            CruiseID = id;
            if(id == NextCruiseID)
            {
                NextCruiseID++;
            }
            CruisePassengers = new List<Passenger>();
            CruisePorts = new List<Port>();
        }
        public Cruise(string pCruiseName, decimal pCruiseCost)
        {
            CruiseName = pCruiseName;
            CruiseCost = pCruiseCost;
            CruiseID = NextCruiseID;
            NextCruiseID++;
            CruisePassengers = new List<Passenger>();
            CruisePorts = new List<Port>();
        }
        public Cruise()
        {
            CruiseName = "Sample Cruise Name";
            CruiseID = NextCruiseID;
            NextCruiseID++;
            CruisePassengers = new List<Passenger>();
            CruisePorts = new List<Port>();
        }

        public void AddPort(Port pPort)
        {
            if (this.CruisePorts.Contains(pPort) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{this.ToString()} already has {pPort.ToString()}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                CruisePorts.Add(pPort);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pPort.PortName} Added to {this.CruiseName}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        public void RemovePort(Port pPort)
        {
            if (CruisePorts.Contains(pPort))
            {
                CruisePorts.Remove(pPort);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pPort.PortName} Removed from {this.CruiseName}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pPort.PortName} was not found\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void AddPassenger(Passenger pPassenger)
        {
            if (this.CruisePassengers.Contains(pPassenger) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{this.ToString()} already has {pPassenger.ToString()}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                CruisePassengers.Add(pPassenger);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pPassenger.FName} Added to {this.CruiseName}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemovePassenger(Passenger pPassenger)
        {
            if (CruisePassengers.Contains(pPassenger))
            {
                CruisePassengers.Remove(pPassenger);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pPassenger.FName} Removed from {this.CruiseName}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pPassenger.FName} was not found\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public override string ToString()
        {
            return $"{this.CruiseName} ({this.CruiseID})";
        }
    }

    class CruisePortDockManager
    {
        public Cruise Cruise { get; set; }
        public Port Port { get; set; }
        public int MaxDays { get; set; }

        public CruisePortDockManager(Cruise cruise, Port port, int days)
        {
            Cruise = cruise;
            Port = port;
            MaxDays = days;
        }
    }

    class CPD_PassengerTripManager
    {
        public Passenger _Passenger;
        public CruisePortDockManager _PortDockManager;
        public List<Trip> Trips;
        public CPD_PassengerTripManager(Passenger Passenger, CruisePortDockManager cruisePortDockManager)
        {
            _Passenger = Passenger;
            _PortDockManager = cruisePortDockManager;
            Trips = new List<Trip>();
        }
        public CPD_PassengerTripManager(Passenger Passenger)
        {
            _Passenger = Passenger;
        }
        public bool DaysRemaining()
        {
            if (Trips.Count == _PortDockManager.MaxDays)
            {
                return false;
            }
            else return true;
        }
        public int ReturnDaysBooked()
        {
            return Trips.Count;
        }
        public void BookTrip(Trip trip)
        {
            Trips.Add(trip);
        }
        public void RemoveTrip(Trip trip)
        {
            if (Trips.Contains(trip))
            {
                Trips.Remove(trip);
            }
            else
            {
                throw new Exception("Somehow this trip does not exist in CPD_PT list.");
            }
        }
    }

    class Passenger
    {
        public bool debug = false;
        public string FName { get; set; }
        public string SName { get; set; }
        public string Passport { get; set; }
        public List<Cruise> P_Cruises { get; set; }

        public decimal PassengerTotalCost { get; set; } = 0;
        
        public Passenger(string fname, string sname, string passport)
        {
            FName = fname;
            SName = sname;
            Passport = passport;
            P_Cruises = new List<Cruise>();
        }

        public bool IsCruiseAssignedToPassenger()
        {
            if (P_Cruises.Count > 0)
            {
                return true;
            }
            else { return false; }
        }
        public void AssignCruiseToPassenger(Cruise pCruise)
        {
            if (!P_Cruises.Contains(pCruise))
            {
                P_Cruises.Add(pCruise);
                pCruise.AddPassenger(this);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pCruise.ToString()} was assinged to {this.ToString()}");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            else if(P_Cruises.Contains(pCruise) && !pCruise.CruisePassengers.Contains(this))
            {
                pCruise.AddPassenger(this);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pCruise.ToString()} was assinged to {this.ToString()}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pCruise.ToString()} already assigned to Passenger\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }


        }
        public void UnAssignCruiseFromPassenger(Cruise pCruise)
        {
            if (P_Cruises.Contains(pCruise))
            {
                pCruise.RemovePassenger(this);
                P_Cruises.Remove(pCruise);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pCruise.ToString()} removed from {this.ToString()}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pCruise.ToString()} is not assigned to {this.ToString()}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public bool CheckFreeTripEligibility(CPD_PassengerTripManager pcpd)
        {
            if (pcpd.Trips.Count >= 2)
            {
                return false;
            }
            else return true;
        }


        public void CalculatePassengerTotalCost(CPD_PassengerTripManager pcpd)
        {
            decimal cost = 0;
            try { cost += pcpd._PortDockManager.Cruise.CruiseCost; } catch (NullReferenceException) { cost += 0; }
            try
            {
                CPD_PassengerTripManager cpd = pcpd;
                int freecounter = 0;
                foreach (Trip trip in cpd.Trips)
                {
                    if (freecounter >= 2)
                    {
                        cost += trip.TripCost;
                    }
                    freecounter++;
                }
            }
            catch (NullReferenceException) { cost += 0; }
            this.PassengerTotalCost = Math.Round(cost,2);
        }
        public override string ToString()
        {
            return $"{this.FName} {this.SName} ({this.Passport})";
        }
    }
    class Port
    {
        public bool debug = false;
        public string PortName { get; set; }
        public int PortID { get; private set; }
        private static int NextPortID { get; set; } = 1;
        public List<Trip> PortTrips { get; private set; }

        public Port(int portId, string portName)
        {
            PortID = portId;
            PortName = portName;
            if(portId == NextPortID)
            {
                NextPortID++;
            }
            PortTrips = new List<Trip>();
        }
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
            if (PortTrips.Contains(pTrip) && debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pTrip.TripName} Already exists in {this.PortName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                PortTrips.Add(pTrip);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{pTrip.TripName} Added to {this.PortName}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
        }
        public void RemoveTrip(Trip pTrip)
        {
            if (PortTrips.Contains(pTrip))
            {
                PortTrips.Remove(pTrip);
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{pTrip.TripName} Removed from  {this.PortName}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{pTrip.TripName} was not found\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
    class Trip
    {
        public string TripName { get; set; }
        public int TripID { get; private set; }
        private static int NextTripID { get; set; } = 1;
        public decimal TripCost { get; private set; } = 0;


        public Trip(string ptripName, int ptripID, decimal pTripCost)
        {
            TripName = ptripName;
            TripID = ptripID;
            TripCost = pTripCost;
            if (ptripID == NextTripID) { NextTripID++; }

        }
        public Trip(string pTripName, decimal pTripCost)
        {
            TripName = pTripName;

            TripCost = pTripCost;
            TripID = NextTripID;
            NextTripID++;
        }

        public Trip()
        {
            TripName = "Sample Trip Name";

            TripID = NextTripID;
            NextTripID++;
        }

        public override string ToString()
        {
            return $"ID:{TripID}, {TripName}";
        }
    }
}
