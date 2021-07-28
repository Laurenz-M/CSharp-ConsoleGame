using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using MainGuiNamespace;

namespace Game
{
    using MainGuiNamespace;

    static class blackjack
    {
        public static void MainGame()
        {

            for (string newGame = "ja"; newGame == "ja";)
            {

                Random draw_Card = new Random();
                Random first_Card = new Random();

                int Hand_Value = 0;
                int Ace_Count = 4;
                int King_Count = 4;
                int Queen_Count = 4;
                int Jack_Count = 4;
                int Ten_count = 4;
                int Nine_Count = 4;
                int Eight_Count = 4;
                int Seven_Count = 4;
                int Six_Count = 4;
                int Five_Count = 4;
                int Four_Count = 4;
                int Three_Count = 4;
                int Two_Count = 4;
                int[] Card_Deck_Quantity = { -1, -1, Two_Count, Three_Count, Four_Count, Five_Count, Six_Count, Seven_Count, Eight_Count, Nine_Count, Ten_count, Jack_Count, Queen_Count, King_Count, Ace_Count };
                string[] Card_Deck_Names = { "", "", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "jack", "queen", "king", "ace" };

                Console.Clear();
                //Console.WriteLine("Quantitiy of Card two is:" + Two_Count);
                //Console.WriteLine(Card_Deck_Names[Card_Deck_Names.Length-1]);

                Hand_Value = firstCard(Card_Deck_Names, Card_Deck_Quantity, first_Card, Hand_Value);
                /*int test = 0;
                while (test != 22) {
                    test = 0;
                    Hand_Value = 0;

                    for (int i = 2; i < Card_Deck_Quantity.Length;i++)
                    {
                        //ProgramWL(i + "", "Blue");
                        Card_Deck_Quantity[i] = 4;
                    }

                    test = firstCard(Card_Deck_Names, Card_Deck_Quantity, first_Card, Hand_Value);
                    if(test > 21) { ProgramWL(test+" über 21!","Red"); }
                    Console.WriteLine(test);
                    Thread.Sleep(20);
                }*/
                if (Hand_Value == 21) { Console.WriteLine("    Blackjack!","Green"); continue; }

                for (string newCard = "ja"; newCard == "ja" && Hand_Value <= 21;)
                {

                    Program.WL("\n    Du hast " + Hand_Value + " auf der Hand.");
                    Program.WL("    Möchtest du eine weitere Karte ziehen?"); Program.WL("    <Ja> / <Nein>", "DarkCyan"); Program.W("    ");//Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("    <Ja> / <Nein>"); Console.Write("    ");
                    newCard = Console.ReadLine().Trim().ToLower();
                    while (newCard != "ja" && newCard != "nein")
                    {
                        Program.WL("\n    Ungültige Antwort! Bitte antworte mit <Ja> oder <Nein> !", "Red");
                        Program.W("    Möchtest du eine weitere Karte ziehen?  ");
                        //Program.W("    ");
                        newCard = Console.ReadLine().Trim().ToLower();
                        Program.ClearLines(3);
                    }
                    if (newCard == "ja")
                    {
                        int DrawnCard = drawCard(Card_Deck_Names, Card_Deck_Quantity, first_Card, Hand_Value);
                        Program.WL("    Du hast eine " + DrawnCard + " gezogen!", "Green");
                        Hand_Value += DrawnCard;
                    }
                }

                if (Hand_Value > 21)
                {
                    Program.WL("    Du hast Überzogen! Du hattest " + Hand_Value + " auf der Hand. Du verlierst!", "Red");
                }
                else if (Hand_Value <= 21)
                {
                    Program.WL("    Du willst keine Karte mehr ziehen. Der Dealer zeigt dir jetzt sein Blatt.");
                    int dealer_Hand_Value = dealerDraw(Card_Deck_Names, Card_Deck_Quantity, first_Card, draw_Card, Hand_Value);
                    if (dealer_Hand_Value > 21) Program.WL("    Der Dealer hat mit " + dealer_Hand_Value + " überzogen! Du gewinnst!","Green");
                    else if (dealer_Hand_Value < Hand_Value) Program.WL("    Du hast eine höhere Hand als der Dealer! Der Dealer hat nur " + dealer_Hand_Value + " auf der Hand. Du gewinnst!","Green");
                    else if (dealer_Hand_Value == Hand_Value) Program.WL("    Deine Hand hat den gleichen Wert wie die des Dealers. Ihr hattet beide " + dealer_Hand_Value + " auf der Hand. Es ist unentschieden!","Yellow");
                    else Program.WL("    Der Dealer hat mit " + dealer_Hand_Value + " eine höhere Hand als du mit " + Hand_Value + ". Du verlierst!","Red");
                }

                Thread.Sleep(700);
                Program.WL("\n    Möchtest du erneut spielen?"); Program.W("    ");
                newGame = Console.ReadLine().Trim().ToLower();   // ".Trim()" removes spaces before and afer the input, ".ToLower()" puts the input into lowercase letters.

                while (newGame != "ja" && newGame != "nein")
                {
                    Program.WL("\n    Ungültige Antwort! Bitte antworte mit <Ja> oder <Nein> !", "Red");
                    Program.W("    Möchtest du erneut spielen?  ");
                    newGame = Console.ReadLine().Trim().ToLower();
                    Program.ClearLines(3);
                }
                //Console.ReadKey();
                //ResetArrays and Variables here
                //Hand_Return_Value = 0;
            }
        }

        static int firstCard(string[] Card_Deck_Names, int[] Card_Deck_Quantity, Random first_Card, int Hand_Value)
        {
            int Hand_Return_Value = 0;
            //Hand_Value = 0;


            for (int i = 0; i < 2; i++)
            {

                int Card_Index = first_Card.Next(2, Card_Deck_Names.Length);

                while (Card_Deck_Quantity[Card_Index] <= 0)
                {
                    Card_Index = first_Card.Next(2, Card_Deck_Names.Length);
                    //Console.WriteLine("Redraw:" + Card_Index + " with Quantity of:" + Card_Deck_Quantity[Card_Index]);
                }

                string Card_Name = Card_Deck_Names[Card_Index];
                //ProgramWL("" + Hand_Value, "Green");
                if (Card_Index == 11 || Card_Index == 12 || Card_Index == 13) { Hand_Return_Value += 10; Hand_Value += 10; }
                else if (Card_Index == 14 && Hand_Value < 11) { Hand_Return_Value += 11; Hand_Value += 11; }
                else if (Card_Index == 14 && Hand_Value >= 11) { Hand_Return_Value += 1; Hand_Value += 1; }
                else Hand_Return_Value += Card_Index; Hand_Value += Card_Index;

                Card_Deck_Quantity[Card_Index]--;
                //Hand_Value = 0;

            }
            return Hand_Return_Value;
        }

        static int drawCard(string[] Card_Deck_Names, int[] Card_Deck_Quantity, Random draw_Card, int Hand_Value)
        {
            int Hand_Return_Value = 0;


            int Card_Index = draw_Card.Next(2, Card_Deck_Names.Length);

            while (Card_Deck_Quantity[Card_Index] <= 0)
            {
                Card_Index = draw_Card.Next(2, Card_Deck_Names.Length);
                //Console.WriteLine("drawCard Redraw");
            }

            string Card_Name = Card_Deck_Names[Card_Index];
            //ProgramWL("" + Hand_Value, "Green");
            if (Card_Index == 11 || Card_Index == 12 || Card_Index == 13) { Hand_Return_Value += 10; Hand_Value += 10; } //{ Hand_Return_Value += 10; Console.WriteLine("+10"); }
            else if (Card_Index == 14 && Hand_Value >= 11) { Hand_Return_Value += 1; Hand_Value += 1; }//{ Hand_Return_Value += 1; Console.WriteLine("+1"); }
            else if (Card_Index == 14 && Hand_Value < 11) { Hand_Return_Value += 11; Hand_Value += 11; } //{ Hand_Return_Value += 11; Console.WriteLine("+11"); }
            //ProgramWL("" + Card_Index, "Green");
            else Hand_Return_Value += Card_Index; Hand_Value += Card_Index; //Console.WriteLine("hey");
            Card_Deck_Quantity[Card_Index]--;


            /*Console.ForegroundColor = ConsoleColor.Red;
            ProgramWL("" + Hand_Return_Value, "Red");
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine(Card_Deck_Quantity[Card_Index]);*/
            return Hand_Return_Value;
        }

        static int dealerDraw(string[] Card_Deck_Names, int[] Card_Deck_Quantity, Random first_Card, Random draw_Card, int Hand_Value)
        {
            int dealer_Hand_Value = firstCard(Card_Deck_Names, Card_Deck_Quantity, first_Card, Hand_Value);
            //Console.WriteLine(dealer_Hand_Value);
            while (dealer_Hand_Value < 17)
            {
                //Console.WriteLine("dealerDraw");
                dealer_Hand_Value += drawCard(Card_Deck_Names, Card_Deck_Quantity, draw_Card, Hand_Value);
                //Console.WriteLine(dealer_Hand_Value);
            }

            return dealer_Hand_Value;
        }

        /*static void ProgramWL(string WriteLineText, string TextColor = null, string SpaceAfterPrinting = null, string TypeOfPrinting = null, string NewTextColor = null)
        {
            ConsoleColor OriginalColor = Console.ForegroundColor;

            ConsoleColor TestColor;
            if (TextColor != null && Enum.TryParse(TextColor, out TestColor))
            {
                Console.ForegroundColor = TestColor;

            }

            if (TypeOfPrinting == null || TypeOfPrinting == "WriteLine") Console.WriteLine(WriteLineText);
            else Console.Write(WriteLineText);

            if (NewTextColor != null && Enum.TryParse(NewTextColor, out TestColor))
            {
                Console.ForegroundColor = TestColor;
            }
            else Console.ForegroundColor = OriginalColor;

            if (SpaceAfterPrinting != null) Console.Write(SpaceAfterPrinting);
        }*/

        public static void InitializeCards()
        {

        }
    }

    static class guessnumbers
    {
        public static void MainGame()
        {
            Console.Clear();
            for (string newGame = "ja"; newGame == "ja";)
            {
                Program.WL("\n    Das Spiel funktioniert so: Du bestimmst den Abstand der Zahlen und rätst dann eine Zahl dazwischen.");

                Program.W("    Niedrigste Zahl: ");
                string firstNumberString = Console.ReadLine();
                float firstNumberFloat = Program.WhileNotNumber(firstNumberString);

                Program.W("    Höchste Zahl: ");
                string secondNumberString = Console.ReadLine();
                float secondNumberFloat = Program.WhileNotNumber(secondNumberString);

                Program.W("    Rate nun eine Zahl zwischen der höchsten und der niedrigsten Zahl:");
                string guessedNumberString = Console.ReadLine();
                float guessedNumberFloat = Program.WhileNotNumber(guessedNumberString);
                //int guessedNumberFloat = Convert.ToInt32(Program.WhileNotNumber(guessedNumberString));

                while (firstNumberFloat > secondNumberFloat || firstNumberFloat < 0 || secondNumberFloat < 0 || guessedNumberFloat < firstNumberFloat || guessedNumberFloat > secondNumberFloat)
                {
                    Console.Clear();
                    Program.WL("    Deine erste Zahl muss kleiner sein als die Zweite!\n    Denke daran, dass beide Zahlen nicht kleiner als null sein dürfen!");
                    Program.W("    Gib deine erste Zahl erneut ein: ");
                    firstNumberString = Console.ReadLine();
                    firstNumberFloat = Program.WhileNotNumber(firstNumberString);
                    Program.W("    Gib deine zweite Zahl erneut ein: ");
                    secondNumberString = Console.ReadLine();
                    secondNumberFloat = Program.WhileNotNumber(secondNumberString);
                    Program.W("    Wähle deine Zahl zwischen der höchsten und der niedrigsten Zahl erneut: ");
                    guessedNumberString = Console.ReadLine();
                    guessedNumberFloat = Program.WhileNotNumber(guessedNumberString);
                }


                Random randomOne = new Random();
                double randomNumber = randomOne.NextDouble(firstNumberFloat, secondNumberFloat);
                randomNumber = Math.Round(randomNumber, 0);

                double probability = 100 / (secondNumberFloat - firstNumberFloat + 1);
                probability = Math.Round(probability, 1);

                if (guessedNumberFloat == randomNumber)
                {
                    Program.WL("\n    Super! Die zufällige Zahl war " + randomNumber + " und du hast sie erraten!\n    Deine Change lag bei " + probability + " %!", "DarkCyan");
                }
                else
                {
                    Program.WL("\n     Schade! Die zufällige Zahl war " + randomNumber + " aber du hast auf " + guessedNumberFloat + " getippt...\n  Nächstes mal liegst du bestimmt richtig!\n     Deine Change lag bei " + probability + " %!", "Red");
                }
                Thread.Sleep(700);
                Program.WL("    Möchtest du erneut spielen?");
                newGame = Console.ReadLine().Trim().ToLower();   // ".Trim()" removes spaces before and afer the input, ".ToLower()" puts the input into lowercase letters.

                while (newGame != "ja" && newGame != "nein")
                {
                    Program.WL("\n    Ungültige Antwort! Bitte antworte mit <Ja> oder <Nein> !", "Red");
                    Program.W("    Möchtest du erneut spielen?  ");
                    newGame = Console.ReadLine().Trim().ToLower();
                    Program.ClearLines(3);
                }
                Console.Clear();

            }
        }
    }

    static class rolldice
    {
        public static void MainGame()
        {
            //int StreakRolldice = 0;
            for (string newGame = "ja"; newGame == "ja";)
            {
                Console.Clear();
                Program.WL("\n    Zwei Würfel werden auf einmal gewürfelt.\n    Wenn du zwei gleiche Zahlen würfelst, gewinnst du!");
                Program.WL("    Drücke <Enter> um zu würfeln!\n ");

                Random randomOne = new Random();

                int diceOne = 0;
                int diceTwo = 1;
                int attempts = 0;

                for (; diceOne != diceTwo; attempts++)
                {
                    while (Console.ReadKey().Key != ConsoleKey.Enter)
                    {
                        //Program.ClearLines(0);
                    }
                    Program.ClearLines(0);
                    //Console.ReadKey();
                    diceOne = randomOne.Next(1, 7);
                    diceTwo = randomOne.Next(1, 7);

                    /*ProgramWL("    Du hast eine ", "", "", "Write");
                    ProgramWL("" + diceOne, "Red", "", "Write");
                    ProgramWL(" und eine ", "", "", "Write");
                    ProgramWL("" + diceTwo, "Red", "", "Write");
                    ProgramWL(" gewürfelt!", "", "", "Write");
                    Program.W(diceOne + "§Red", "Blue");*/
                    Program.ColorParts("    Du hast eine ", diceOne + "§Red", " und eine ", diceTwo + "§Red", " gewürfelt!");

                }
                if (attempts != 1)
                {
                    /*ProgramWL("\n    Du hast zwei ", "", "", "Write");
                    ProgramWL(diceOne + "en", "DarkCyan", "", "Write");
                    ProgramWL(" gewürfelt!", "", "", "Write");
                    ProgramWL(" Du hast ", "", "", "Write");
                    ProgramWL("" + attempts, "DarkCyan", "", "Write");
                    ProgramWL(" Würfe gebraucht!\n\n", "", "", "Write");
                    ProgramWL("    Deine Chance lag bei ungefähr ", "", "", "Write");
                    ProgramWL("16,667%", "DarkCyan", "", "Write");
                    ProgramWL("!\n", "", "", "Write");*/
                    Program.ColorParts("    Du hast zwei ", diceOne + "en§DarkCyan", " gewürfelt! ", "Du hast ", attempts + "§DarkCyan", " Würfe gebraucht!\n");
                    Program.ColorParts("    Deine Chance lag bei ungefähr ", "16,667%§DarkCyan", "!\n");
                }
                else
                {
                    /*ProgramWL("\n    Du hast zwei ", "", "", "Write");
                    ProgramWL("" + diceOne, "DarkCyan", "", "Write");
                    ProgramWL(" gewürfelt! Du hast ", "", "", "Write");
                    ProgramWL("einen", "DarkCyan", "", "Write");
                    ProgramWL(" Wurf gebraucht!\n", "", "", "Write");*/
                    Program.ColorParts("    Du hast zwei ", diceOne + "en§DarkCyan", " gewürfelt! ", "Du hast ", "einen§DarkCyan", " Wurf gebraucht!\n");
                    Program.ColorParts("    Deine Chance lag bei ungefähr ", "16,667%§DarkCyan", "!\n");
                }


                Thread.Sleep(700);
                Program.WL("    Möchtest du erneut spielen?");
                newGame = Console.ReadLine().Trim().ToLower();   // ".Trim()" removes spaces before and afer the input, ".ToLower()" puts the input into lowercase letters.

                while (newGame != "ja" && newGame != "nein")
                {
                    Program.WL("\n    Ungültige Antwort! Bitte antworte mit <Ja> oder <Nein> !", "Red");
                    Program.W("    Möchtest du erneut spielen?    ");
                    newGame = Console.ReadLine().Trim().ToLower();
                    Program.ClearLines(3);
                }
                Console.Clear();
            }
        }
    }

    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }

}

namespace MainGuiNamespace
{
    public static class Program
    {

        public static void WL(string WriteLineText, string TextColor = null, string NewTextColor = null)
        {
            ConsoleColor OriginalColor = Console.ForegroundColor;
            ConsoleColor TestColor;

            if (TextColor != null && Enum.TryParse(TextColor, out TestColor))
            {
                Console.ForegroundColor = TestColor;

            }
            Console.WriteLine(WriteLineText);

            if (NewTextColor != null && Enum.TryParse(NewTextColor, out TestColor))
            {
                Console.ForegroundColor = TestColor;
            }
            else Console.ForegroundColor = OriginalColor;
        }

        public static void W(string WriteLineText, string TextColor = null, string SpaceAfterPrinting = null, string TypeOfPrinting = null, string NewTextColor = null)
        {
            ConsoleColor OriginalColor = Console.ForegroundColor;
            ConsoleColor TestColor;

            if (TextColor != null && Enum.TryParse(TextColor, out TestColor))
            {
                Console.ForegroundColor = TestColor;

            }
            Console.Write(WriteLineText);

            if (NewTextColor != null && Enum.TryParse(NewTextColor, out TestColor))
            {
                Console.ForegroundColor = TestColor;
            }
            else Console.ForegroundColor = OriginalColor;
        }

        public static void ColorParts(params string[] Texts)
        {

            foreach (string SingleText in Texts)
            {
                char ch = '§';
                int freq = SingleText.Split(ch).Length - 1;
                if (freq == 0)
                {
                    Program.W(SingleText);
                }
                else
                {
                    string[] OutText =
                    {
                SingleText.Substring(0,SingleText.LastIndexOf(ch)),
                SingleText.Substring(SingleText.LastIndexOf(ch))
                };

                    //OutText[1] = Regex.Replace("He\"ll,o Wo'r.ld", "[@,\\.\";'\\\\]", string.Empty);
                    OutText[1] = OutText[1].Replace(Convert.ToString(ch), "");
                    Program.W(OutText[0] + "", "" + OutText[1]);
                    OutText[0] = "";
                    OutText[1] = "";
                }
                //Console.WriteLine("[0]:" + OutText[0] + "\n[1]:" + OutText[1]);
            }
        }

        public static void UserInput()//string Text = null)
        {
            ConsoleColor OriginalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Program.W("    ");  //puts tab in front of user input
            while (Console.Read() != (int)ConsoleKey.Enter)
            {
                //nothing
            }
            //if(Text != null) Program.WL(Text);
            Console.ForegroundColor = OriginalColor;
            //Console.ReadLine();
        }

        public static string WhileInputInvalid(string Input)
        {
            while (Input.Trim().ToLower() != "ja" && Input.Trim().ToLower() != "nein")
            {
                Program.WL("Ungültige Eingabe! Bitte antworte mit " + "Ja" + " oder" + " Nein!:", "Red");
                //Program.UserInput();
                Input = Convert.ToString(Console.ReadLine());
            }
            return Input;
        }

        public static bool IsAllDigits(string s)
        {
            if (s == "") return false;
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static float WhileNotNumber(string Input)
        {
            while (Program.IsAllDigits(Input) == false)
            {
                //float firstNumberFloat = Convert.ToSingle(firstNumberString);
                Program.WL("    Bitte gib eine gültige, ganze Zahl ein!");
                Input = Console.ReadLine();
            }
            return Convert.ToSingle(Input);
        }

        public static void ClearLines(int NumberOfLines)
        {
            int currentLineCursorStart = Console.CursorTop;
            if (Console.CursorTop >= NumberOfLines)
            {
                for (int LineNumber = 0; LineNumber <= NumberOfLines; LineNumber++)
                {
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop - LineNumber);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor);
                }
                Console.SetCursorPosition(0, currentLineCursorStart - NumberOfLines);
            }
            else throw new ApplicationException("Can't clear more lines than available!");
        }
    }

    class MainUi
    {
        static int ChooseGame(string[] GameNames)
        {
            Program.WL("\n    Es gibt " + (GameNames.Length-1) + " verschiedene Spiele für dich zur Auswahl.\n    Was möchtest du spielen?");
            for (int loopVar = 1; loopVar < GameNames.Length; loopVar++)
            {
                Program.WL("    " + loopVar + ". " + GameNames[loopVar], "Yellow");
            }
            Program.W("    Antworte mit der Zahl oder dem Namen des gewünschten Spiels: ");
            //Program.UserInput();

            int tempVar = 0;

            for (bool ValidGame = true; ValidGame == true;)
            {

                string TempChosenGame = Console.ReadLine();
                if(tempVar == 0)
                {
                    tempVar = 1;
                }
                else
                {
                    Program.ClearLines(2);
                }


                if (Program.IsAllDigits(TempChosenGame) == true)
                {
                    int ChosenGameInt = Convert.ToInt32(TempChosenGame);
                    if (ChosenGameInt > 0 && ChosenGameInt < GameNames.Length)
                    {
                        ValidGame = true;
                        return ChosenGameInt;
                    }
                }
                else if (Program.IsAllDigits(TempChosenGame) == false)
                {
                    string ChosenGameString = TempChosenGame;
                    int i = 0;
                    for (string Game = GameNames[0]; Game.Trim().ToLower() != ChosenGameString.ToLower().Trim() && i < GameNames.Length; i++)
                    {
                        if (ChosenGameString.Trim().ToLower() == GameNames[i].Trim().ToLower())
                        {
                            ValidGame = true;
                            return i;
                        }
                    }
                }
                Program.W("\n    Ungültige Eingabe! Bitte gib erneut ein: ", "Red");
            }

            return -1;
        }
        public static void Main()
        {
            /*Program.WL("Was ist dein Name?");
            Program.UserInput();
            string name = Convert.ToString(Console.ReadLine());
            Program.WL("Wie alt bist du?");
            string age = Convert.ToString(Console.ReadLine());
            Program.WL("Du heißt also " + name + " und bist " + age + " Jahre alt?");
            string ageAndNameCorrect = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            ageAndNameCorrect = Program.WhileInputInvalid(ageAndNameCorrect);

            while (ageAndNameCorrect.Trim().ToLower() == "nein")
            {
                Console.Clear();
                Program.WL("\nGib deinen Namen erneut ein: ", "", "ConsoleWrite");
                name = Convert.ToString(Console.ReadLine());
                Program.WL("Gib dein Alter erneut ein: ", "", "ConsoleWrite");
                age = Convert.ToString(Console.ReadLine());
                Program.WL("Du heißt also " + name + " und bist " + age + " Jahre alt?");
                ageAndNameCorrect = Console.ReadLine();
            }
            */

            string[] GameNames = { "", "Blackjack", "Zahlen raten", "Pasch würfeln" };
            Action[] MainGames = { Game.blackjack.MainGame, Game.guessnumbers.MainGame, Game.rolldice.MainGame };
            Thread.Sleep(100);
            Program.W("\n    Willkommen!");
            Thread.Sleep(100);

            for (string newGame = "ja"; newGame == "ja";)
            {
                MainGames[MainUi.ChooseGame(GameNames) - 1]();
                Console.Clear();
                Program.WL("\n    Möchtest du etwas Anderes spielen?");
                Program.WL("    Ja / Nein", "DarkCyan");
                newGame = Console.ReadLine().Trim().ToLower();
                while (newGame != "ja" && newGame != "nein")
                {
                    Program.WL("\n    Ungültige Antwort! Bitte antworte mit <Ja> oder <Nein> !", "Red");
                    Program.W("    Möchtest du etwas Anderes spielen?    ");
                    newGame = Console.ReadLine().Trim().ToLower();
                    Program.ClearLines(3);
                }
                Console.Clear();
            }
            //Console.ReadLine();
        }
    }
}