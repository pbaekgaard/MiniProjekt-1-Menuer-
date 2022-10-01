using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

public interface IMenuItem
{
    public void Select();
    public string Title();

    public bool IsFile();
}
public class MenuItem : IMenuItem
{
    public void CenterPrint(string printString)
    {
        Console.SetCursorPosition((Console.WindowWidth - printString.Length) / 2, Console.CursorTop);
        Console.WriteLine(printString);
    }
    public bool IsFile()
    {
        return false;
    }
    private bool _menuOn = true;

    public string Title()
    {
        return this._Title;
    }
    public MenuItem(string _title, string _content)
    {
        _Title = _title;
        Content = _content;
    }

    public void Select()
    {

        do
        {
            Console.Clear();
            CenterPrint(this.Title());
            CenterPrint(this.Content);
            // Console.WriteLine(this.Title());
            // Console.WriteLine(this.Content);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Escape:
                case ConsoleKey.Enter:
                    _menuOn = false;
                    break;
                default:
                    _menuOn = true;
                    break;
            }
        } while (_menuOn);
    }
    public string _Title;
    public string Content;
}