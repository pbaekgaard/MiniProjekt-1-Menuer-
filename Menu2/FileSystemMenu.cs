using System.ComponentModel.Design;
using System.IO.Compression;
using System.Runtime.InteropServices.ComTypes;
using static System.Console;
namespace Menu2;
public class FileSystemMenu : IMenuItem
{
    private FileInfo[] Files;
    private DirectoryInfo[] Folders;
    public string Title()
    {
        return this._Title;
    }
    private string prefix = " >>";
    private string postfix = "<< ";
    private List<IMenuItem> MenuItems = new List<IMenuItem>();
    bool _running = true;
    private int selected;
    public FileSystemMenu(string _title, DirectoryInfo directoryInfo)
    {
        _Title = _title;
        Files = directoryInfo.GetFiles();
        Folders = directoryInfo.GetDirectories();
    }

    private string _Title { get; }

    public void Add(IMenuItem item)
    {
        MenuItems.Add(item);
    }

    public void Start()
    {
        selected = 0;
        Select();
    }

    private void HandleInput()
    {
        ConsoleKey keyPressed;
        ConsoleKeyInfo keyInfo = ReadKey();
        keyPressed = keyInfo.Key;
        switch (keyPressed)
        {
            case ConsoleKey.Backspace:
            case ConsoleKey.Escape:
                {
                    _running = false;
                    break;
                }
            case ConsoleKey.UpArrow:
                selected = selected == 0 ? MenuItems.Count - 1 : selected - 1;
                break;
            case ConsoleKey.DownArrow:
                selected = selected == MenuItems.Count - 1 ? 0 : selected + 1;
                break;
            case ConsoleKey.Enter:
                MenuItems[selected].Select();
                break;
            default:
                break;
        }
    }

    public void CenterPrint(string printString)
    {
        Console.SetCursorPosition((Console.WindowWidth - printString.Length) / 2, Console.CursorTop);
        Console.WriteLine(printString);
    }
    public bool IsFile()
    {
        return false;
    }
    private void DrawMenu()
    {
        Console.Clear();
        BackgroundColor = ConsoleColor.Cyan;
        ForegroundColor = ConsoleColor.Black;
        CenterPrint(_Title);
        // Console.WriteLine(_Title);
        Console.ResetColor();
        foreach (IMenuItem item in MenuItems)
        {
            if (item == MenuItems[selected])
            {
                BackgroundColor = ConsoleColor.White;
                ForegroundColor = ConsoleColor.Black;
                CenterPrint($"{prefix}{item.Title()}{postfix}");
                // Console.WriteLine($"{prefix}{item.Title()}{postfix}");
            } else if (item.IsFile())
            {
                BackgroundColor = ConsoleColor.Red;
                ForegroundColor = ConsoleColor.Black;
                CenterPrint($"{prefix}{item.Title()}{postfix}");
            } else
            {
                BackgroundColor = ConsoleColor.Black;
                ForegroundColor = ConsoleColor.White;
                CenterPrint(item.Title());
                // Console.WriteLine(item.Title());
            }
        }
        Console.ResetColor();
    }

    public void Select()
    {
        Console.WriteLine(MenuItems.Count);
        foreach (DirectoryInfo folder in Folders)
        {
            MenuItems.Add( new FileSystemMenu(folder.Name, new DirectoryInfo($"{folder.Parent+"/"+folder.Name}")));
            // Console.WriteLine(folder.Name);
        }

        foreach (FileInfo file in Files)
        {
            MenuItems.Add(new FileItem(file.Name, file));
        }
        Console.WriteLine(MenuItems.Count);
        
        do
        {
            DrawMenu();
            HandleInput();
        } while (_running);
    }
}