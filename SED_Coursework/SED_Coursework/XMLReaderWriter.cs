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
            RPassengers ReadPassengers = new RPassengers(_system);
            RPorts Readports = new RPorts(_system);
            RTrips Readtrips = new RTrips(_system);
            RPortTrips ReadPortTrips = new RPortTrips(_system);
            RCPassengers ReadCPasangers = new RCPassengers(_system);
            RCPD ReadCPD = new RCPD(_system);
            RCPD_PT ReadCPD_PT = new RCPD_PT(_system);
            Readcruises.ReadCruises();
            ReadPassengers.ReadPassengers();
            Readports.ReadPorts();
            Readtrips.ReadTrips();
            ReadPortTrips.ReadPortTrips();
            ReadCPasangers.ReadCPassengers();
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
                try
                {
                    Cruise newCruise = new Cruise(int.Parse(node.ChildNodes[0].InnerText), node.ChildNodes[1].InnerText, decimal.Parse(node.ChildNodes[2].InnerText));
                    _system.AddCruise(newCruise);
                } catch {}
            }
        }
        
    }
    class RPassengers
    {
        AdminSystem _system;
        public RPassengers(AdminSystem system)
        {
            _system = system;
        }
        public void ReadPassengers()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Passengers.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                try
                {
                    Passenger newPassenger = new Passenger(node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[0].InnerText);
                    _system.AddPassenger(newPassenger);
                }catch {}
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
                try
                {
                    Port newport = new Port(int.Parse(node.ChildNodes[0].InnerText), node.ChildNodes[1].InnerText);
                    _system.AddPort(newport);
                }catch {}
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
                try
                {
                    Trip newtrip = new Trip(node.ChildNodes[1].InnerText, int.Parse(node.ChildNodes[0].InnerText), decimal.Parse(node.ChildNodes[2].InnerText));
                    _system.AddTrip(newtrip);
                }catch {}
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
                try
                {
                    Port port = _system.AvailablePorts.FirstOrDefault(o => o.PortID == int.Parse(node.ChildNodes[0].InnerText));
                    Trip trip = _system.AvailableTrips.FirstOrDefault(o => o.TripID == int.Parse(node.ChildNodes[1].InnerText));
                    port.AddTrip(trip);
                }catch {}
            }
        }
    }
    class RCPassengers
    {
        AdminSystem _system;
        public RCPassengers(AdminSystem system)
        {
            _system = system;
        }
        public void ReadCPassengers()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Cruise_Passengers.xml");
            foreach(XmlNode node in xmlDoc.DocumentElement)
            {
                try
                {
                    Cruise cruise = _system.Cruises.FirstOrDefault(o => o.CruiseID == int.Parse(node.ChildNodes[0].InnerText));
                    Passenger Passenger = _system.Passengers.FirstOrDefault(o => o.Passport == node.ChildNodes[1].InnerText);
                    Passenger.AssignCruiseToPassenger(cruise);
                } catch {}
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
                try
                {
                    Cruise cruise = _system.Cruises.FirstOrDefault(o => o.CruiseID == int.Parse(node.ChildNodes[0].InnerText));
                    Port port = _system.AvailablePorts.FirstOrDefault(o => o.PortID == int.Parse(node.ChildNodes[1].InnerText));
                    CruisePortDockManager newCPD = new CruisePortDockManager(cruise, port, int.Parse(node.ChildNodes[2].InnerText));
                    cruise.AddPort(port);
                    _system.AddC_P_DManager(newCPD);
                }catch {}
            }
        }
    }
    class RCPD_PT
    {
        AdminSystem _system;
        CPD_PassengerTripManager cPD_PassengerTripManager;
        public RCPD_PT(AdminSystem system)
        {
            _system = system;
        }
        public void ReadCPD_PT()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Cruise_Port_Passenger_Trip.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                try
                {
                    Cruise cruise = _system.Cruises.FirstOrDefault(o => o.CruiseID == int.Parse(node.ChildNodes[0].InnerText));
                    Port port = _system.AvailablePorts.FirstOrDefault(o => o.PortID == int.Parse(node.ChildNodes[1].InnerText));
                    Passenger Passenger = _system.Passengers.FirstOrDefault(o => o.Passport == node.ChildNodes[2].InnerText);
                    Trip trip = _system.AvailableTrips.FirstOrDefault(o => o.TripID == int.Parse(node.ChildNodes[3].InnerText));
                    CruisePortDockManager cruisePortDockManager = _system.CruisePortDockManagers.FirstOrDefault(o => o.Port == port && o.Cruise == cruise);
                    if (_system.CPD_PassTripManagers.FirstOrDefault(o => o._Passenger == Passenger && o._PortDockManager == cruisePortDockManager) != null)
                    {
                        cPD_PassengerTripManager = _system.CPD_PassTripManagers.FirstOrDefault(o => o._Passenger == Passenger && o._PortDockManager == cruisePortDockManager);
                        cPD_PassengerTripManager.BookTrip(trip);

                    }
                    else
                    {
                        CPD_PassengerTripManager newPassengerTripManager = new CPD_PassengerTripManager(Passenger, cruisePortDockManager);
                        _system.AddCPD_PassTripManager(newPassengerTripManager);
                        newPassengerTripManager.BookTrip(trip);
                    }
                }catch {}
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
            WPassengers WritePassengers = new WPassengers(_system);
            WCruises WriteCruises = new WCruises(_system);
            WPortTrips WritePortTrips = new WPortTrips(_system);
            WCPassengers WriteCruisePassenger = new WCPassengers(_system);
            WCPD WriteCPD = new WCPD(_system);
            WCPD_PT WriteCPD_PT = new WCPD_PT(_system);
            WriteTrips.Write();
            WritePorts.Write();
            WritePassengers.Write();
            WriteCruises.Write();
            WritePortTrips.Write();
            WriteCruisePassenger.Write();
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
                    try
                    {
                        writer.WriteStartElement("Trip");

                        writer.WriteElementString("id", trip.TripID.ToString());
                        writer.WriteElementString("name", trip.TripName);
                        writer.WriteElementString("cost", trip.TripCost.ToString());

                        writer.WriteEndElement();
                    }
                    catch { }
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
                    try
                    {
                        writer.WriteStartElement("Port");

                        writer.WriteElementString("id", port.PortID.ToString());
                        writer.WriteElementString("name", port.PortName);

                        writer.WriteEndElement();
                    }
                    catch { }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WPassengers
    {
        AdminSystem _system;
        public WPassengers(AdminSystem system)
        {
            _system = system;
        }

        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Passengers.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Passengers");

                foreach (Passenger Passenger in _system.Passengers)
                {
                    try
                    {
                        writer.WriteStartElement("Passenger");

                        writer.WriteElementString("passport", Passenger.Passport);
                        writer.WriteElementString("fname", Passenger.FName);
                        writer.WriteElementString("sname", Passenger.SName);

                        writer.WriteEndElement();
                    }catch { }
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
                    try
                    {
                        writer.WriteStartElement("Cruise");

                        writer.WriteElementString("id", cruise.CruiseID.ToString());
                        writer.WriteElementString("name", cruise.CruiseName);
                        writer.WriteElementString("cost", cruise.CruiseCost.ToString());

                        writer.WriteEndElement();
                    }
                    catch { }
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
                        try
                        {
                            writer.WriteStartElement("Port_Trip");

                            writer.WriteElementString("portid", port.PortID.ToString());
                            writer.WriteElementString("tripid", trip.TripID.ToString());

                            writer.WriteEndElement();
                        }
                        catch { }
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    class WCPassengers
    {
        AdminSystem _system;
        public WCPassengers(AdminSystem system)
        {
            _system = system;
        }
        public void Write()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("Cruise_Passengers.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("CruisePassengers");

                foreach (Cruise cruise in _system.Cruises)
                {
                    foreach (Passenger Passenger in cruise.CruisePassengers)
                    {
                        try
                        {
                            writer.WriteStartElement("CruisePassenger");

                            writer.WriteElementString("cruiseid", cruise.CruiseID.ToString());
                            writer.WriteElementString("Passengerpassport", Passenger.Passport);

                            writer.WriteEndElement();
                        }catch { }
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
                    try
                    {
                        writer.WriteStartElement("Cruise_Port");

                        writer.WriteElementString("cruiseid", dockManager.Cruise.CruiseID.ToString());
                        writer.WriteElementString("portid", dockManager.Port.PortID.ToString());
                        writer.WriteElementString("maxdays", dockManager.MaxDays.ToString());

                        writer.WriteEndElement();
                    }
                    catch { }
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
            using (XmlWriter writer = XmlWriter.Create("Cruise_Port_Passenger_Trip.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("CPPTs");

                foreach (CPD_PassengerTripManager _PassengerTripManager in _system.CPD_PassTripManagers)
                {
                    foreach (Trip trip in _PassengerTripManager.Trips)
                    {
                        try
                        {
                            writer.WriteStartElement("CPPT");

                            writer.WriteElementString("cruiseid", _PassengerTripManager._PortDockManager.Cruise.CruiseID.ToString());
                            writer.WriteElementString("portid", _PassengerTripManager._PortDockManager.Port.PortID.ToString());
                            writer.WriteElementString("Passengerpassport", _PassengerTripManager._Passenger.Passport);
                            writer.WriteElementString("tripid", trip.TripID.ToString());

                            writer.WriteEndElement();
                        }
                        catch { }
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
