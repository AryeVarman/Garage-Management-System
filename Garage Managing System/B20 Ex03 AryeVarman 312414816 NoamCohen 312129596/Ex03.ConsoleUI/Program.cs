using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

// $G$ DSN-999 (-15)  you implemented a system without a car or motorcycle but with a truck (in this exercise requirements, you asked to create and use those classes, in addition to other things)

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {

            // $G$ CSS-000 (-3) The variable name is not meaningful and understandable, don't use abbreviation.
            Interface i = new Interface();
            GarageObjectGenerator g = new GarageObjectGenerator();
            Garage h = new Garage();

            i.RunSystem(h, g);
        }
    }
}
