using System;

namespace ConsoleApplication
{
    public static class Options
    {
        static AppHelper app = new AppHelper();
        static DynamicMenu menu = new DynamicMenu();   
        public static string input;
        public static string[] mainMenu = {"Get list of files in directory", "Get list of folders in directory", "Manage files", "Manage folders", "Generate index file"};
        public static string[] filesMenu = {"Create file", "Delete file", "Move file", "Rename File", "Read text from file", "Write text to file", "Search file for text", "Generate index file", "Return to MAIN MENU"};
        public static string[] foldersMenu = {"Create folder", "Delete folder", "Move Folder", "Rename Folder", "Return to MAIN MENU"};
        
        static Options()
        {
            
        }

        public static void MainMenuOptions(int selectedItem)
        {
            switch(selectedItem)
            {
                case 0:
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a directory path: ");
                    input = Console.ReadLine();
                    
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        string[] files = app.ListFilesInDirectory(input);
                        Console.WriteLine("input = " + input);
                        ListMaker list = new ListMaker();
                        TableMaker tableMaker = new TableMaker();
                        
                        string[] table = list.CreateTable(files, "file", Options.input);
                        tableMaker.PrintTableToConsole(table);
                        long listSize = app.GetSizeOfFileList(input);
                        Console.WriteLine("The total size of the files within this folder (excluding subfolders) is: " + Utilities.SelectAppropriateFileSizeFormat(listSize));
                        
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(NullReferenceException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! Invalid user input!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }   
                }   
                case 1: 
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a directory:");
                    input = Console.ReadLine();
                    try
                    {
                        var list = new ListMaker();
                        Console.ForegroundColor = ConsoleColor.Green;
                        string[] folders = app.ListSubfoldersInDirectory(input);
                        list.CreateTable(folders, "folder", Options.input);
                        long totalSize = app.GetSizeOfDirectory(input) - app.GetSizeOfFileList(input);
                        Console.WriteLine("The total size of the subfolders within this directory is: " + Utilities.SelectAppropriateFileSizeFormat(totalSize));

                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! Invalid user input!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }  
                }
                case 2:
                {
                    Console.Clear();
                    Console.WriteLine("FILE MANAGER");
                    menu.Menu(filesMenu, 2);
                    break;
                }
                case 3:
                {
                    Console.Clear();
                    Console.WriteLine("FOLDER MANAGER");
                    menu.Menu(foldersMenu, 3);
                    break;
                }
                case 4:
                {
                    Console.Clear();
                    Console.WriteLine("This option creates a text file which compiles information on all of the files and folders in the location specified.");
                    Console.WriteLine("To continue, please enter the directory path where you would like to create your index file:");
                    input = Console.ReadLine();

                    try
                    {
                        app.CreateIndexFile(input);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("SUCCESS! Your new index file has been created at " + input + "\\index.txt");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! Invalid user input!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                case 5:
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("GOODBYE!");
                    Environment.Exit(0);
                    break;
                }
            }
        }

        public static void ManageFiles(int selectedItem)
        {
            switch(selectedItem)
            {
                //CreateFile
                case 0:
                {
                    Console.Clear();
                    Console.WriteLine("Enter the path for the file you wish to create: ");
                    string input = Console.ReadLine();

                    try
                    {
                        if(app.CheckFileExists(input) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SUCCESS! Your file has been created.");
                            menu.Menu(mainMenu, 1);
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! Could not create a file in this location!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! Either the file already exists or you have entered an invalid file path!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Delete File
                case 1:
                {
                    Console.Clear();
                    Console.WriteLine("WARNING! This function will permenantly delete the specified file. To cancel, press 'C'.");
                    Console.WriteLine("Enter the path for the file you wish to delete: ");
                    string input = Console.ReadLine();

                    if(input == "c" || input == "C")
                    {
                        menu.Menu(filesMenu, 1);
                    }
                        
                    try
                    {
                        app.RemoveFile(input);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("SUCCESS! File has been removed");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! This file path does not exist!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Move File
                case 2:
                {
                    Console.Clear();
                    Console.WriteLine("Please enter the path of the file that you wish to move: ");
                    string input1 = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the path you wish to move the file to: ");
                    string input2 = Console.ReadLine();

                    try
                    {
                        app.MoveFile(input1, input2);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Operation successful!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The file you are trying to move doesn't exist!");
                        menu.Menu(mainMenu, 1);
                        break;
                    } 
                }
                //Rename file
                case 3:
                {
                    Console.Clear();
                    menu.Menu(mainMenu, 1);
                    break;
                }
                //Read text from file
                case 4:
                {
                    Console.Clear();
                    Console.WriteLine("Select file to read: ");
                    string input = Console.ReadLine();

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(app.ReadTextFromFile(input));
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The file you are trying to read doesn't exist!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Write text to file
                case 5:
                {
                    Console.Clear();
                    Console.WriteLine("Select file to write to:");
                    string input1 = Console.ReadLine();
                    Console.WriteLine("Enter the text to write to the file:");
                    string input2 = Console.ReadLine();

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        app.WriteTextToFile(input1, input2);
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The file you are trying to write to doesn't exist!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }  
                }
                //search file for text
                case 6:
                {
                    Console.Clear();
                    Console.WriteLine("Select file to search:");
                    string input1 = Console.ReadLine();
                    Console.WriteLine("Enter the text to search for:");
                    string input2 = Console.ReadLine();
                    
                    try
                    {
                        if(app.SearchForTextInFile(input1, input2) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(input1 + " includes the phrase '" + input2 + "'");
                            menu.Menu(mainMenu, 1);
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(input1 + " does NOT contain the phrase '" + input2 + "'");
                        menu.Menu(mainMenu, 1);
                        break;  
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! File path cannot be found!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Return to menu
                case 7:
                {
                    Console.Clear();
                    menu.Menu(mainMenu, 1);
                    break;
                }
            }
        }

        public static void ManageFolders(int selectedItem)
        {
            switch(selectedItem)
            {
                //Create Folder
                case 0:
                {
                    Console.Clear();
                    Console.WriteLine("Enter the path for the folder you wish to create:");
                    string input = Console.ReadLine();

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        app.CreateNewFolder(input);
                        Console.WriteLine("SUCCESS! New folder created at " + input);
                        menu.Menu(mainMenu, 1);
                        break;
                    }

                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The requested directory path is invalid!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Delete Folder
                case 1:
                {
                    Console.Clear();
                    Console.WriteLine("WARNING! This function will permenantly delete the specified folder and everything contained within. To cancel, enter 'C'.");
                    Console.WriteLine("Enter the path of the folder you wish to delete:");
                    string input = Console.ReadLine();

                    if(input == "c" || input == "C")
                    {
                        menu.Menu(mainMenu, 1);
                    }

                    try
                    {
                        app.RemoveFolder(input, true);

                        if(app.CheckFolderExists(input) == false)
                        {   
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SUCCESS! The specified folder has been removed.");
                            menu.Menu(mainMenu, 1);
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The specified folder could not be removed.");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The folder you are trying to remove does not exist");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Move FOLDER
                case 2:
                {
                    Console.Clear();
                    Console.WriteLine("Enter the name of the folder you would like to move:");
                    string input1 = Console.ReadLine();
                    Console.WriteLine("Enter the location where you would like to move the folder:");
                    string input2 = Console.ReadLine();
                    app.MoveFolder(input1, input2);

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("SUCCESS! Your folder has been moved to " + input2);
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The folder you are trying to move does not exist");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Rename FOLDER
                case 3:
                {
                    Console.Clear();
                    Console.WriteLine("Enter the path of the folder you would like to rename:");
                    string input1 = Console.ReadLine();
                    Console.WriteLine("Enter a new name for the folder:");
                    string input2 = Console.ReadLine();
                    string destinationPath = input1.Remove(input1.LastIndexOf(@"\")) + @"\" + input2;
                    Console.WriteLine(destinationPath);
                    
                    if (input1 == destinationPath)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Your folder is already named'" + input2 + "'!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    
                    app.RenameFolder(input1, input2);

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("SUCCESS! Your folder has been renamed '" + input2 + "'!");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                    catch(ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR! The folder you are trying to move does not exist");
                        menu.Menu(mainMenu, 1);
                        break;
                    }
                }
                //Return to menu
                case 4:
                {
                    Console.Clear();
                    menu.Menu(mainMenu, 1);
                    break;
                }
            }
        }
    }
}