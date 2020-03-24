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
            Console.Clear();
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
            Console.WriteLine("*OOP Assignment 2*\n"); // The program title is printed to the console.
            Console.Write(">>");
            string[] input = Console.ReadLine().Split(' '); // The user is asked for an input.

            switch (input[0])
            {
                case "diff": // If the user wants to use the 'diff' command,
                    if (input.Count() == 3) Diff2(input[1], input[2]); // If the user provided the correct amount of parameters, the diff method is called.
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
            Console.Write("Press any key to continue... ");
            Console.ReadLine(); // The program waits until the user is ready to repeat.
            Console.Clear(); // Then the console is cleared,
            return true; // And the loop repeats.
        }

        ///<summary>
        ///This method compares two strings.
        ///</summary>
        ///<param name="string1">
        ///The first string to be compared.
        ///</param>
        ///<param name="string2">
        ///The second string to be compared.
        ///</param>
        ///<returns>
        ///Returns true if the strings are identical. Otherwise, returns false.
        ///</returns>
        static void Diff(string filename1, string filename2)
        {
            TextFile file1 = new TextFile(filename1);
            TextFile file2 = new TextFile(filename2);
            Console.ForegroundColor = ConsoleColor.White;

            int lines = Max(file1.Count(), file2.Count());
            int chars;
            for (int i = 0; i < lines; i++)
            {
                chars = Max(file1[i].Count(), file2[i].Count());
                //Console.WriteLine($"\nThe longest line is {chars} characters long.");
                if (file1[i] != file2[i])
                {   
                    Output.Write($"\n{i+1}", ConsoleColor.Green);
                    Output.Write("| ", ConsoleColor.White);
                    for (int j = 0; j < chars; j++)
                    {
                        try
                        {
                            if (file1[i][j] != file2[i][j]) Output.Write(file1[i][j].ToString(), ConsoleColor.Green);
                            else Output.Write(file1[i][j].ToString(), ConsoleColor.White);
                        }
                        catch (IndexOutOfRangeException) 
                        { 
                            //try { Output.Write(file2[i][j].ToString(), ConsoleColor.Red); }
                            //catch (IndexOutOfRangeException) {}
                        }
                    }
                }
                else 
                {
                    Output.Write($"\n{i+1}| ", ConsoleColor.White);
                    Output.Write(file1[i], ConsoleColor.White);
                }
            }
            Console.Write("\n");
        }

        static void Diff2(string filename1, string filename2)
        {
            TextFile file1 = new TextFile(filename1);
            TextFile file2 = new TextFile(filename2);
            Console.ForegroundColor = ConsoleColor.White;

            int lines = Max(file1.Count(), file2.Count());
            int words;
            for (int i = 0; i < lines; i++)
            {
                bool condition = false;
                try
                {
                    if (file1[i] != file2[i]) condition = true;
                }
                catch (ArgumentOutOfRangeException) {}

                if (condition)
                {   
                    List<string> words1 = file1[i].Split(' ').ToList();
                    List<string> words2 = file2[i].Split(' ').ToList();

                    //words = Max(words1.Count(), words2.Count());
                    words = words1.Count();

                    Output.Write($"\n{i+1}", ConsoleColor.Green);
                    Output.Write($"| ");

                    for(int j = 0; j < words; j++)
                    {
                        try
                        {
                            if (words1[j] == words2[j])
                            {
                                Output.Write(words1[j]+" ");
                            }
                            else
                            {
                                Output.Write(words1[j]+" ", ConsoleColor.Green);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            if (words1.Count > j) Output.Write(words1[j]+" ", ConsoleColor.Green);
                            else Output.Write(words2[j]+" ", ConsoleColor.Green);
                        }
                    }
                }
                else 
                {
                    try { Output.Write($"\n{i+1}| {file1[i]}"); } catch (ArgumentOutOfRangeException) {} 
                }
            }
            Console.Write("\n");
        }

        static int Max(int value1, int value2)
        {
            if (value1 >= value2) return value1;
            else return value2;
        }
    }

    class TextFile : List<string>
    {
        ///<summary>
        ///This loads a text file into the list 'contents'.
        ///</summary>
        ///<param name="filename">
        ///The name of the text file to be loaded.
        ///</param>
        public TextFile(string filename)
        {
            if (System.IO.File.Exists($"Files/{filename}")) // If the file the user selected exists,
            {
                base.AddRange(System.IO.File.ReadAllLines($"Files/{filename}").ToList()); // Read the files contents and add to this file.
            }
            else // Otherwise,
            {
                Output.FileNotFound(filename); // Display an error.
            }
        }
    }

    class Output
    {
        public static void FileNotFound(string filename)
        {
            Console.WriteLine($"Error: {filename} not found.");
        }

        public static void InvalidCommand(string command)
        {
            Console.WriteLine($"Error: Command Not Recognised: {command}");
        }

        public static void InvalidArguments(string command, int arguments_given, int arguments_expected)
        {
            Console.WriteLine($"Error: '{command}' expects {arguments_expected} arguments, but {arguments_given} are given.");
        }

        public static void Write(string str, ConsoleColor color=ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}