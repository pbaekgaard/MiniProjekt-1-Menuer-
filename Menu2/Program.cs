using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Shell;

namespace Menu2
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu MainMenu = new Menu("fancymenu");
            MainMenu.Add(new MenuItem("Punkt1", "Indhold af punkt 1... det er indtil videre bare tekst"));
            MainMenu.Add(new MenuItem("Punkt2", "Indhold af punkt 2... det er indtil videre også bare tekst"));
            MainMenu.Add(new MenuItem("Et lidt længere menupunkt", "Indhold af punkt 3... det er indtil videre også bare tekst"));
            Menu SubMenu = new Menu("SubMenu");
            SubMenu.Add(new MenuItem("SubPunkt1 ", "Indhold af punkt 1... det er indtil videre bare tekst"));
            SubMenu.Add(new MenuItem("SubPunkt2", "Indhold af punkt 2... det er indtil videre også bare tekst"));
            SubMenu.Add(new MenuItem("SUUUUUB Et lidt længere menupunkt", "Indhold af punkt 3... det er indtil videre også bare tekst"));
            MainMenu.Add(SubMenu);
            MainMenu.Add(new InfiniteMenu("Uendelig menu"));
            MainMenu.Add(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? new FileSystemMenu("Browse ~/ directory", new DirectoryInfo("/home/flcky98/"))
                : new FileSystemMenu("Browse Desktop Folder", new DirectoryInfo(KnownFolders.Desktop.Path)));

            MainMenu.Start();
        }
    }
}
