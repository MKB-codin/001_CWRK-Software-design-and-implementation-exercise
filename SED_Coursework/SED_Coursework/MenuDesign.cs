using System;
using System.Collections.Generic;
using System.Linq;
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
    class AddPassangerMenu : ConsoleMenu
    {
        AdminSystem _system;
        public AddPassangerMenu(AdminSystem system)
        {
            _system = system;
        }

        public override void CreateMenu()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "Add Passanger";
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
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "View Passanger(s)";
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
            _menuItems.Add(new AddTripMenu(_system));
            if (_system.Passangers.Count > 0)
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
    class AddTripMenu : ConsoleMenu
    {
        AdminSystem _system;
        public AddTripMenu(AdminSystem system)
        {
            _system = system;
        }

        public override void CreateMenu()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "View Trip(s)";
        }
    }
    #endregion
}
