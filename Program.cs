using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Assignment2
{
    class Git
    {
        ///<summary>
        ///This method runs when the program starts. It begins the main loop.
        ///</summary>
        static void Main(string[] args)
        {
            while(Loop()) {} // The core loop.
        }

        ///<summary>
        ///The main loop. Handles user input.
        ///</summary>
        ///<returns>
        ///Returns true if the program is still running. Otherwise, returns false.
        ///</returns>
        static bool Loop()
        {
            Console.Clear();
            Console.WriteLine("*OOP Assignment 2*\n"); // The program title is printed to the console.
            Console.Write(">>");
            string[] input = Console.ReadLine().Split(' '); // The user is asked for an input.

            switch (input[0])
            {
                case "diff": // If the user wants to use the 'diff' command,
                    if (input.Count() == 3) Diff(input[1], input[2]); // If the user provided the correct amount of parameters, the diff method is called.
                    else Output.InvalidArguments("diff", input.Count()-1, 2); // If the user did not provide the correct amount of parameters, an error is displayed.
                    break;
                case "help": // If the user wants to use the 'help' command,
                    if (input.Count() == 1) Console.WriteLine("Commands: \n'help': Displays available commands.\n'diff <file1> <file2>': Tests to see if there are any differences between two files.\n'exit': Closes the program."); // They are shown a list of commands.
                    else Output.InvalidArguments("help", input.Count()-1, 0); // But, if the user did not provide the correct amount of parameters, an error is displayed instead.
                    break;
                case "exit": // If the user wants to use the 'exit' command,
                    if (input.Count() == 1) return false; // The loop is stopped, closing the program.
                    else Output.InvalidArguments("exit", input.Count()-1, 0); // But, if the user did not provide the correct amount of parameters, an error is displayed instead.
                    break;
                default: // If the user's input is not recognised, 
                    Output.InvalidCommand(input[0]); // An error message is diplayed.
                    break;
            }
            Output.LogAndWrite("", "Press any key to continue... ", false, "write");
            Console.ReadLine(); // The program waits until the user is ready to repeat.
            return true; // And the loop repeats.
        }

        ///<summary>
        ///This method compares two strings and displays the differences to the console. 
        ///</summary>
        ///<param name="filename1">
        ///The first file to be compared.
        ///</param>
        ///<param name="filename2">
        ///The second file to be compared.
        ///</param>
        static void Diff(string filename1, string filename2)
        {
            TextFile file1 = new TextFile(filename1); // The files are loaded.
            TextFile file2 = new TextFile(filename2);
            Console.ForegroundColor = ConsoleColor.White; // The text colour is set to white.

            string log_filename = $"{filename1}-{filename2}";

            int lines;
            if (file1.Count() >= file2.Count()) lines = file1.Count(); // The number of lines in the longest text file.
            else lines = file2.Count();

            for (int i = 0; i < lines; i++) // This iterates through each line of the files.
            {
                bool condition = false;
                try { if (file1[i] != file2[i]) condition = true; } // If the lines are different (and the end of the file has not been met),
                catch (ArgumentOutOfRangeException) {}
                if (condition)
                {   
                    List<string> words1 = file1[i].Split(' ').ToList(); // Each line is split into words.
                    List<string> words2 = file2[i].Split(' ').ToList();

                    Output.LogAndWrite(log_filename, $"\n{i+1}", true);
                    Output.LogAndWrite(log_filename, "| ");

                    for(int j = 0; j < words1.Count(); j++) // For each word in the line,
                    {
                        try
                        {
                            if (words1[j] == words2[j]) // If the words are the same in each file,
                            {
                                Output.LogAndWrite(log_filename, words1[j]+" "); // It is written & logged in white.
                            }
                            else
                            {
                                Output.LogAndWrite(log_filename, words1[j], true); // Otherwise, it's done in green.
                                Output.LogAndWrite("", " ", false, "write"); // This is written for formatting.
                            }
                        }
                        catch (ArgumentOutOfRangeException) {}
                    }
                }
                else // If the lines are the same,
                {
                    try
                    {
                        Output.LogAndWrite(log_filename, $"\n{i+1}"); // It is written to the console in white (and logged)
                        Output.LogAndWrite(log_filename, "  ", false, "log");
                        Output.LogAndWrite(log_filename, $"| {file1[i]}");
                    } catch (ArgumentOutOfRangeException) {} 
                }
            }
            Output.LogAndWrite(log_filename, "\n"); // Then a new line character is written.
        }
    }

    class TextFile : List<string>
    {
        ///<summary>
        ///This loads a text file into the base.
        ///</summary>
        ///<param name="filename">
        ///The name of the text file to be loaded.
        ///</param>
        public TextFile(string filename)
        {
            if (System.IO.File.Exists($"{filename}")) // If the file the user selected exists,
            {
                base.AddRange(System.IO.File.ReadAllLines($"{filename}").ToList()); // Read the files contents and add to this file.
            }
            else // Otherwise,
            {
                Output.FileNotFound(filename); // Display an error.
            }
        }
    }

    class Output
    {
        ///<summary>
        ///Displays an appropriate error to the console.
        ///</summary>
        ///<param name="filename">
        ///The name of the file which could not be found.
        ///</param>
        public static void FileNotFound(string filename)
        {
            Console.WriteLine($"Error: {filename} not found."); // Error is written.
        }

        ///<summary>
        ///Displays an appropriate error to the console.
        ///</summary>
        ///<param name="command">
        ///The input which couldn't be understood.
        ///</param>
        public static void InvalidCommand(string command)
        {
            Console.WriteLine($"Error: Command Not Recognised: {command}"); // Error is written.
        }

        ///<summary>
        ///Displays an appropriate error to the console.
        ///</summary>
        ///<param name="command">
        ///The command the user entered.
        ///</param>
        ///<param name="arguments_given">
        ///The number of arguments the user gave.
        ///</param>
        ///<param name="arguments_expected">
        ///The number of arguments this command expected.
        ///</param>
        public static void InvalidArguments(string command, int arguments_given, int arguments_expected)
        {
            Console.WriteLine($"Error: '{command}' expects {arguments_expected} arguments, but {arguments_given} are given."); // Error is written.
        }

        ///<summary>
        ///Writes text to the console.
        ///</summary>
        ///<param name="str">
        ///The text to be written to the console.
        ///</param>
        ///<param name="modified">
        ///Whether or not the word has been modified
        ///</param>
        private static void Write(string str, bool modified=false)
        {
            if (!modified) Console.ForegroundColor = ConsoleColor.White; 
            else Console.ForegroundColor = ConsoleColor.Green; // The colour is set to green if the text has been modified.
            Console.Write(str); // The text is written.
            Console.ForegroundColor = ConsoleColor.White; // The colour is reset.
        }

        ///<summary>
        ///Writes text to a log file.
        ///</summary>
        ///<param name="filename">
        ///The name of the file to be logged to.
        ///</param>
        ///<param name="text">
        ///The text to be written to the log file.
        ///</param>
        ///<param name="modified">
        ///Whether or not the word has been modified
        ///</param>
        private static void Log(string filename, string text, bool modified=false)
        {
            if (!System.IO.File.Exists($"{filename}")) System.IO.File.AppendAllText($"{filename}", "//If a word has been modified, it is followed by an asterix.\n"); // If the file has just been created, add a comment to the top.
            if (!modified) System.IO.File.AppendAllText($"{filename}", text); // If the text being added has not been modified, it is logged without an asterix, 
            else System.IO.File.AppendAllText($"{filename}", $"{text}* "); // Otherwise, it is logged with an asterix.
        }

        ///<summary>
        ///Writes text to a log file and the console.
        ///</summary>
        ///<param name="filename">
        ///The name of the file to be logged to.
        ///</param>
        ///<param name="text">
        ///The text to be written to the log file and the console.
        ///</param>
        ///<param name="modified">
        ///Whether or not the word has been modified
        ///</param>
        public static void LogAndWrite(string filename, string text, bool modified=false, string mode="both")
        {
            if (mode == "both" || mode == "write") Write(text, modified); // The text is written to the console.
            if (mode == "both" || mode == "log") Log(filename, text, modified); // The text is logged to the file.
        }
    }
}