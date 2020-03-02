// 场景层
using System;
using System.Collections.Generic;
namespace RetroSnaker
{
    class InitConfigManager
    {
        private static int Width;
        private static int Height;
        private static int InitLeng;
        private static int MoveFps;
        private static InitConfig DefaultConfig = new InitConfig(){Width = 10,Height = 10, InitLeng = 3,MoveFps = 12};
        public static InitConfig Init(){
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Input game config.");
            if (InputDefault()) {
                Console.Clear();
                return DefaultConfig;
            } else {
                InputWidth();
                InputHeight();
                InputLeng();
                InputMoveFps();
                Console.Clear();
                return new InitConfig(){Width = Width,Height = Height, InitLeng = InitLeng,MoveFps = MoveFps};
            }
        }
        private static bool InputDefault(){
            Console.Write("Enter use default config (y/n): ");
            String input = Console.ReadLine();
            if (input == "n") {
                return false;
            }
            return true;
        }
        private static void InputWidth(){
            Console.Write("Enter map width (5-30): ");
            String input = Console.ReadLine();
            int intInput;
            bool success = Int32.TryParse(input,out intInput);
            if (success && intInput >= 5 && intInput <= 30) {
                Width = intInput + 2;
                // Console.WriteLine("You input true number: " + intInput.ToString());
            } else {
                Console.WriteLine("You input error value. Please retry.");
                InputWidth();
            }
        }
        private static void InputHeight(){
            Console.Write("Enter map height (5-30): ");
            String input = Console.ReadLine();
            int intInput;
            bool success = Int32.TryParse(input,out intInput);
            if (success && intInput >= 5 && intInput <= 30) {
                Height = intInput + 2;
                // Console.WriteLine("You input true number: " + intInput.ToString());
            } else {
                Console.WriteLine("You input error value. Please retry.");
                InputHeight();
            }
        }
        private static void InputLeng(){
            Console.Write("Enter init snacker length (1-map width): ");
            String input = Console.ReadLine();
            int intInput;
            bool success = Int32.TryParse(input,out intInput);
            if (success && intInput >= 1 && intInput <= Width - 2) {
                InitLeng = intInput;
                // Console.WriteLine("You input true number: " + intInput.ToString());
            } else {
                Console.WriteLine("You input error value. Please retry.");
                InputLeng();
            }
        }
        private static void InputMoveFps(){
            Console.Write("Enter snacker moveFps (1-60)fps/cell: ");
            String input = Console.ReadLine();
            int intInput;
            bool success = Int32.TryParse(input,out intInput);
            if (success && intInput >= 1 && intInput <= 60) {
                MoveFps = intInput;
                // Console.WriteLine("You input true number: " + intInput.ToString());
            } else {
                Console.WriteLine("You input error value. Please retry.");
                InputMoveFps();
            }
        }
    }
}