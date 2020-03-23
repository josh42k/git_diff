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
            Console.WriteLine("*OOP Assignment 2*\n"); // The program title is printed to the console.
            Console.Write(">>");
            string[] input = Console.ReadLine().Split(' '); // The user is asked for an input.

            switch (input[0])
            {
                case "diff": // If the user wants to use the 'diff' command,
                    if (input.Count() == 3) // If the user provided the correct amount of parameters,
                    {
                        if (Diff(LoadFile(input[1]), LoadFile(input[2]))) Console.WriteLine($"{input[1]} and {input[2]} are different"); // The user is told whether or not the files are different.
                        else Console.WriteLine($"{input[1]} and {input[2]} are not different");
                    }
                    else Console.WriteLine("Error: 'diff' takes exactly 2 arguments."); // If the user did not provide the correct amount of parameters, an error is displayed.
                    break;
                case "help": // If the user wants to use the 'help' command,
                    if (input.Count() == 1) Console.WriteLine("Commands: \n'help': Displays available commands.\n'diff <file1> <file2>': Tests to see if there are any differences between two files.\n'exit': Closes the program."); // They are shown a list of commands.
                    else Console.WriteLine("Error: 'help' does not take any arguments."); // But, if the user did not provide the correct amount of parameters, an error is displayed instead.
                    break;
                case "exit": // If the user wants to use the 'exit' command,
                    if (input.Count() == 1) return false; // The loop is stopped, closing the program.
                    else Console.WriteLine("Error: 'exit' does not take any arguments."); // But, if the user did not provide the correct amount of parameters, an error is displayed instead.
                    break;
                default: // If the user's input is not recognised, 
                    Console.WriteLine($"Error: Command Not Recognised: {input[0]}"); // An error message is diplayed.
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
        static bool Diff(string string1, string string2)
        {
            if (string1 == string2) return false; // If the strings are identical, return false,
            else return true; // Otherwise, return true.
        }

        ///<summary>
        ///This loads a text file into a string.
        ///</summary>
        ///<param name="filename">
        ///The name of the text file to be loaded.
        ///</param>
        ///<returns>
        ///Returns the file as a string.
        ///</returns>
        static string LoadFile(string filename)
        {
            if (System.IO.File.Exists($"Files/{filename}")) // If the file the user selected exists,
            {
                return System.IO.File.ReadAllText($"Files/{filename}"); // Return the contents as a string.
            }
            else // Otherwise,
            {
                Console.WriteLine($"Error: {filename} not found."); // Display an error message.
                return filename;
            }
        }
    }
}