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
            if (Cruises.Contains(pCruise))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pCruise.CruiseName} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Cruises.Add(pCruise);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{pCruise.CruiseName} added to system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemoveCruise(Cruise pCruise)
        {
            if (Cruises.Contains(pCruise))
            {
                Cruises.Remove(pCruise);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pCruise.CruiseName} removed from system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pCruise.CruiseName} was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void AddPort(Port pPort)
        {
            if (AvailablePorts.Contains(pPort))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPort.PortName} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                AvailablePorts.Add(pPort);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{pPort.PortName} added to system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemovePort(Port pPort)
        {
            if (AvailablePorts.Contains(pPort))
            {
                AvailablePorts.Remove(pPort);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pPort.PortName} removed from system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPort.PortName} was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void AddPassanger(Passanger pPassanger)
        {
            if (Passangers.Contains(pPassanger))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Passangers.Add(pPassanger);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) added to system\n");
            Console.ForegroundColor = ConsoleColor.White;
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (Passangers.Contains(pPassanger))
            {
                Passangers.Remove(pPassanger);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) removed from system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void AddTrip(Trip pTrip)
        {
            if (AvailableTrips.Contains(pTrip))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pTrip.TripName} already exists in the system\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            AvailableTrips.Add(pTrip);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{pTrip.TripName} added to system\n");
            Console.ForegroundColor = ConsoleColor.White;
            // NEEDS CHECK TO PREVENT DUPLICATES ✔️
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

        public List<Passanger> CruisePassangers { get; private set; }
        public List<Port> CruisePorts{ get; private set; }

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
            if (this.CruisePorts.Contains(pPort))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{this.ToString()} already has {pPort.ToString()}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                CruisePorts.Add(pPort);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{pPort.PortName} Added to {this.CruiseName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void RemovePort(Port pPort)
        {
            if (CruisePorts.Contains(pPort))
            {
                CruisePorts.Remove(pPort);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pPort.PortName} Removed from {this.CruiseName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPort.PortName} was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void AddPassanger(Passanger pPassanger)
        {
            if (this.CruisePassangers.Contains(pPassanger))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{this.ToString()} already has {pPassanger.ToString()}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {

                CruisePassangers.Add(pPassanger);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{pPassanger.FName} Added to {this.CruiseName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            // NEEDS CHECK TO PREVENT DUPLICATES
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (CruisePassangers.Contains(pPassanger))
            {
                CruisePassangers.Remove(pPassanger);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pPassanger.FName} Removed from {this.CruiseName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPassanger.FName} was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
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
        public double Passport { get; set; }
        public Cruise AssignedCruise { get; private set; }
        public List<Trip> AssignedTrips { get; private set; }
        public List<Trip> TripsThatDontComeFree{ get; private set;}
        public decimal PassangerTotalCost { get; set; } = 0;
        
        public Passanger(string fname, string sname, double passport)
        {
            FName = fname;
            SName = sname;
            Passport = passport;
            AssignedTrips = new List<Trip>();
            TripsThatDontComeFree = new List<Trip>();
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
            pCruise.AddPassanger(this);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{pCruise.ToString()} was assinged to {this.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;

        }
        public void UnAssignCruiseFromPassanger()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{this.AssignedCruise.ToString()} removed from {this.ToString()}\n");
            Console.ForegroundColor = ConsoleColor.White;
            this.AssignedCruise = null;
        }
        public void AddToAssignedTrips(Trip pTrip)
        {
            if (AssignedTrips.Count >= 2 )
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nThis passanger has already reached their two free trips");
                Console.WriteLine($"Adding another trip will incur a cost of £{pTrip.TripCost}");
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Do you want to continue y or n?");
                        string response = Console.ReadLine().ToLower();
                        if (response == "y")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"Charge of £{pTrip.TripCost} applied");
                            this.AssignedTrips.Add(pTrip);
                            this.TripsThatDontComeFree.Add(pTrip);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        else if (response == "n")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Cancelled");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    }
                    catch { 
                        Console.ForegroundColor = ConsoleColor.DarkYellow; 
                        Console.WriteLine("Answer with either 'y' or 'n' ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }                
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                this.AssignedTrips.Add(pTrip);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{pTrip.ToString()} was assigned to {this.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
        public void RemoveAssignedTrip(Trip pTrip)
        {
            if (this.AssignedTrips.Contains(pTrip))
            {
                this.AssignedTrips.Remove(pTrip);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n {pTrip.ToString()} was removed from {this.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pTrip.ToString()} was not found");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void CalculatePassangerTotalCost()
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
            this.PassangerTotalCost = Math.Round(cost,2);
        }
        public void fixCost()
        {
            if (this.PassangerTotalCost < 0)
            {
                this.PassangerTotalCost = 0;
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

        public List<Trip> PortTrips { get; private set; }

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
            if (PortTrips.Contains(pTrip))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pTrip.TripName} Already exists in {this.PortName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                PortTrips.Add(pTrip);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{pTrip.TripName} Added to {this.PortName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
        public void RemoveTrip(Trip pTrip)
        {
            if (PortTrips.Contains(pTrip))
            {
                PortTrips.Remove(pTrip);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pTrip.TripName} Removed from  {this.PortName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow ;
                Console.WriteLine($"\n{pTrip.TripName} was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
    class Trip
    {
        public string TripName { get; set; }
        public int TripID { get; private set; }
        private static int NextTripID { get; set; } = 1;
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) added to {this.TripName}\n");
            Console.ForegroundColor = ConsoleColor.White;
            // NEEDS CHECK TO PREVENT DUPLICATES  -- NOT NEEDED AS PASSANGER MAY WANT THE SAME TRIP TWICE IN CRUISE - ON DIFFERENT PORTS
        }
        public void RemovePassanger(Passanger pPassanger)
        {
            if (TripPassangers.Contains(pPassanger))
            {
                TripPassangers.Remove(pPassanger);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) removed from {this.TripName}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{pPassanger.FName + pPassanger.SName} ({pPassanger.Passport}) was not found\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
