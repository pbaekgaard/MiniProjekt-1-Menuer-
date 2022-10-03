using System.ComponentModel.Design;
using System.Runtime.InteropServices.ComTypes;
using static System.Console;
namespace Menu2;
public class Menu : IMenuItem
{
    public string Title()
    {
        return this._Title;
    }
    private string prefix = " >>";
    private string postfix = "<< ";
    private List<IMenuItem> MenuItems = new List<IMenuItem>();
    bool _running = true;
    private int selected;
    public Menu(string _title)
    {
        _Title = _title;
    }

    public bool IsFile()
    {
        return false;
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
        _running = true;
        ConsoleKey keyPressed;
        ConsoleKeyInfo keyInfo = ReadKey();
        keyPressed = keyInfo.Key;
        switch (keyPressed)
        {
            case ConsoleKey.Backspace:
            {
                _running = false;
                break;
            }
            case ConsoleKey.Escape:
            {
                Environment.Exit(1);
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
            }
            else
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
        do
        {
            DrawMenu();
            HandleInput();
        } while (_running);
    }
}