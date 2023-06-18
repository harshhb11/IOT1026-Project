using System;

namespace MinotaurLabyrinth
{
    public class YourRoomName : Room
    {
        public override RoomType Type { get; } = RoomType.Harshroom;

        // Custom property for your room
        public bool IsTrapActivated { get; private set; }

        // Custom method to activate the trap in the room
        public void ActivateTrap()
        {
            IsTrapActivated = true;
            Console.WriteLine("The trap in the room has been activated!");
        }

        // Override the Activate method to add custom room activation logic
        public override void Activate(Hero hero, Map map)
        {
            base.Activate(hero, map);

            // Check if the trap is activated and apply its effect to the hero
            if (IsTrapActivated)
            {
                hero.TakeDamage(10);
                Console.WriteLine("You got trapped and took 10 damage!");
            }
        }

        // Override the DisplaySense method to provide custom sensory information about the room
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            // Customize the displayed sense information based on the hero's distance from the room
            if (heroDistance <= 2)
            {
                Console.WriteLine("You hear the sound of gears grinding in the room.");
                return true;
            }
            
            return false;
        }

        // Override the Display method to customize the room's display representation
        public override DisplayDetails Display()
        {
            if (IsTrapActivated)
                return new DisplayDetails("[X]", ConsoleColor.Red); // Display the room with an activated trap
            else
                return new DisplayDetails("[ ]", ConsoleColor.Gray); // Display the room without a trap
        }
    }
}