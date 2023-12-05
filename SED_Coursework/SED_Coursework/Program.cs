using SED_Coursework;
using System.Runtime.InteropServices;


Console.WriteLine("Welcome To The Crusie Manager System");
AdminSystem system = new AdminSystem();

MainFileReader mainFileReader = new MainFileReader(system);
mainFileReader.Begin();
while (true)
{
    Console.WriteLine("ENTER PASSWORD: ");
    string password = Console.ReadLine();
    if (system.CheckPassword(password))
    {
        break;
    }
    else { Console.WriteLine("Incorrect"); }
}
new SystemManagerMenu(system).Select();
