namespace MinotaurLabyrinth
{
    static class Display
    {
        public static void ScreenUpdate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Cyan);
            ConsoleHelper.WriteLine("                    ~~~~~~ Welcome to the Minotaur's Labyrinth ~~~~~~", ConsoleColor.Yellow);
            ConsoleHelper.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Cyan);
            Console.WriteLine();
            ConsoleHelper.WriteLine("Map Overview:", ConsoleColor.White);
            DisplayMap(hero, map);
            Console.WriteLine();
            ConsoleHelper.WriteLine("Available Commands:", ConsoleColor.White);
            ConsoleHelper.Write($"{hero.CommandList}", ConsoleColor.White);
            Console.WriteLine();
            DisplayStatus(hero, map);
            ConsoleHelper.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Cyan);
            Console.WriteLine();
        }

        public static void DisplayStatus(Hero hero, Map map)
        {
            bool somethingSensed = DisplaySenses(hero, map);
            if (!somethingSensed)
                ConsoleHelper.WriteLine("You sense nothing of interest nearby.", ConsoleColor.Gray);
            if (hero.HasSword)
                ConsoleHelper.WriteLine("You are currently wielding the enchanted sword! Use it wisely!", ConsoleColor.Magenta);
        }

        private static bool DisplaySenses(Hero hero, Map map)
        {
            int sensedRooms = 0;
            var sensableLocs = map.GetSensableLocations(hero);
            (int px, int py) = hero.Location;
            foreach (var loc in sensableLocs)
            {
                var room = map.GetRoomAtLocation(loc);
                (int lx, int ly) = loc;
                int distanceFromPlayer = Math.Abs(lx - px) + Math.Abs(ly - py);
                if (room.DisplaySense(hero, distanceFromPlayer))
                    sensedRooms++;
            }
            return sensedRooms > 0;
        }

        public static void DisplayMap(Hero hero, Map map)
        {
            map.Display(hero.Location, hero.HasMap, map.DebugMode);
            Console.WriteLine();
            ConsoleHelper.WriteLine("Legend:", ConsoleColor.White);
            ConsoleHelper.WriteLine("M - Minotaur", ConsoleColor.Red);
            ConsoleHelper.WriteLine("P - Player", ConsoleColor.Green);
            ConsoleHelper.WriteLine("E - Exit", ConsoleColor.Cyan);
            ConsoleHelper.WriteLine("X - Wall", ConsoleColor.DarkGray);
            ConsoleHelper.WriteLine("R - Room", ConsoleColor.Gray);
        }
    }

    public static class ConsoleHelper
    {
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Write(DisplayDetails details)
        {
            Console.ForegroundColor = details.Color;
            Console.Write(details.Text);
            Console.ResetColor();
        }

        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}