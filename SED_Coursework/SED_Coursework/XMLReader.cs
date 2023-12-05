using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SED_Coursework
{
    class MainFileReader
    {
        AdminSystem _system;
        public MainFileReader(AdminSystem system)
        {
            _system = system;
        }
        public void Begin()
        {
            RCruises Readcruises = new RCruises(_system);
            RPassangers Readpassangers = new RPassangers(_system);
            RPorts Readports = new RPorts(_system);
            RTrips Readtrips = new RTrips(_system);
            RPortTrips ReadPortTrips = new RPortTrips(_system);
            RCPassangers ReadCPasangers = new RCPassangers(_system);
            RCPD ReadCPD = new RCPD(_system);
            RCPD_PT ReadCPD_PT = new RCPD_PT(_system);
            Readcruises.ReadCruises();
            Readpassangers.ReadPassangers();
            Readports.ReadPorts();
            Readtrips.ReadTrips();
            ReadPortTrips.ReadPortTrips();
            ReadCPasangers.ReadCPassangers();
            ReadCPD.ReadCPD();
            ReadCPD_PT.ReadCPD_PT();
        }
    }
    class RCruises
    {
        AdminSystem _system;

        public RCruises(AdminSystem system)
        {
            _system = system;
        }
        public void ReadCruises()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Cruises.xml");
            foreach(XmlNode node in xmlDoc.DocumentElement)
            {
                Cruise newCruise = new Cruise(int.Parse(node.ChildNodes[0].InnerText), node.ChildNodes[1].InnerText, decimal.Parse(node.ChildNodes[2].InnerText));
                _system.AddCruise(newCruise);
            }
        }

        public void Write()
        {

        }
        
    }
    class RPassangers
    {
        AdminSystem _system;
        public RPassangers(AdminSystem system)
        {
            _system = system;
        }
        public void ReadPassangers()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Passangers.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                Passanger newpassanger = new Passanger(node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[0].InnerText);
                _system.AddPassanger(newpassanger);
            }
        }

        public void Write() { }
    }
    class RPorts
    {
        AdminSystem _system;
        public RPorts(AdminSystem system)
        {
            _system = system;
        }
        public void ReadPorts()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Ports.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                Port newport = new Port(int.Parse(node.ChildNodes[0].InnerText),node.ChildNodes[1].InnerText);
                _system.AddPort(newport);
            }
        }
    }
    class RTrips
    {
        AdminSystem _system;
        public RTrips(AdminSystem system)
        {
            _system = system;
        }
        public void ReadTrips()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Trips.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                Trip newtrip = new Trip(node.ChildNodes[1].InnerText, int.Parse(node.ChildNodes[0].InnerText), decimal.Parse(node.ChildNodes[2].InnerText));
                _system.AddTrip(newtrip);
            }
        }
    }
    class RPortTrips
    {
        AdminSystem _system;
        public RPortTrips(AdminSystem system)
        {
            _system = system;
        }
        public void ReadPortTrips()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Ports_Trips.xml");
            foreach(XmlNode node in xmlDoc.DocumentElement)
            {
                Port port = _system.AvailablePorts.FirstOrDefault(o=>o.PortID == int.Parse(node.ChildNodes[0].InnerText));
                Trip trip = _system.AvailableTrips.FirstOrDefault(o=>o.TripID == int.Parse(node.ChildNodes[1].InnerText));
                port.AddTrip(trip);
            }
        }
    }
    class RCPassangers
    {
        AdminSystem _system;
        public RCPassangers(AdminSystem system)
        {
            _system = system;
        }
        public void ReadCPassangers()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Cruise_Passangers.xml");
            foreach(XmlNode node in xmlDoc.DocumentElement)
            {
                Cruise cruise = _system.Cruises.FirstOrDefault(o => o.CruiseID == int.Parse(node.ChildNodes[0].InnerText));
                Passanger passanger = _system.Passangers.FirstOrDefault(o => o.Passport == node.ChildNodes[1].InnerText);
                passanger.AssignCruiseToPassanger(cruise);
            }
        }
    }
    class RCPD
    {
        public AdminSystem _system;
        public RCPD(AdminSystem system)
        {
            _system = system;
        }
        public void ReadCPD()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Cruise_Ports.xml");
            foreach(XmlNode node in xmlDoc.DocumentElement)
            {
                Cruise cruise = _system.Cruises.FirstOrDefault(o => o.CruiseID == int.Parse(node.ChildNodes[0].InnerText));
                Port port = _system.AvailablePorts.FirstOrDefault(o => o.PortID == int.Parse(node.ChildNodes[1].InnerText));
                CruisePortDockManager newCPD = new CruisePortDockManager(cruise, port, int.Parse(node.ChildNodes[2].InnerText));
                cruise.AddPort(port);
                _system.AddC_P_DManager(newCPD);
            }
        }
    }
    class RCPD_PT
    {
        AdminSystem _system;
        CPD_PassangerTripManager cPD_PassangerTripManager;
        public RCPD_PT(AdminSystem system)
        {
            _system = system;
        }
        public void ReadCPD_PT()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Cruise_Port_Passanger_Trip.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                Cruise cruise = _system.Cruises.FirstOrDefault(o => o.CruiseID == int.Parse(node.ChildNodes[0].InnerText));
                Port port = _system.AvailablePorts.FirstOrDefault(o => o.PortID == int.Parse(node.ChildNodes[1].InnerText));
                Passanger passanger = _system.Passangers.FirstOrDefault(o => o.Passport == node.ChildNodes[2].InnerText);
                Trip trip = _system.AvailableTrips.FirstOrDefault(o => o.TripID == int.Parse(node.ChildNodes[3].InnerText));
                CruisePortDockManager cruisePortDockManager = _system.CruisePortDockManagers.FirstOrDefault(o => o.Port == port && o.Cruise == cruise);
                if (_system.CPD_PassTripManagers.FirstOrDefault(o => o._Passanger == passanger && o._PortDockManager == cruisePortDockManager) != null)
                {
                    cPD_PassangerTripManager = _system.CPD_PassTripManagers.FirstOrDefault(o => o._Passanger == passanger && o._PortDockManager == cruisePortDockManager);
                    cPD_PassangerTripManager.BookTrip(trip);
                    
                }
                else
                {
                    CPD_PassangerTripManager newpassangerTripManager = new CPD_PassangerTripManager(passanger, cruisePortDockManager);
                    _system.AddCPD_PassTripManager(newpassangerTripManager);
                    newpassangerTripManager.BookTrip(trip);
                }
            }
        }
    }
}
