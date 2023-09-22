using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Security;
using static System.Net.Mime.MediaTypeNames;
using QueryBuilderP;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilderP
{
    internal class Program
    {
        /// DATA PATHS
        private static string ProjectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
        private static string PokemonFilePath = @$"{ProjectFolder}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}AllPokemon.csv";
        private static string BannedFilePath = @$"{ProjectFolder}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}BannedGames.csv";
        private static string DBPath = @$"{ProjectFolder}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";

        /// Essentially a second copy of the Pokemon table and BannedGame table in the databases. Needed for the ID assigned to object to autoincrement, be found, and be deleted correctly.
        private static List<Pokemon> DBPokemons = new List<Pokemon>();
        private static List<BannedGame> DBBannedGames = new List<BannedGame>();
        private static List<Pokemon> Pokemons = new List<Pokemon>();
        private static List<BannedGame> BannedGames = new List<BannedGame>();

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\t\t\t\t\t\t\t QUERY BUILDER DEMO   \t\t\t\t\t\t\n");

            /// Creating the instance of the class and passing the database path for the connection.
            QueryBuilder QBClass = new QueryBuilder(DBPath);

            /// Reading the data in the Pokemon table in order to get the last id before creating new objects. Therefore, the Id will increment correctly.
            DBPokemons = QBClass.ReadAll<Pokemon>();

            /// Reading the data in the BannedGame table in order to get the last Id before creating new objects. Therefore, the Id will increment correctly.
            DBBannedGames = QBClass.ReadAll<BannedGame>();

            /// ADDING POKEMON CSV OBJECT OT POKEMON LIST
            try
            {
                using (var sr = new StreamReader($@"{PokemonFilePath}"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] inputs = line.Split(',');

                        Pokemons.Add(new Pokemon(int.Parse(inputs[0]), inputs[1], inputs[2], inputs[3], inputs[4], int.Parse(inputs[5]), int.Parse(inputs[6]),
                            int.Parse(inputs[7]), int.Parse(inputs[8]), int.Parse(inputs[9]), int.Parse(inputs[10]), int.Parse(inputs[11]), int.Parse(inputs[12])));

                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR OPENING FILE: " + e.Message);
            }

            /// ADDING BANNED GAMES CSV OBJECT TO BANNEDGAMES LIST
            try
            {
                using (var sr = new StreamReader($@"{BannedFilePath}"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] inputs = line.Split(',');

                        BannedGames.Add(new BannedGame(inputs[0], inputs[1], inputs[2], inputs[3]));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR OPENING FILE: " + e.Message);
            }

            /// Introduction
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\nCOMPUTER: CURRENTLY, THE FIRST ID IS \"" + DBPokemons[0].Id + "\" AND THE LAST ID IN THE DATABASE IS \"" + DBPokemons.Last().Id + "\" FOR THE \"Pokemon\" TABLE. \nTHE FOLLWOWING " +
                "ARE THE CURRENT POKEMONS IN THE \"Pokemon\" TABLE IN THE DATABASE.");
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();
            foreach (Pokemon pokemon in DBPokemons)
            {
                Console.WriteLine(pokemon);
            }
            Console.WriteLine("\nCount of \"Pokemons\" in the \"Pokemon\" table: " + DBPokemons.Count);

            Console.WriteLine("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();

            Console.WriteLine("\n\nCOMPUTER: CURRENTLY, THE FIRST ID IS \"" + DBBannedGames[0].Id + "\" AND THE LAST ID IN THE DATABASE IS \"" + DBBannedGames.Last().Id + "\" FOR THE \"BannedGame\" TABLE. \nTHE FOLLWOWING " +
                "ARE THE CURRENT \"BANNED GAMES\" IN THE \"BannedGame\" TABLE IN THE DATABASE.");
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();
            foreach (BannedGame game in DBBannedGames)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine("\nCount of \"Banned Games\" in the \"BannedGame\" table: " + DBBannedGames.Count);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("NOTE: PARTS OF THE TABLE LIKE SHOWN ABOVE WILL NOT BE DISPLAYED ANYMORE FOR THE \"UPDATES\" TO THE DATABASE SHOW UP IN THE CONSOLE AFTER IT IS CLOSED.");
            Console.ForegroundColor = ConsoleColor.Gray;

            /// 6.a. ERASING ALL RECORDS FROM A OBJECT TYPE'S TABLE ////////////////////////////////////////////////////////////////////////////////////////////// 
            Console.WriteLine("\nCOMPUTER: I will now delete the \"Pokemon\" at \"Id: "+ DBPokemons.Last().Id +"\" from the \"Pokemon\" table in the database.");
            var delete = DBPokemons.Where(p => p.Id == DBPokemons.Last().Id);
            foreach (var p in delete)
            {
                QBClass.Delete(p);
            }
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("\nSUCCESS.");

            /// 6.b. WRITING ALL OBJECTS IN A COLLECTION TO THE DATABASE ////////////////////////////////////////////////////////////////////////////////////////////// 
            // POKEMON TABLE
            Console.WriteLine("\nCOMPUTER: I will now pick a collection of \"Pokemons\" from \"AllPokemon.csv\" where \"Type2: 1045\". I will write it to the database.");
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();

            var addPokemons = Pokemons.Where(p => p.Type2 == "Grass");
            foreach (var p in addPokemons) 
            {
                QBClass.Create(p);
            }

            Console.Clear();
            Console.WriteLine("\nSUCCESS.");

            // BANNEDGAME TABLE
            Console.WriteLine("\nCOMPUTER: I will now pick a collection of \"Banned Games\" from \"BannedGames.csv\" where \"Series: Soldier of Fortune\" and write it to the database.");
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();

            var addBannedGames = BannedGames.Where(game => game.Series == "Soldier of Fortune");
            foreach (var game in addBannedGames)
            {
                QBClass.Create(game);
            }

            Console.Clear();
            Console.WriteLine("\nSUCCESS.");

            /// 6.c. WRTITING A SINGLE OBJECT TO THE DATABASE ////////////////////////////////////////////////////////////////////////////////////////////// 
            // AllPokemon csv file
            Console.WriteLine("\nCOMPUTER: I will now pick a \"Pokemon\" from \"AllPokemon.csv\" with \"DexNumber: 890\" and write it to the database.");
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();

            var addPokemon = Pokemons.Where(p => p.DexNumber == 890);
            foreach (var p in addPokemon)
            {
                QBClass.Create(p);
            }

            Console.Clear();
            Console.WriteLine("\nSUCCESS.");

            // BannedGames csv file
            Console.WriteLine("\nCOMPUTER: I will now pick a \"Banned Game\" from \"BannedGames.csv\" with \"Series: Syndicate\" and write it to the database.");
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.ReadLine();

            var addBannedGame = BannedGames.Where(game => game.Series == "Syndicate");
            foreach (var game in addBannedGame)
            {
                QBClass.Create(game);
            }

            Console.Clear();
            Console.WriteLine("\nSUCCESS.");

            // Ending
            Console.Write("\nPRESS [ENTER] TO CONTINUE.");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Write("\nCOMPUTER: GOODBYE.");
        }
    }
}

