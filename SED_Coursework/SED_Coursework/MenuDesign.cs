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
            string newCruiseName = Console.ReadLine();
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
            if(_cruise.CruisePassangers != null)
            {
                _menuItems.Add(new ViewPassangersInCruiseMenu(_cruise,_system.Passangers));
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
            _cruise.AddPassanger(_passanger);
        }
        public override string MenuText()
        {
            return _passanger.ToString();
        }
    }

    class ViewPassangersInCruiseMenu : ConsoleMenu
    {
        Cruise _cruise;
        List<Passanger> _passangers;
        public ViewPassangersInCruiseMenu(Cruise cruise, List<Passanger> passangers)
        {
            _cruise = cruise;
            _passangers = passangers;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach( Passanger passanger in _passangers.OrderBy(o => o.Passport))
            {
                _menuItems.Add(new ViewPassangerInCruiseMenu(_cruise,passanger));
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
        Cruise _cruise;
        Passanger _passanger;
        public ViewPassangerInCruiseMenu(Cruise crusie, Passanger passanger)
        {
            _cruise = crusie;
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddTripToPassangerMenu(_cruise, _passanger));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return _passanger.ToString();
        }
    }
    class AddTripToPassangerMenu : ConsoleMenu
    {
        Cruise _cruise;
        Passanger _passanger;
        public AddTripToPassangerMenu(Cruise cruise, Passanger passanger)
        {
            _cruise = cruise;
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Trip trip in _passanger.AssignedTrips.OrderBy(o => o.TripID))
            {
                _menuItems.Add(new AddTripToPassangerMenuItem(_passanger, trip));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Assign a trip to Passanger";
        }
    }
    class AddTripToPassangerMenuItem : MenuItem
    {
        Passanger _passanger;
        Trip _trip;

        public AddTripToPassangerMenuItem(Passanger passanger, Trip trip)
        {
            _passanger = passanger;
            _trip = trip;
        }
        public override void Select()
        {
            ; _passanger.AddToAssignedTrips(_trip);
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }

    class RemoveTripFromPassangerMenu : ConsoleMenu
    {
        Passanger _passanger;
        public RemoveTripFromPassangerMenu(Passanger passanger)
        {
            _passanger = passanger;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach(Trip trip in _passanger.AssignedTrips.OrderBy(o => o.TripID))
            {
                _menuItems.Add(new RemoveTripFromPassangerMenuItem(_passanger, trip));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Remove a trip from this passanger";
        }
    }
    class RemoveTripFromPassangerMenuItem : MenuItem
    {
        Passanger _passanger;
        Trip _trip;

        public RemoveTripFromPassangerMenuItem(Passanger passanger, Trip trip)
        {
            _passanger = passanger;
            _trip = trip;
        }
        public override void Select()
        {
            _passanger.RemoveAssignedTrip(_trip);
        }
        public override string MenuText()
        {
            return _trip.ToString();
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
            return _passanger.ToString();
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
            _menuItems.Add(new AddPortToCruiseMenu(_system.AvailablePorts, _cruise));
            if (_cruise.CruisePassangers != null)
            {
                _menuItems.Add(new RemovePortFromCruiseMenu(_cruise));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Passangers";
        }
    }
    class AddPortToCruiseMenu : ConsoleMenu
    {
        List<Port> _ports;
        Cruise _cruise;
        public AddPortToCruiseMenu(List<Port> ports, Cruise cruise)
        {
            _ports = ports;
            _cruise = cruise;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Port port in _ports)
            {
                _menuItems.Add(new AddPortToCruiseMenuItem(_cruise, port));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Port";
        }
    } //NOT DONE NEEDS IMPLEMENTING
    class AddPortToCruiseMenuItem : MenuItem
    {
        Cruise _cruise;
        Port _port;
        public AddPortToCruiseMenuItem(Cruise cruise, Port port)
        {
            _cruise = cruise;
            _port = port;
        }
        public override void Select()
        {
            _cruise.AddPort(_port);
        }
        public override string MenuText()
        {
            return _port.ToString();
        }
    } //NOT DONE NEEDS IMPLEMENTING
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
            return "Port";
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
            newPortName = Console.ReadLine();
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
            int newPassangerPassportNumber = 0;

            Console.WriteLine("What is the passanger's first name?");
            newPassangerFName = Console.ReadLine();
            Console.WriteLine("What is the passanger's surname?");
            newPassangerSName = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("What is their Passport ID (9 digits)");
                newPassangerPassportNumber = int.Parse(Console.ReadLine());
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
                _menuItems.Add(new UnAssignCruiseFromPassangerMenuItem(_passanger));
            }
            else
            {
                _menuItems.Add(new AssignCruiseToPassangerMenu(_system.Cruises, _passanger));
            }
            _menuItems.Add(new ViewPassangerPassangerTotalCost(_passanger));
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

    class ViewPassangerPassangerTotalCost : MenuItem
    {
        Passanger _Passanger;
        public ViewPassangerPassangerTotalCost(Passanger passanger)
        {
            _Passanger = passanger;
        }

        public override void Select()
        {
            _Passanger.CalculatePassangerTotalCost();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"£{_Passanger.PassangerTotalCost}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return "View Cost of Trip";
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
            Console.WriteLine($"{_passanger.ToString()} Assigned to Cruise: {_passanger.AssignedCruise.ToString()}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string MenuText()
        {
            return "View Cruise Assigned to Passanger";
        }
    }

    class UnAssignCruiseFromPassangerMenuItem : MenuItem
    {
        Passanger _Passanger;
        public UnAssignCruiseFromPassangerMenuItem(Passanger passanger)
        {
            _Passanger = passanger;
        }
        public override void Select()
        {
            _Passanger.UnAssignCruiseFromPassanger();
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
            newTripName = Console.ReadLine();
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
