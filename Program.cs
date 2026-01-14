using System.Text;
string AppTmpDir = @"C:\tmp\";
string PythonFile = AppTmpDir + @"PyKube.py";
string BatchFile = AppTmpDir + @"PyKube.bat";
bool DoCleanTMPFiles = false;
int colorNumber = 2;
string KubeParts = "#";

Console.BackgroundColor = ConsoleColor.Black;
List<ConsoleColor> colorsList = new List<ConsoleColor>()
{
    ConsoleColor.Black,
    ConsoleColor.DarkBlue,
    ConsoleColor.DarkGreen,
    ConsoleColor.DarkCyan,
    ConsoleColor.DarkRed,
    ConsoleColor.DarkMagenta,
    ConsoleColor.DarkYellow,
    ConsoleColor.Gray,
    ConsoleColor.DarkGray,
    ConsoleColor.Blue,
    ConsoleColor.Green,
    ConsoleColor.Cyan,
    ConsoleColor.Red,
    ConsoleColor.Magenta,
    ConsoleColor.Yellow,
    ConsoleColor.White
};

SayIt("Enter the size of the cube X :");
int X = Convert.ToInt32(Console.ReadLine());
Console.Clear();
int staticX = X;
Console.WriteLine("Enter the size of the cube Y :");
int Y = Convert.ToInt32(Console.ReadLine());
int staticY = Y;
Console.Clear();
changeColor();
KubeBuilder();

void changeColor()
{
    var color = colorsList[colorNumber];
    Console.Clear();
    colorNumber++;
    Console.ForegroundColor = color;
    Console.WriteLine($"Change Color ? \n press enter for next\n Escape to continue \n Color : {color} color number : {colorNumber}");
    ConsoleKeyInfo key = Console.ReadKey(true);
    if (colorNumber > 15) { colorNumber = 0; }
    if (key.Key == ConsoleKey.Escape) { return; }
    changeColor();
}

void KubeBuilder()
{
    colorNumber--;
    string pyTranslatedColor = (@"'" + colorsList[colorNumber] + "'");
    Y = staticY;
    Console.Clear();

    while (Y != 0)
    {
        X = staticX * 2;
        KubeXbuilder();
        X = staticX * 2;
        Y--;
        Thread.Sleep(10);
        Console.WriteLine("");

    }

    void KubeXbuilder()
    {
        while (X != 0)
        {
            X--;
            KubePartsBuilder();
        }
    }

    Console.WriteLine("\n Press Enter to Generate Final Cube ");
    Console.ReadLine();
    GeneratePythonKube(AppTmpDir, PythonFile, BatchFile, DoCleanTMPFiles, staticX, staticY, pyTranslatedColor);

    void GeneratePythonKube(string AppTmpDir, string PythonFile, string BatchFile, bool DoCleanTMPFiles, int staticX, int staticY, string pyTranslatedColor)
    {
        if (!Directory.Exists(AppTmpDir))
        {
            Directory.CreateDirectory(AppTmpDir);
        }
        var pythonTurtleCode = $"import turtle\r\n\r\nturtle.title('PyKube')\r\nfrom time import sleep\r\nturtle.color({pyTranslatedColor})\r\nfor i in range(2):\r\n    sleep(0.2)\r\n    turtle.begin_fill()\r\n    turtle.forward({staticX * 10})\r\n    turtle.left(90)\r\n    turtle.forward({staticY * 10})\r\n    turtle.left(90)\r\n    sleep(0.2)\r\n    turtle.end_fill()\r\nturtle.penup()\r\nturtle.left(90)\r\nturtle.left(90)\r\nturtle.forward(1000)\r\nturtle.done()\r\n\r\n";
        WriteFile(PythonFile, pythonTurtleCode);
        var batchPythonLaunch = $"start {PythonFile}";
        WriteFile(BatchFile, batchPythonLaunch);

        System.Diagnostics.Process.Start(@"C:\tmp\PyKube.bat");
        if (DoCleanTMPFiles == true) { SayIt("Cleanig TMP files... "); Thread.Sleep(2000); File.Delete(BatchFile); Thread.Sleep(5000); File.Delete(PythonFile); }
        else { Console.Beep(); Console.WriteLine(@"Data stored on C:\tmp"); Thread.Sleep(1000); File.Delete(BatchFile); Console.Read(); }
    }
}

void SayIt(string sayIt) { Console.Write(sayIt); }

void KubePartsBuilder() { Console.Write(KubeParts); }

static void WriteFile(string path, string content)
{
    using FileStream fs = File.Create(path);
    byte[] info = new UTF8Encoding(true).GetBytes(content);
    fs.Write(info, 0, info.Length);
    fs.Flush();
    fs.Close();
}