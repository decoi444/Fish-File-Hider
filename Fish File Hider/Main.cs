using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace FileHider
{
    public class Program
    {
        static void Main(string[] args) 
        { 
            if (args != null && args.Length > 0) { MainMenu(args[0]); }
            else { UI.WriteLine("Drag The File into this exe"); Console.ReadKey(); }
        }
        public static void MainMenu(string FilePath = null)
        {
            Console.Clear();
            Console.Title = "Fish File Hider";
            Console.WriteLine();
            UI.WriteLineAlt("Fish File Hider");
            UI.WriteSpacing(true);
            UI.WriteBarrierLine("1", "Hide File");
            UI.WriteBarrierLine("2", "UnHide Folder");
            UI.WriteSpacing(true);
            UI.WriteSpacing(false);
            Console.Write("   -> ");
            ConsoleKey choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.D1) { HideFile(FilePath); }
            if (choice == ConsoleKey.D2) { UnHideFile(FilePath); }
            MainMenu();
        }
        public static void HideFile(string FilePath)
        {
            DirectoryInfo file = new DirectoryInfo(FilePath);
            file.Attributes |= FileAttributes.Hidden;
            file.Attributes |= FileAttributes.System;
        }
        public static void UnHideFile(string FilePath)
        {
            DirectoryInfo file = new DirectoryInfo(FilePath);
            file.Attributes |= FileAttributes.Normal;
        }
    }

    public class UI
    {
        public static ConsoleColor color = ConsoleColor.DarkBlue;
        public static void WriteLine(string line)
        {
            Console.Write("  ");
            Thread.Sleep(20);
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
            Thread.Sleep(20);
            Console.Write("  ");
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(20);
            }
            Thread.Sleep(20);
            Console.WriteLine();
        }
        public static void WriteLineAlt(string line)
        {
            Console.Write("  ");
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
            Console.Write("  ");
            Console.Write(line);
            Thread.Sleep(20);
            Console.WriteLine();
        }
        public static void WriteBarrierLine(string num, string line)
        {
            Console.Write("  ");
            Thread.Sleep(20);
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
            Console.Write("  [");
            Thread.Sleep(20);
            Console.ForegroundColor = color;
            Console.Write(num);
            Console.ResetColor();
            Thread.Sleep(20);
            Console.Write("] ");
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(20);
            }
            Thread.Sleep(20);
            Console.WriteLine();
        }
        public static void WriteSpacing(bool writeline)
        {
            if (!writeline)
            {
                Console.Write("  ");
                Thread.Sleep(20);
                Console.BackgroundColor = color;
                Console.ForegroundColor = color;
                Console.Write(" ");
                Console.ResetColor();
            }
            if (writeline)
            {
                Console.Write("  ");
                Thread.Sleep(20);
                Console.BackgroundColor = color;
                Console.ForegroundColor = color;
                Console.WriteLine(" ");
                Console.ResetColor();
            }
        }
    }
    public class Utils
    {
        public static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        public static void RunAsAdministrator()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
            startInfo.Verb = "runas";

            try { Process.Start(startInfo); }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

            Environment.Exit(0);
        }
        public static string RandomMac()
        {
            string chars = "ABCDEF0123456789";
            string windows = "26AE";
            string result = "";
            Random random = new Random();

            result += chars[random.Next(chars.Length)];
            result += windows[random.Next(windows.Length)];

            for (int i = 0; i < 5; i++)
            {
                result += "-";
                result += chars[random.Next(chars.Length)];
                result += chars[random.Next(chars.Length)];

            }

            return result;
        }
        public static string RandomId(int length)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string text2 = "";
            Random random = new Random();
            for (int i = 0; i < length; i++) { text2 += text[random.Next(text.Length)].ToString(); }
            return text2;
        }
    }
}
