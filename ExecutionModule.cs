using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace FilumExecutor
{
    public class ExecutionModule
    {   
        public static void CommandExecutorWithNoRestart()
        {
            try
            {
                string command;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                Console.WriteLine("==============================================");
                var arguments = new Collection();
                while (true)
                {
                    Console.Write("Enter the arguments for the command: ");
                    command = Console.ReadLine();
                    Console.WriteLine("==============================================");
                    if (command == "exit")
                    {
                        break;
                    }
                    else if (command.Contains("cd") == true)
                    {
                        var regex = new Regex(@"[a-zA-z]:\\");
                        if (regex.IsMatch(command.Substring(3)) == true)
                        {
                            startInfo.WorkingDirectory = command.Substring(3);

                        }
                        else
                        {
                            startInfo.WorkingDirectory = startInfo.WorkingDirectory + command.Substring(3) + "\\";

                        }
                    }
                    if (command.StartsWith("cd") == false)
                    {
                        arguments.Add(command);
                    }
                    else
                    {
                        continue;
                    }
                }

                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                int resNum = 1;

                foreach (string arg in arguments)
                {
                    startInfo.Arguments = "/C " + arg;
                    process.Start();
                    Console.WriteLine("Response " + resNum);
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    Console.WriteLine(output);
                    Console.WriteLine(error);
                    resNum = resNum + 1;
                }
                Console.WriteLine("==============================================");
                Console.WriteLine("Completed the execution of the commands!");
                Console.WriteLine("Thank you for using FilumExecuter!");
                Console.WriteLine("==============================================");
            }
            catch(Exception e)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("An error has occured!");
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("==============================================");
            }
        }
        public static void CommandExecutorWithRestart()
        {
            try
            {
                string command;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                Console.WriteLine("==============================================");
                var arguments = new Collection();
                while (true)
                {
                    Console.Write("Enter the arguments for the command: ");
                    command = Console.ReadLine();
                    Console.WriteLine("==============================================");
                    if (command == "exit")
                    {
                        break;
                    }
                    else if (command.Contains("cd") == true)
                    {
                        var regex = new Regex(@"[a-zA-z]:\\");
                        if (regex.IsMatch(command.Substring(3)) == true)
                        {
                            startInfo.WorkingDirectory = command.Substring(3);

                        }
                        else
                        {
                            startInfo.WorkingDirectory = startInfo.WorkingDirectory + command.Substring(3) + "\\";

                        }
                    }
                    if (command.StartsWith("cd") == false)
                    {
                        arguments.Add(command);
                    }
                    else
                    {
                        continue;
                    }
                }

                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                int resNum = 1;
                foreach (string arg in arguments)
                {
                    startInfo.Arguments = "/C " + arg;
                    process.Start();
                    Console.WriteLine("Response " + resNum);
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    Console.WriteLine(output);
                    Console.WriteLine(error);
                    resNum = resNum + 1;
                    if (process.HasExited == true)
                    {
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                }
                Console.WriteLine("==============================================");
                Console.WriteLine("Completed the execution of the commands!");
                Console.WriteLine("Thank you for using FilumExecuter!");
                Console.WriteLine("==============================================");
            }
            catch(Exception e)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("An error has occured!");
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("==============================================");
            }
        }
        
        public static void Main(string[] args)
        {
        CommandExecutor:    
            Console.WriteLine("Welcome to FilumExecuter!");
            Console.WriteLine("==============================================");
            Console.Write("Do you want to use commands that should restart on completion/termination(y/n): ");
            string restart = Console.ReadLine();
            
            if (restart == "y")
            {
                CommandExecutorWithRestart();
            }
            else if (restart == "n")
            {
                CommandExecutorWithNoRestart();
            }
            else
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("==============================================");
                goto CommandExecutor;
            }
            goto CommandExecutor;
        }
    }
}