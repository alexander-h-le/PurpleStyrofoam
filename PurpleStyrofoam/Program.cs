using System;

namespace PurpleStyrofoam
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Test");
            using (var game = new Game1())
                game.Run();
        }
    }
}
