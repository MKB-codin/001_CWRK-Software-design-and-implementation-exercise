using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

    class MainFileWriter
    {
        AdminSystem _system;
        public MainFileWriter(AdminSystem system)
        {
            _system = system;
        }
        public void Begin()
        {
            WTrips WriteTrips = new WTrips(_system);
            WPorts WritePorts = new WPorts(_system);
            WPassangers WritePassangers = new WPassangers(_system);
            WCruises WriteCruises = new WCruises(_system);
            WPortTrips WritePortTrips = new WPortTrips(_system);
            WCPassangers WriteCruisePassanger = new WCPassangers(_system);
            WCPD WriteCPD = new WCPD(_system);
            WCPD_PT WriteCPD_PT = new WCPD_PT(_system);
            WriteTrips.Write();
            WritePorts.Write();
            WritePassangers.Write();
            WriteCruises.Write();
            WritePortTrips.Write();
            WriteCruisePassanger.Write();
            WriteCPD.Write();
            WriteCPD_PT.Write();
        }
    }
    class WTrips
    {
        AdminSystem _system;
        public WTrips(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Trips.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Trips");

                foreach (Trip trip in _system.AvailableTrips)
                {
                    writer.WriteStartElement("Trip");

                    writer.WriteElementString("id", trip.TripID.ToString());
                    writer.WriteElementString("name", trip.TripName);
                    writer.WriteElementString("cost", trip.TripCost.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WPorts
    {
        AdminSystem _system;
        public WPorts(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Port.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Ports");

                foreach (Port port in _system.AvailablePorts)
                {
                    writer.WriteStartElement("Port");

                    writer.WriteElementString("id", port.PortID.ToString());
                    writer.WriteElementString("name", port.PortName);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WPassangers
    {
        AdminSystem _system;
        public WPassangers(AdminSystem system)
        {
            _system = system;
        }

        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Passangers.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Passangers");

                foreach (Passanger passanger in _system.Passangers)
                {
                    writer.WriteStartElement("Passanger");

                    writer.WriteElementString("passport", passanger.Passport);
                    writer.WriteElementString("fname", passanger.FName);
                    writer.WriteElementString("sname", passanger.SName);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WCruises
    {
        AdminSystem _system;
        public WCruises(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Cruises.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Cruises");

                foreach (Cruise cruise in _system.Cruises)
                {
                    writer.WriteStartElement("Cruise");

                    writer.WriteElementString("id", cruise.CruiseID.ToString());
                    writer.WriteElementString("name", cruise.CruiseName);
                    writer.WriteElementString("cost", cruise.CruiseCost.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WPortTrips
    {
        AdminSystem _system;
        public WPortTrips(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Ports_Trips.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Port_Trips");

                foreach (Port port in _system.AvailablePorts)
                {
                    foreach (Trip trip in port.PortTrips)
                    {
                        writer.WriteStartElement("Port_Trip");

                        writer.WriteElementString("portid", port.PortID.ToString());
                        writer.WriteElementString("tripid", trip.TripID.ToString());

                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WCPassangers
    {
        AdminSystem _system;
        public WCPassangers(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Cruise_Passangers.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("CruisePassangers");

                foreach (Cruise cruise in _system.Cruises)
                {
                    foreach (Passanger passanger in cruise.CruisePassangers)
                    {
                        writer.WriteStartElement("CruisePassanger");

                        writer.WriteElementString("cruiseid", cruise.CruiseID.ToString());
                        writer.WriteElementString("passangerpassport", passanger.Passport);

                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WCPD
    {
        AdminSystem _system;
        public WCPD(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Cruise_Ports.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Cruise_Ports");

                foreach (CruisePortDockManager dockManager in _system.CruisePortDockManagers)
                {
                    writer.WriteStartElement("Cruise_Port");

                    writer.WriteElementString("cruiseid", dockManager.Cruise.CruiseID.ToString());
                    writer.WriteElementString("portid", dockManager.Port.PortID.ToString());
                    writer.WriteElementString("maxdays", dockManager.MaxDays.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }

    class WCPD_PT
    {
        AdminSystem _system;
        public WCPD_PT(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Cruise_Port_Passanger_Trip.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("CPPTs");

                foreach (CPD_PassangerTripManager _passangerTripManager in _system.CPD_PassTripManagers)
                {
                    foreach (Trip trip in _passangerTripManager.Trips)
                    {
                        writer.WriteStartElement("CPPT");

                        writer.WriteElementString("cruiseid", _passangerTripManager._PortDockManager.Cruise.CruiseID.ToString());
                        writer.WriteElementString("portid", _passangerTripManager._PortDockManager.Port.PortID.ToString());
                        writer.WriteElementString("passangerpassport", _passangerTripManager._Passanger.Passport);
                        writer.WriteElementString("tripid", trip.TripID.ToString());

                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
