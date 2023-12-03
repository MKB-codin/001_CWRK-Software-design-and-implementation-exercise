using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SED_Coursework
{
    #region MainMenu
    class SystemManagerMenu : ConsoleMenu
    {
        AdminSystem _system;
        public SystemManagerMenu(AdminSystem system)
        {
            _system = system;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new ManageCruisesMenu(_system));
            _menuItems.Add(new ManagePortsMenu(_system));
            _menuItems.Add(new ManagePassangersMenu(_system));
            _menuItems.Add(new ManageTripsMenu(_system)); 
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "System Manager Menu";
        }

    }
    #endregion

    #region Cruises
    class ManageCruisesMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ManageCruisesMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddCruiseMenuItem(_system));
            if(_system.Cruises.Count > 0)
            {
                _menuItems.Add(new ViewCruisesMenu(_system));
            }
            _menuItems.Add(new ExitMenuItem(this));

        }
        public override string MenuText()
        {
            return "Manage Cruises";
        }
        
    }
    class AddCruiseMenuItem : MenuItem
    {
        AdminSystem _system;
        public AddCruiseMenuItem(AdminSystem system)
        {
            _system = system;
        }
        public override void Select()
        { 
            Console.WriteLine("Enter Cruise Name:");
            string newCruiseName = Console.ReadLine().ToLower();
            decimal newCruiseCost = Math.Round(ConsoleHelpers.GetDecimalInRange(0, 10000000000, "Enter Cruise cost"),2);
            Cruise newCruise = new Cruise(newCruiseName, newCruiseCost);
            _system.AddCruise(newCruise);
        }
        public override string MenuText()
        {
            return "Add Cruise";
        }
    }
    class ViewCruisesMenu : ConsoleMenu
    {
        AdminSystem _system;

        public ViewCruisesMenu(AdminSystem system)
        {
            _system = system;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Cruise cruise in _system.Cruises.OrderBy(o => o.CruiseID).ToList())
            {
                _menuItems.Add(new ViewCruiseMenu(_system,cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Cruises";
        }
    }
    class ViewCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _Cruise;
        public ViewCruiseMenu(AdminSystem system,Cruise cruise)
        {
            _system = system;
            _Cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new PassangersCruiseMenu(_system, _Cruise));
            _menuItems.Add(new PortsCruiseMenu(_system, _Cruise));
            _menuItems.Add(new RemoveCruiseFromSystemMenuItem(_system,_Cruise));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }


    class PassangersCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        public PassangersCruiseMenu(AdminSystem system, Cruise cruise)
        {
            _system = system;
            _cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPassangerToCruiseMenu(_system.Passangers, _cruise));
            if(_cruise.CruisePassangers.Count > 0)
            {
                _menuItems.Add(new ViewPassangersInCruiseMenu(_system,_cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Passangers";
        }
    }
    class AddPassangerToCruiseMenu : ConsoleMenu
    {
        List<Passanger> _passangers;
        Cruise _cruise;
        public AddPassangerToCruiseMenu(List<Passanger> passangers, Cruise cruise)
        {
            _passangers = passangers;
            _cruise = cruise;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Passanger passanger in _passangers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new AddPassangerToCruiseMenuItem(_cruise, passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Add Passanger to Cruise";
        }
    }
    class AddPassangerToCruiseMenuItem : MenuItem
    {
        Cruise _cruise;
        Passanger _passanger;
        public AddPassangerToCruiseMenuItem(Cruise cruise,  Passanger passanger)
        {
            _cruise = cruise;
            _passanger = passanger;
        }
        public override void Select()
        {
            _passanger.AssignCruiseToPassanger(_cruise);
        }
        public override string MenuText()
        {
            return _passanger.ToString();
        }
    }
 
    class ViewPassangersInCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        public ViewPassangersInCruiseMenu(AdminSystem system, Cruise cruise)
        {
            _cruise = cruise;
            _system = system;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach( Passanger passanger in _cruise.CruisePassangers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new ViewPassangerInCruiseMenu(_system,_cruise,passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Passanger in Cruise";
        }
    }
    class ViewPassangerInCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        Passanger _passanger;
        public ViewPassangerInCruiseMenu(AdminSystem system,Cruise crusie, Passanger passanger)
        {
            _system = system;
            _cruise = crusie;
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddTripToPassanger_PortMenu(_system, _cruise, _passanger));
            if (_passanger.AssignedTrips.Count > 0)
            {
                _menuItems.Add(new RemoveTripFromPassanger_PortMenu(_system, _cruise, _passanger));
            }
            if (_cruise.CruisePassangers.Count > 0)
            { 
                _menuItems.Add(new RemovePassangerFromCruiseMenuItem(_cruise, _passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _passanger.ToString();
        }
    }

    class AddTripToPassanger_PortMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        Passanger _passanger;
        CruisePortDockManager cruisePortDockManager;
        CPD_PassangerTripManager _passangerTripManager;
        public AddTripToPassanger_PortMenu(AdminSystem system, Cruise cruise, Passanger passanger)
        {
            _system = system;
            _cruise = cruise;
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Port port in _cruise.CruisePorts.OrderBy(o => o.PortID))
            {
                cruisePortDockManager = _system.CruisePortDockManagers.FirstOrDefault(o => o.Port == port && o.Cruise == _cruise);
                if (_system.CPD_PassTripManagers.FirstOrDefault(o => o._Passanger == _passanger && o._PortDockManager == cruisePortDockManager) != null)
                {
                    _passangerTripManager = _system.CPD_PassTripManagers.FirstOrDefault(o => o._Passanger == _passanger && o._PortDockManager == cruisePortDockManager);
                }
                else
                {
                    _passangerTripManager = new CPD_PassangerTripManager(_passanger, cruisePortDockManager);
                    _system.AddCPD_PassTripManager(_passangerTripManager);
                }
                _menuItems.Add(new AddTripToPassanger_TripMenu(_passangerTripManager,_cruise, port, _passanger));
            }
            
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Assign a trip to Passanger";
        }
    }
    class AddTripToPassanger_TripMenu : ConsoleMenu
    {

        Port _port;
        Passanger _passanger;
        Cruise _cruise;
        CPD_PassangerTripManager _passangerTripManager;
        public AddTripToPassanger_TripMenu(CPD_PassangerTripManager passangerTripManager,Cruise cruise, Port port, Passanger passanger)
        {

            _passangerTripManager = passangerTripManager;
            _cruise = cruise;
            _port = port;
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Trip trip in _port.PortTrips.OrderBy(o => o.TripID))
            {
                if (_passangerTripManager.DaysRemaining())
                { 
                    _menuItems.Add(new AddTripToPassangerMenuItem(_passanger, _passangerTripManager, trip)); 
                }
                else
                {
                    _menuItems.Add(new AddTripToPassanger_NoTripsRemainingMenuItem(_port,trip));   
                }
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Port : {_port.ToString()} (Trips Assigned: {_passangerTripManager.ReturnDaysBooked()}/{_passangerTripManager._PortDockManager.MaxDays})";
        }
    }

    class AddTripToPassangerMenuItem : MenuItem
    {
        Passanger _passanger;
        Trip _trip;
        CPD_PassangerTripManager _passangerTripManager;
        public AddTripToPassangerMenuItem(Passanger passanger, CPD_PassangerTripManager passangerTripManager, Trip trip)
        {
            _passangerTripManager = passangerTripManager;
            _passanger = passanger;
            _trip = trip;
        }
        public override void Select()
        {
            if (!_passanger.CheckFreeTripEligibility())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nThis passanger has already used their two free trips");
                Console.WriteLine($"Adding another trip will incur a cost of £{_trip.TripCost}");
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Do you want to continue y or n?");
                        string response = Console.ReadLine().ToLower();
                        if (response == "y")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"\n{_trip.ToString()} was assigned to {_passanger.ToString()}");
                            Console.WriteLine($"Charge of £{_trip.TripCost} applied");
                            _passanger.AddToAssignedTrips(_trip);
                            _passanger.AddToTripsThatDontComeFree(_trip);
                            _passangerTripManager.BookTrip(_trip);
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
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Answer with either 'y' or 'n' ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                _passangerTripManager.BookTrip(_trip);
                _passanger.AddToAssignedTrips(_trip);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{_trip.ToString()} was assigned to {_passanger.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }
    class AddTripToPassanger_NoTripsRemainingMenuItem : MenuItem
    {
        Port _port;
        Trip _trip;
        public AddTripToPassanger_NoTripsRemainingMenuItem(Port port, Trip trip)
        {
            _port = port;
            _trip = trip;
        }
        public override void Select()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"This {_port} has reached its maximum number of trips. Please Remove some trips to add more.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }
    
    class RemoveTripFromPassanger_PortMenu : ConsoleMenu
    {
        Passanger _passanger;
        AdminSystem _system;
        Cruise _cruise;
        CPD_PassangerTripManager _passangerTripManager;
        public RemoveTripFromPassanger_PortMenu(AdminSystem Adminsystem, Cruise cruise,Passanger passanger)
        {
            _passanger = passanger;
            _cruise = cruise;
            _system = Adminsystem;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            
            foreach (Port port in _cruise.CruisePorts.OrderBy(o => o.PortID))
            {
                _passangerTripManager = _system.CPD_PassTripManagers.FirstOrDefault(o => o._Passanger == _passanger && o._PortDockManager.Port == port );
                _menuItems.Add(new RemoveTripFromPassangerTripMenu(_passangerTripManager, _cruise, port, _passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Remove a trip from this passanger";
        }
    }
    class RemoveTripFromPassangerTripMenu : ConsoleMenu
    {
        Passanger _passanger;
        CPD_PassangerTripManager _passangerTripManager;
        Cruise _cruise;
        Port _port;
        public RemoveTripFromPassangerTripMenu(CPD_PassangerTripManager PassangerTripManager, Cruise cruise, Port port, Passanger passanger)
        {
            _passangerTripManager = PassangerTripManager;
            _cruise = cruise;
            _port = port;
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Trip trip in _passangerTripManager.Trips.OrderBy(o => o.TripID))
            {
                if (_passangerTripManager.ReturnDaysBooked() > 0)
                {
                    _menuItems.Add(new RemoveTripFromPassangerMenuItem(_passanger, _passangerTripManager,trip));
                }
                else
                {
                    _menuItems.Add(new RemoveTripFromPassangerNoTripsLeftMenuItem(_port));
                }
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Port : {_port.ToString()} (Trips Assigned: {_passangerTripManager.ReturnDaysBooked()}/{_passangerTripManager._PortDockManager.MaxDays})";
        }
    }
    class RemoveTripFromPassangerMenuItem : MenuItem
    {
        Passanger _passanger;
        CPD_PassangerTripManager _passangerTripManager;
        Trip _trip;
        public RemoveTripFromPassangerMenuItem(Passanger passanger,CPD_PassangerTripManager passangerTripManager ,Trip trip)
        {
            _passanger = passanger;
            _passangerTripManager = passangerTripManager;
            _trip = trip;
        }
        public override void Select()
        {
            _passangerTripManager.RemoveTrip(_trip);
            _passanger.RemoveAssignedTrip(_trip);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{_trip.ToString()} was removed from {_passanger} at {_passangerTripManager._PortDockManager.Port.ToString()}");
            Console.ForegroundColor= ConsoleColor.White;
        }
        public override string MenuText()
        {
            return $"Trip: {_trip.ToString()}";
        }
    }
    class RemoveTripFromPassangerNoTripsLeftMenuItem : MenuItem
    {
        Port _port;
        public RemoveTripFromPassangerNoTripsLeftMenuItem(Port port)
        {
            _port = port;
        }
        public override void Select()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"This port has no more trips to remove. Add some trips to remove them.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return _port.ToString();
        }
    }

    class RemovePassangerFromCruiseMenuItem : MenuItem
    {
        Cruise _cruise;
        Passanger _passanger;
        public RemovePassangerFromCruiseMenuItem(Cruise cruise, Passanger passanger)
        {
            _cruise = cruise;
            _passanger = passanger;
        }
        public override void Select()
        {
            _cruise.RemovePassanger(_passanger);
        }
        public override string MenuText()
        {
            return "Remove Passanger From Cruise";
        }
    }


    class PortsCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        public PortsCruiseMenu(AdminSystem system, Cruise cruise)
        {
            _system = system;
            _cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPortToCruiseMenu(_system, _cruise));
            if (_cruise.CruisePassangers != null)
            {
                _menuItems.Add(new RemovePortFromCruiseMenu(_cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Ports";
        }
    }
    class AddPortToCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        public AddPortToCruiseMenu(AdminSystem adminSystem, Cruise cruise)
        {
            _system = adminSystem;
            _cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Port port in _system.AvailablePorts)
            {
                _menuItems.Add(new AddPortToCruiseMenuItem(_system,_cruise, port));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Add a Port to Cruise";
        }
    } 
    class AddPortToCruiseMenuItem : MenuItem
    {
        AdminSystem _system;
        Cruise _cruise;
        Port _port;
        public AddPortToCruiseMenuItem(AdminSystem adminSystem, Cruise cruise, Port port)
        {
            _system = adminSystem;
            _cruise = cruise;
            _port = port;
        }
        public override void Select()
        {
            int daysDocked = int.Parse(ConsoleHelpers.GetDecimalInRange(1, 7, "How many days wi ll this cruise stay at this port?").ToString());
            CruisePortDockManager cruisePortDockManager = new CruisePortDockManager(_cruise, _port, daysDocked); 
            _system.AddC_P_DManager(cruisePortDockManager);
            _cruise.AddPort(_port);
        }
        public override string MenuText()
        {
            return _port.ToString();
        }
    } 
    class RemovePortFromCruiseMenu : ConsoleMenu
    {
        Cruise _cruise;
        public RemovePortFromCruiseMenu(Cruise cruise)
        {
            _cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Port port in _cruise.CruisePorts.OrderBy(o=>o.PortID))
            {
                _menuItems.Add(new RemovePortFromCruiseMenuItem(_cruise, port));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Remove a Port from Cruise";
        }
    }
    class RemovePortFromCruiseMenuItem : MenuItem
    {
        Cruise _cruise;
        Port _port;
        public RemovePortFromCruiseMenuItem(Cruise cruise , Port port)
        {
            _cruise = cruise;
            _port = port;
        }
        public override void Select()
        {
            _cruise.RemovePort(_port);
        }
        public override string MenuText()
        {
            return _port.ToString();
        }
    }


    class RemoveCruiseFromSystemMenuItem : MenuItem
    {
        AdminSystem _system;
        Cruise _cruise;
        public RemoveCruiseFromSystemMenuItem(AdminSystem system, Cruise cruise)
        {
            _system = system;
            _cruise = cruise;
        }

        public override void Select()
        {
            _system.RemoveCruise(_cruise);
        }
        public override string MenuText()
        {
            return "Remove Cruise from System";
        }
    }
#endregion

    #region Ports
    class ManagePortsMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ManagePortsMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPortMenuItem(_system));
            if (_system.AvailablePorts.Count > 0)
            {
                _menuItems.Add(new ViewPortsMenu(_system));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Manage Ports";
        }
    }

    class AddPortMenuItem : MenuItem
    {
        AdminSystem _system;
        public AddPortMenuItem(AdminSystem system)
        {
            _system = system;
        }

        public override void Select()
        {
            string newPortName = "Sample Port Name";
            Console.WriteLine("What is the name of the port?");
            newPortName = Console.ReadLine().ToLower();
            Port newPort = new Port(newPortName);
            _system.AddPort(newPort);
        }
        public override string MenuText()
        {
            return "Add Port";
        }
    }

    class ViewPortsMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ViewPortsMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Port port in _system.AvailablePorts.OrderBy(o => o.PortID))
            { 
                _menuItems.Add(new ViewPortMenu(_system,port));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Port(s)";
        }
    }
    class ViewPortMenu : ConsoleMenu
    {
        AdminSystem _system;
        Port _port;
        public ViewPortMenu(AdminSystem system,Port port)
        {
            _system = system;
            _port = port;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddTripsToPortMenu(_port, _system.AvailableTrips));
            if (_port.PortTrips.Count > 0)
            {
                _menuItems.Add(new RemoveTripsFromPortMenu(_port));
            }
            _menuItems.Add(new RemovePortFromSystemMenuItem(_system, _port));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _port.ToString();
        }
    }

    class AddTripsToPortMenu : ConsoleMenu
    {
        Port _port;
        List<Trip> _trips;
        public AddTripsToPortMenu(Port port, List<Trip> trips)
        {
            _port = port;
            _trips = trips;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Trip trip in _trips)
            {
                _menuItems.Add(new AddTripToPortMenuItem(trip, _port));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Add Trip to Port";
        }
    }
    class AddTripToPortMenuItem : MenuItem
    { 
        Port _port;
        Trip _trip;
        public AddTripToPortMenuItem(Trip trip, Port port)
        {
            _trip = trip;
            _port = port;
        }
        public override void Select()
        {
            _port.AddTrip(_trip);
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }
    class RemoveTripsFromPortMenu : ConsoleMenu
    {
        Port _port;
        public RemoveTripsFromPortMenu(Port port)
        {
            _port = port;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Trip trip in _port.PortTrips.OrderBy(o=>o.TripID))
            {
                _menuItems.Add(new RemoveTripFromPortMenuItem(_port, trip));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Remove Trip(s) from Port";
        }

    }
    class RemoveTripFromPortMenuItem : MenuItem
    {
        Port _port;
        Trip _trip;
        public RemoveTripFromPortMenuItem(Port port, Trip trip)
        {
            _port = port;
            _trip = trip;
        }

        public override void Select()
        {
            _port.RemoveTrip(_trip);
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }

    class RemovePortFromSystemMenuItem : MenuItem
    {
        AdminSystem _system;
        Port _port;
        public RemovePortFromSystemMenuItem(AdminSystem system, Port port)
        {
            _system = system;
            _port = port;
        }

        public override void Select()
        {
            _system.RemovePort(_port);
        }
        public override string MenuText()
        {
            return "Remove Port From System";
        }
    }
    #endregion

    #region Passangers
    class ManagePassangersMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ManagePassangersMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPassangerMenu(_system));
            if (_system.Passangers.Count > 0)
            {
                _menuItems.Add(new ViewPassangersMenu(_system));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Manage Passangers";
        }
    }

    class AddPassangerMenu : MenuItem
    {
        AdminSystem _system;
        public AddPassangerMenu(AdminSystem system)
        {
            _system = system;
        }
        public override string MenuText()
        {
            return "Add Passanger";
        }
        public override void Select()
        {
            string newPassangerFName;
            string newPassangerSName;
            double newPassangerPassportNumber = 0;

            Console.WriteLine("What is the passanger's first name?");
            newPassangerFName = Console.ReadLine().ToLower();
            Console.WriteLine("What is the passanger's surname?");
            newPassangerSName = Console.ReadLine().ToLower();
            while (true)
            {
                Console.WriteLine("What is their Passport ID (9 digits)");
                try
                {
                    newPassangerPassportNumber = double.Parse(Console.ReadLine());
                }
                catch
                {
                    newPassangerPassportNumber = 0;
                    Console.WriteLine("Please enter a 9 digit number.");
                }
                if(newPassangerPassportNumber.ToString().Length == 9)
                {
                    break;
                }
            }
            Passanger newPassanger = new Passanger(newPassangerFName, newPassangerSName, newPassangerPassportNumber);
            _system.AddPassanger(newPassanger);
        }
    }

    class ViewPassangersMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ViewPassangersMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Passanger passanger in _system.Passangers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new ViewPassangerMenu(_system,passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "View Passanger(s)";
        }
    }
    class ViewPassangerMenu : ConsoleMenu
    {
        AdminSystem _system;
        Passanger _passanger;
        public ViewPassangerMenu(AdminSystem system,Passanger passanger)
        {
            _system = system;
            _passanger = passanger;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            if ( _passanger.IsCruiseAssignedToPassanger())
            {
                _menuItems.Add(new ViewPassangerCruiseMenuItem(_passanger));
                _menuItems.Add(new UnAssignCruiseFromPassangerMenu(_passanger));
            }
            else
            {
                _menuItems.Add(new AssignCruiseToPassangerMenu(_system.Cruises, _passanger));
            }
            _menuItems.Add(new ViewPassangerPassangerTotalCostMenu(_passanger));
            _menuItems.Add(new RemovePassanger_SystemMenuItem(_system, _passanger));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _passanger.ToString();
        }
    }

    class AssignCruiseToPassangerMenu : ConsoleMenu
    {
        List<Cruise> _Cruises;
        Passanger _Passanger;
        public AssignCruiseToPassangerMenu(List<Cruise> cruises, Passanger passanger)
        {
            _Cruises = cruises;
            _Passanger = passanger;

        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Cruise cruise in _Cruises.OrderBy(o => o.CruiseID))
            {
                _menuItems.Add(new AssignCruiseToPassangerMenuItem(cruise,_Passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Assign Cruise to Passanger";
        }
    }
    class AssignCruiseToPassangerMenuItem : MenuItem
    {
        Cruise _Cruise;
        Passanger _Passanger;
        public AssignCruiseToPassangerMenuItem(Cruise cruise, Passanger passanger)
        {
            _Cruise = cruise;
            _Passanger = passanger;
        }

        public override void Select()
        {
            _Passanger.AssignCruiseToPassanger(_Cruise);
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }

    class ViewPassangerPassangerTotalCostMenu : ConsoleMenu
    {
        Passanger _Passanger;
        public ViewPassangerPassangerTotalCostMenu(Passanger passanger)
        {
            _Passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Cruise cruise in _Passanger.P_Cruises)
            {
                _menuItems.Add(new ViewPassangerPassangerTotalCostMenuItem(_Passanger, cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Total Cost of Cruise";
        }
    }

    class ViewPassangerPassangerTotalCostMenuItem : MenuItem
    {
        Passanger _Passanger;
        Cruise _Cruise;
        public ViewPassangerPassangerTotalCostMenuItem(Passanger passanger, Cruise cruise)
        {
            _Passanger = passanger;
            _Cruise = cruise;
        }

        public override void Select()
        {
            _Passanger.CalculatePassangerTotalCost(_Cruise);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"£{_Passanger.PassangerTotalCost}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }
    class ViewPassangerCruiseMenuItem : MenuItem
    {
        Passanger _passanger;
        public ViewPassangerCruiseMenuItem(Passanger passanger)
        {
            _passanger = passanger;
        }
        public override void Select()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{_passanger.ToString()} Assigned to Cruises:");
            foreach(Cruise cruise in _passanger.P_Cruises)
            {
                Console.WriteLine( cruise.ToString() );
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return "View Cruise Assigned to Passanger";
        }
    }

    class UnAssignCruiseFromPassangerMenu : ConsoleMenu
    {
        Passanger _Passanger;
        public UnAssignCruiseFromPassangerMenu(Passanger passanger)
        {
            _Passanger = passanger;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Cruise cruise in _Passanger.P_Cruises)
            {
                _menuItems.Add(new UnAssignCruiseFromPassangerMenuItem(_Passanger,cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "UnAssign Cruise from Passanger";
        }
    }

    class UnAssignCruiseFromPassangerMenuItem : MenuItem
    {
        Passanger _Passanger;
        Cruise _Cruise;
        public UnAssignCruiseFromPassangerMenuItem(Passanger passanger, Cruise cruise)
        {
            _Passanger = passanger;
            _Cruise = cruise;
        }
        public override void Select()
        {
            _Passanger.UnAssignCruiseFromPassanger(_Cruise);
        }
        public override string MenuText()
        {
            return "UnAssign Cruise from Passanger";
        }
    }

    class RemovePassanger_SystemMenuItem : MenuItem
    {
        AdminSystem _system;
        Passanger _passanger;
        public RemovePassanger_SystemMenuItem(AdminSystem system, Passanger passanger)
        {
            _system = system;
            _passanger = passanger;
        }
        public override void Select()
        {
            _system.RemovePassanger(_passanger);
        }
        public override string MenuText()
        {
            return "Remove Passanger from System";
        }
    }
    #endregion

    #region Trips
    class ManageTripsMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ManageTripsMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddTripMenuItem(_system));
            if (_system.AvailableTrips.Count > 0)
            {
                _menuItems.Add(new ViewTripsMenu(_system));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Manage Trips";
        }
    }
    class AddTripMenuItem : MenuItem
    {
        AdminSystem _system;
        public AddTripMenuItem(AdminSystem system)
        {
            _system = system;
        }

        public override void Select()
        {
            string newTripName = "Sample Trip Name";
            Console.WriteLine("Enter Trip Name");
            newTripName = Console.ReadLine().ToLower();
            decimal newTripCost = Math.Round(ConsoleHelpers.GetDecimalInRange(0, 10000000000, "Enter Trip cost"),2);
            Trip newTrip = new Trip(newTripName, newTripCost);
            _system.AddTrip(newTrip);
        }
        public override string MenuText()
        {
            return "Add Trip";
        }
    }
    class ViewTripsMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ViewTripsMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Trip trip in _system.AvailableTrips.OrderBy(o => o.TripID))
            {
                _menuItems.Add(new ViewTripMenu(trip,_system));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Trip(s)";
        }
    }
    class ViewTripMenu : ConsoleMenu
    {
        Trip _trip;
        AdminSystem _system;
        public ViewTripMenu(Trip trip, AdminSystem system)
        {
            _trip = trip;
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new ViewTripCostMenuItem(_trip));
            _menuItems.Add(new RemoveTripFromSystemMenuItem(_system,_trip));
            _menuItems.Add(new ExitMenuItem(this)); 
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }
    class ViewTripCostMenuItem : MenuItem
    {
        Trip _trip;
        public ViewTripCostMenuItem(Trip trip)
        {
            _trip = trip;
        }

        public override void Select()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n£{_trip.TripCost}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return "View Cost of Trip";
        }
    }
    class RemoveTripFromSystemMenuItem : MenuItem
    {
        AdminSystem _system;
        Trip _trip;
        public RemoveTripFromSystemMenuItem(AdminSystem system, Trip trip)
        {
            _system = system;
            _trip = trip;
        }
        public override void Select()
        {
            _system.RemoveTrip(_trip);
        }
        public override string MenuText()
        {
            return "Remove Trip from System";
        }
    }
    #endregion
}
