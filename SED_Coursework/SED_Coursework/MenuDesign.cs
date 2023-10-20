using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED_Coursework
{
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
            _menuItems.Add(new CruiseManagerMenu(_system));
            _menuItems.Add(new AvailablePortsMenu(_system));
            _menuItems.Add(new AvailableTripsMenu(_system));
            _menuItems.Add(new ExitMenuItem(this));
        }
        public override string MenuText()
        {
            return "System Manager Menu";
        }

    }

    class CruiseManagerMenu : ConsoleMenu
    {
        AdminSystem _system;
        public CruiseManagerMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Cruise cruise in  _system.Cruises.OrderBy(o=>o.CruiseID).ToList())
            {
                _menuItems.Add(new ViewCruiseMenu(cruise));
            }
        }
        public override string MenuText()
        {
            return "Cruise Manager";
        }
        
    }

    class ViewCruiseMenu : ConsoleMenu
    {
        Cruise _cruise;

        public ViewCruiseMenu(Cruise cruise)
        {
            _cruise = cruise;
        }

        public override void CreateMenu()
        {
            throw new NotImplementedException(); 
        }
        public override string MenuText()
        {
            return _cruise.ToString();
        }
    }

    class AvailablePortsMenu : ConsoleMenu
    {
        AdminSystem _system;
        public AvailablePortsMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "Available Ports";
        }
    }

    class AvailableTripsMenu : ConsoleMenu
    {
        AdminSystem _system;
        public AvailableTripsMenu(AdminSystem system)
        {
            _system = system;
        }
        public override void CreateMenu()
        {
            throw new NotImplementedException();
        }
        public override string MenuText()
        {
            return "Available Trips";
        }
    }



}
