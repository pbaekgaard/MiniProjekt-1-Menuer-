using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class FileItem : IMenuItem
{
    public void CenterPrint(string printString)
    {
        Console.SetCursorPosition((Console.WindowWidth - printString.Length) / 2, Console.CursorTop);
        Console.WriteLine(printString);
    }
    private bool _menuOn = true;
    public string Title()
    {
        return this._Title;
    }
    public bool IsFile()
    {
        return true;
    }
    public FileItem(string _title, FileInfo _file)
    {
        _Title = _title;
        _File = _file;
    }

    public void Select()
    {
        new Process
        {
            StartInfo = new ProcessStartInfo($"{_File.Directory+"/"+_File.Name}")
            {
                UseShellExecute = true
            }
        }.Start();        // System.Diagnostics.Process.Start(@$"{_File.Directory+"/"+_File.Name}");
    }
    public string _Title;
    private FileInfo _File;
}
