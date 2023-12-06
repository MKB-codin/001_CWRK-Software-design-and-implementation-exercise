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
            _menuItems.Add(new ManagePassengersMenu(_system));
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
            _system.debug = !_system.debug;
            _system.AddCruise(newCruise);
            _system.debug = !_system.debug;
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
            _menuItems.Add(new PassengersCruiseMenu(_system, _Cruise));
            _menuItems.Add(new PortsCruiseMenu(_system, _Cruise));
            _menuItems.Add(new RemoveCruiseFromSystemMenuItem(_system,_Cruise));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }


    class PassengersCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        public PassengersCruiseMenu(AdminSystem system, Cruise cruise)
        {
            _system = system;
            _cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPassengerToCruiseMenu(_system.Passengers, _cruise));
            if(_cruise.CruisePassengers.Count > 0)
            {
                _menuItems.Add(new ViewPassengersInCruiseMenu(_system,_cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Passengers";
        }
    }
    class AddPassengerToCruiseMenu : ConsoleMenu
    {
        List<Passenger> _Passengers;
        Cruise _cruise;
        public AddPassengerToCruiseMenu(List<Passenger> Passengers, Cruise cruise)
        {
            _Passengers = Passengers;
            _cruise = cruise;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Passenger Passenger in _Passengers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new AddPassengerToCruiseMenuItem(_cruise, Passenger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Add Passenger to Cruise";
        }
    }
    class AddPassengerToCruiseMenuItem : MenuItem
    {
        Cruise _cruise;
        Passenger _Passenger;
        public AddPassengerToCruiseMenuItem(Cruise cruise,  Passenger Passenger)
        {
            _cruise = cruise;
            _Passenger = Passenger;
        }
        public override void Select()
        {
            _Passenger.debug = !_Passenger.debug;
            _Passenger.AssignCruiseToPassenger(_cruise);
        }
        public override string MenuText()
        {
            return _Passenger.ToString();
        }
    }
 
    class ViewPassengersInCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        public ViewPassengersInCruiseMenu(AdminSystem system, Cruise cruise)
        {
            _cruise = cruise;
            _system = system;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach( Passenger Passenger in _cruise.CruisePassengers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new ViewPassengerInCruiseMenu(_system,_cruise,Passenger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Passenger in Cruise";
        }
    }
    class ViewPassengerInCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        Passenger _Passenger;
        public ViewPassengerInCruiseMenu(AdminSystem system,Cruise crusie, Passenger Passenger)
        {
            _system = system;
            _cruise = crusie;
            _Passenger = Passenger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new ViewPassengerInCruise_PortMenu(_system, _cruise, _Passenger));
            if (_cruise.CruisePassengers.Count > 0)
            { 
                _menuItems.Add(new RemovePassengerFromCruiseMenuItem(_cruise, _Passenger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _Passenger.ToString();
        }
    }
    class ViewPassengerInCruise_PortMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        Passenger _Passenger;
        public ViewPassengerInCruise_PortMenu(AdminSystem system, Cruise cruise, Passenger Passenger)
        {
            _system = system;
            _cruise = cruise;
            _Passenger = Passenger;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Port port in _cruise.CruisePorts.OrderBy(o => o.PortID))
            {
                _menuItems.Add(new ViewPassengerInCruise_PortOptionsMenu(_system,_cruise,_Passenger,port));
            }

            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Passenger details at Port";
        }
    }
    class ViewPassengerInCruise_PortOptionsMenu : ConsoleMenu
    {
        AdminSystem _system;
        Cruise _cruise;
        Passenger _Passenger;
        Port _port;
        CruisePortDockManager cruisePortDockManager;
        CPD_PassengerTripManager _PassengerTripManager;
        public ViewPassengerInCruise_PortOptionsMenu(AdminSystem system, Cruise cruise, Passenger Passenger,Port port)
        {
            _system = system;
            _cruise = cruise;
            _Passenger = Passenger;
            _port = port;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();

            cruisePortDockManager = _system.CruisePortDockManagers.FirstOrDefault(o => o.Port == _port && o.Cruise == _cruise);
            if (_system.CPD_PassTripManagers.FirstOrDefault(o => o._Passenger == _Passenger && o._PortDockManager == cruisePortDockManager) != null)
            {
                _PassengerTripManager = _system.CPD_PassTripManagers.FirstOrDefault(o => o._Passenger == _Passenger && o._PortDockManager == cruisePortDockManager);
            }
            else
            {
                _PassengerTripManager = new CPD_PassengerTripManager(_Passenger, cruisePortDockManager);
                _system.AddCPD_PassTripManager(_PassengerTripManager);
            }

            _menuItems.Add(new ViewPassengerInCruise_AddTripMenu(_PassengerTripManager));
            if (_PassengerTripManager.Trips.Count > 0)
            {
                _menuItems.Add(new ViewPassengerInCruise_RemoveTripMenu(_PassengerTripManager));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Port: {_port.ToString()}";
        }
    }
    class ViewPassengerInCruise_AddTripMenu : ConsoleMenu
    {
        CPD_PassengerTripManager _PassengerTripManager;
        public ViewPassengerInCruise_AddTripMenu(CPD_PassengerTripManager cPD_PassengerTripManager)
        {
            _PassengerTripManager = cPD_PassengerTripManager;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddTripToPassenger_TripMenu(_PassengerTripManager));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Add Trip to Passenger at Port";
        }
    }
    class AddTripToPassenger_TripMenu : ConsoleMenu
    {


        CPD_PassengerTripManager _PassengerTripManager;
        public AddTripToPassenger_TripMenu(CPD_PassengerTripManager PassengerTripManager)
        {

            _PassengerTripManager = PassengerTripManager;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Trip trip in _PassengerTripManager._PortDockManager.Port.PortTrips.OrderBy(o => o.TripID))
            {
                if (_PassengerTripManager.DaysRemaining())
                { 
                    _menuItems.Add(new AddTripToPassengerMenuItem(_PassengerTripManager, trip)); 
                }
                else
                {
                    _menuItems.Add(new AddTripToPassenger_NoTripsRemainingMenuItem(_PassengerTripManager._PortDockManager.Port,trip));   
                }
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Port : {_PassengerTripManager._PortDockManager.Port.ToString()} (Trips Assigned: {_PassengerTripManager.ReturnDaysBooked()}/{_PassengerTripManager._PortDockManager.MaxDays})";
        }
    }

    class AddTripToPassengerMenuItem : MenuItem
    {
        Passenger _Passenger;
        Trip _trip;
        CPD_PassengerTripManager _PassengerTripManager;
        public AddTripToPassengerMenuItem(CPD_PassengerTripManager PassengerTripManager, Trip trip)
        {
            _PassengerTripManager = PassengerTripManager;
            _Passenger = _PassengerTripManager._Passenger;
            _trip = trip;
        }
        public override void Select()
        {
            if (!_PassengerTripManager.CheckFreeTripEligibility())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nThis Passenger has already used their two free trips");
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
                            Console.WriteLine($"\n{_trip.ToString()} was assigned to {_Passenger.ToString()}");
                            Console.WriteLine($"Charge of £{_trip.TripCost} applied");
                            _PassengerTripManager.BookTrip(_trip);
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
                _PassengerTripManager.BookTrip(_trip);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{_trip.ToString()} was assigned to {_Passenger.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }
    class AddTripToPassenger_NoTripsRemainingMenuItem : MenuItem
    {
        Port _port;
        Trip _trip;
        public AddTripToPassenger_NoTripsRemainingMenuItem(Port port, Trip trip)
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

    class ViewPassengerInCruise_RemoveTripMenu : ConsoleMenu
    {
        CPD_PassengerTripManager _PassengerTripManager;
        public ViewPassengerInCruise_RemoveTripMenu(CPD_PassengerTripManager cPD_PassengerTripManager)
        {
            _PassengerTripManager = cPD_PassengerTripManager;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new RemoveTripFromPassengerTripMenu(_PassengerTripManager));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Remove Trip from Passenger at Port: {_PassengerTripManager._PortDockManager.Port.ToString()}";
        }
    }
    class RemoveTripFromPassengerTripMenu : ConsoleMenu
    {
        Passenger _Passenger;
        CPD_PassengerTripManager _PassengerTripManager;
        Port _port;
        public RemoveTripFromPassengerTripMenu(CPD_PassengerTripManager PassengerTripManager)
        {
            _PassengerTripManager = PassengerTripManager;
            _port = _PassengerTripManager._PortDockManager.Port;
            _Passenger = _PassengerTripManager._Passenger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Trip trip in _PassengerTripManager.Trips.OrderBy(o => o.TripID))
            {
                if (_PassengerTripManager.ReturnDaysBooked() > 0)
                {
                    _menuItems.Add(new RemoveTripFromPassengerMenuItem(_Passenger, _PassengerTripManager,trip));
                }
                else
                {
                    _menuItems.Add(new RemoveTripFromPassengerNoTripsLeftMenuItem(_port));
                }
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return $"Port : {_port.ToString()} (Trips Assigned: {_PassengerTripManager.ReturnDaysBooked()}/{_PassengerTripManager._PortDockManager.MaxDays})";
        }
    }
    class RemoveTripFromPassengerMenuItem : MenuItem
    {
        Passenger _Passenger;
        CPD_PassengerTripManager _PassengerTripManager;
        Trip _trip;
        public RemoveTripFromPassengerMenuItem(Passenger Passenger,CPD_PassengerTripManager PassengerTripManager ,Trip trip)
        {
            _Passenger = Passenger;
            _PassengerTripManager = PassengerTripManager;
            _trip = trip;
        }
        public override void Select()
        {
            _PassengerTripManager.RemoveTrip(_trip);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{_trip.ToString()} was removed from {_Passenger} at {_PassengerTripManager._PortDockManager.Port.ToString()}");
            Console.ForegroundColor= ConsoleColor.White;
        }
        public override string MenuText()
        {
            return $"Trip: {_trip.ToString()}";
        }
    }
    class RemoveTripFromPassengerNoTripsLeftMenuItem : MenuItem
    {
        Port _port;
        public RemoveTripFromPassengerNoTripsLeftMenuItem(Port port)
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

    class RemovePassengerFromCruiseMenuItem : MenuItem
    {
        Cruise _cruise;
        Passenger _Passenger;
        public RemovePassengerFromCruiseMenuItem(Cruise cruise, Passenger Passenger)
        {
            _cruise = cruise;
            _Passenger = Passenger;
        }
        public override void Select()
        {
            _cruise.debug = !_cruise.debug;
            _Passenger.UnAssignCruiseFromPassenger(_cruise);
            _cruise.debug = !_cruise.debug;
        }
        public override string MenuText()
        {
            return "Remove Passenger From Cruise";
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
            if (_cruise.CruisePorts.Count > 0)
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
            _cruise.debug = !_cruise.debug;
            _cruise.AddPort(_port);
            _cruise.debug = !_cruise.debug;
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
            _cruise.debug = !_cruise.debug;
            _cruise.RemovePort(_port);
            _cruise.debug = !_cruise.debug;
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
            _system.debug = !_system.debug;
            _system.RemoveCruise(_cruise);
            _system.debug = !_system.debug;

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
            _system.debug = !_system.debug;
            _system.AddPort(newPort);
            _system.debug = !_system.debug;
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
            _port.debug = !_port.debug;
            _port.AddTrip(_trip);
            _port.debug = !_port.debug;
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
            _port.debug = !_port.debug;
            _port.RemoveTrip(_trip);
            _port.debug = !_port.debug;
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
            _system.debug = !_system.debug;
            _system.RemovePort(_port);
            _system.debug = !_system.debug;
        }
        public override string MenuText()
        {
            return "Remove Port From System";
        }
    }
    #endregion

    #region Passengers
    class ManagePassengersMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ManagePassengersMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPassengerMenuItem(_system));
            if (_system.Passengers.Count > 0)
            {
                _menuItems.Add(new ViewPassengersMenu(_system));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Manage Passengers";
        }
    }

    class AddPassengerMenuItem : MenuItem
    {
        AdminSystem _system;
        public AddPassengerMenuItem(AdminSystem system)
        {
            _system = system;
        }
        public override string MenuText()
        {
            return "Add Passenger";
        }
        public override void Select()
        {
            string newPassengerFName;
            string newPassengerSName;
            string newPassengerPassportNumber = "";

            Console.WriteLine("What is the Passenger's first name?");
            newPassengerFName = Console.ReadLine().ToLower();
            Console.WriteLine("What is the Passenger's surname?");
            newPassengerSName = Console.ReadLine().ToLower();
            while (true)
            {
                Console.WriteLine("What is their Passport ID (9 digits)");
                try
                {
                    newPassengerPassportNumber = Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("Please enter a 9 digit number.");
                }
                if(newPassengerPassportNumber.ToString().Length == 9)
                {
                    break;
                }
            }
            Passenger newPassenger = new Passenger(newPassengerFName, newPassengerSName, newPassengerPassportNumber);
            _system.debug = !_system.debug;
            _system.AddPassenger(newPassenger);
            _system.debug = !_system.debug;
        }
    }

    class ViewPassengersMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ViewPassengersMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Passenger Passenger in _system.Passengers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new ViewPassengerMenu(_system,Passenger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "View Passenger(s)";
        }
    }
    class ViewPassengerMenu : ConsoleMenu
    {
        AdminSystem _system;
        Passenger _Passenger;
        public ViewPassengerMenu(AdminSystem system,Passenger Passenger)
        {
            _system = system;
            _Passenger = Passenger;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            if ( _Passenger.IsCruiseAssignedToPassenger())
            {
                _menuItems.Add(new ViewPassengerCruisesMenuItem(_Passenger));
                _menuItems.Add(new UnAssignCruiseFromPassengerMenu(_Passenger));
            }
            else
            {
                _menuItems.Add(new AssignCruiseToPassengerMenu(_system.Cruises, _Passenger));
            }
            _menuItems.Add(new ViewPassengerPassengerTotalCostMenu(_system, _Passenger));
            _menuItems.Add(new RemovePassenger_SystemMenuItem(_system, _Passenger));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _Passenger.ToString();
        }
    }

    class AssignCruiseToPassengerMenu : ConsoleMenu
    {
        List<Cruise> _Cruises;
        Passenger _Passenger;
        public AssignCruiseToPassengerMenu(List<Cruise> cruises, Passenger Passenger)
        {
            _Cruises = cruises;
            _Passenger = Passenger;

        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Cruise cruise in _Cruises.OrderBy(o => o.CruiseID))
            {
                _menuItems.Add(new AssignCruiseToPassengerMenuItem(cruise,_Passenger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Assign Cruise to Passenger";
        }
    }
    class AssignCruiseToPassengerMenuItem : MenuItem
    {
        Cruise _Cruise;
        Passenger _Passenger;
        public AssignCruiseToPassengerMenuItem(Cruise cruise, Passenger Passenger)
        {
            _Cruise = cruise;
            _Passenger = Passenger;
        }

        public override void Select()
        {
            _Passenger.debug = !_Passenger.debug;
            _Passenger.AssignCruiseToPassenger(_Cruise);
            _Passenger.debug = !_Passenger.debug;
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }

    class ViewPassengerPassengerTotalCostMenu : ConsoleMenu
    {
        AdminSystem _system;
        Passenger _Passenger;
        public ViewPassengerPassengerTotalCostMenu(AdminSystem system,Passenger Passenger)
        {
            _system = system;
            _Passenger = Passenger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Cruise cruise in _Passenger.P_Cruises)
            {
                _menuItems.Add(new ViewPassengerPassengerTotalCostMenuItem(_system, _Passenger, cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Total Cost of Cruise";
        }
    }

    class ViewPassengerPassengerTotalCostMenuItem : MenuItem
    {
        AdminSystem _system;
        Passenger _Passenger;
        Cruise _Cruise;
        public ViewPassengerPassengerTotalCostMenuItem(AdminSystem system,Passenger Passenger, Cruise cruise)
        {
            _system = system;
            _Passenger = Passenger;
            _Cruise = cruise;
        }

        public override void Select()
        {
            
            CPD_PassengerTripManager cpdPT = _system.CPD_PassTripManagers.FirstOrDefault(o => o._Passenger == _Passenger && o._PortDockManager.Cruise == _Cruise);
            if (cpdPT != null)
            {
                cpdPT.CalculatePassengerTotalCost();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"£{_Passenger.PassengerTotalCost}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"£{_Cruise.CruiseCost}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }
    class ViewPassengerCruisesMenuItem : MenuItem
    {
        Passenger _Passenger;
        public ViewPassengerCruisesMenuItem(Passenger Passenger)
        {
            _Passenger = Passenger;
        }
        public override void Select()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{_Passenger.ToString()} Assigned to Cruises:\n");
            foreach(Cruise cruise in _Passenger.P_Cruises)
            {
                Console.WriteLine( cruise.ToString() );
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        public override string MenuText()
        {
            return "View Cruise Assigned to Passenger";
        }
    }

    class UnAssignCruiseFromPassengerMenu : ConsoleMenu
    {
        Passenger _Passenger;
        public UnAssignCruiseFromPassengerMenu(Passenger Passenger)
        {
            _Passenger = Passenger;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Cruise cruise in _Passenger.P_Cruises)
            {
                _menuItems.Add(new UnAssignCruiseFromPassengerMenuItem(_Passenger,cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "UnAssign Cruise from Passenger";
        }
    }

    class UnAssignCruiseFromPassengerMenuItem : MenuItem
    {
        Passenger _Passenger;
        Cruise _Cruise;
        public UnAssignCruiseFromPassengerMenuItem(Passenger Passenger, Cruise cruise)
        {
            _Passenger = Passenger;
            _Cruise = cruise;
        }
        public override void Select()
        {
            _Passenger.debug = !_Passenger.debug;
            _Passenger.UnAssignCruiseFromPassenger(_Cruise);
            _Passenger.debug = !_Passenger.debug;
        }
        public override string MenuText()
        {
            return _Cruise.ToString();
        }
    }

    class RemovePassenger_SystemMenuItem : MenuItem
    {
        AdminSystem _system;
        Passenger _Passenger;
        public RemovePassenger_SystemMenuItem(AdminSystem system, Passenger Passenger)
        {
            _system = system;
            _Passenger = Passenger;
        }
        public override void Select()
        {
            _system.debug = !_system.debug;
            _system.RemovePassenger(_Passenger);
            _system.debug = !_system.debug;
        }
        public override string MenuText()
        {
            return "Remove Passenger from System";
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
            _system.debug = !_system.debug;
            _system.AddTrip(newTrip);
            _system.debug = !_system.debug;
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
            _system.debug = !_system.debug;
            _system.RemoveTrip(_trip);
            _system.debug = !_system.debug;
        }
        public override string MenuText()
        {
            return "Remove Trip from System";
        }
    }
    #endregion
}
