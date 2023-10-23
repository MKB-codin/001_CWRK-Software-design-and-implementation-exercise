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
            _menuItems.Add(new AddCruiseMenu(_system));
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
    class AddCruiseMenu : ConsoleMenu
    {
        AdminSystem _system;
        public AddCruiseMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            throw new NotImplementedException();
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
                Console.WriteLine("BRUH");
            }
        }
        public override string MenuText()
        {
            return "View Cruises";
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
            _menuItems.Add(new AddPortMenu(_system));
            if (_system.Passangers.Count > 0)
            {
                _menuItems.Add(new ViewPortMenu(_system));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "Manage Ports";
        }
    }
    class AddPortMenu : ConsoleMenu
    {
        AdminSystem _system;
        public AddPortMenu(AdminSystem system)
        {
            _system = system;
        }

        public override void CreateMenu()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "Add Port";
        }
    }
    class ViewPortMenu : ConsoleMenu
    {
        AdminSystem _system;
        public ViewPortMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "View Port(s)";
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
                _menuItems.Add(new ViewPassangerMenuItem(passanger));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "View Passanger(s)";
        }
    }
    class ViewPassangerMenuItem : MenuItem
    {
        Passanger _passanger;
        public ViewPassangerMenuItem(Passanger passanger)
        {
            _passanger = passanger;
        }
        public override void Select()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return _passanger.ToString();
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
            string newTripName = "Sample Name";
            Console.WriteLine("Enter Trip Name");
            newTripName = Console.ReadLine();
            Trip newTrip = new Trip(newTripName);
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
                _menuItems.Add(new ViewTripMenuItem(trip));
            }
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "View Trip(s)";
        }
    }
    class ViewTripMenuItem : MenuItem
    {
        Trip _trip;
        public ViewTripMenuItem(Trip trip)
        {
            _trip = trip;
        }
        public override void Select()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return _trip.ToString();
        }
    }
    #endregion
}
