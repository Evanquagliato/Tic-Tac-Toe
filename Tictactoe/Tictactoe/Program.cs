using System;


namespace Tictactoe
{
	class Program
	{

		static string[,] gridNumbers = // The main numbers for the tictactoe board
			{
				{"1","2","3"},
				{"4","5","6"},
				{"7","8","9"}
			};

		static void Main(string[] args)
		{
			bool victory = false; // Tracks if victory has been achieved
			bool turnOdd = true; // Tracks who's turn it is


			// Main game loop
			while(!victory)
			{
				Render(); // Renders the screen again after each turn
				TakeTurn(turnOdd); // Has the player take their turn, and calculates what they did
				turnOdd = !turnOdd; // Changes who's turn it is
				victory = VictoryChecker();
			}
			Console.WriteLine("Game over!");
			Console.Read();
		}


		// Renders the game board for play
		static void Render()
		{
			Console.Clear();
			string clearLines = "   |   |   "; // Holds the shape for the non-underscore dividers
			string underLines = "___|___|___"; // Holds the shape for the underscore dividers

			// Iterates down each row of the game board
			for (int i = 0; i < 3; i++) 
			{
				Console.WriteLine(clearLines);
				
				// Writes to each column of the game board
				for (int j = 0; j < 3; j++)
				{
					Console.Write($" {gridNumbers[i,j]} ");
					if (j != 2) // Adds dividers between numbers ONLY, avoids one at the end
					{
						Console.Write("|");
					}
				}

				// Adds the correct shape at the end of the row
				if (i != 2)
				{
					Console.WriteLine("\n" + underLines);
				}
				else if (i == 2)
				{
					Console.WriteLine("\n" + clearLines);
				}

			}
		}

		// Processes the player taking a turn
		static void TakeTurn(bool turnOdd)
		{
			string playerInput = ""; // Tracks player input
			string playerMark = "X"; // Player's marker for their turn
			bool continueGame = false; // Decides if its time to end the loop and move on to the next turn

			// The below ifs determine who's turn it is based on the boolean tracking if its an odd or even turn
			if (turnOdd == true)
			{
				Console.Write("Player 1: Choose your field! ");
				playerMark = "X";
			}
			else if (turnOdd == false)
			{
				Console.Write("Player 2: Choose your field! ");
				playerMark = "O";
			}


			// Asks for input and decides how to process the input
			while (!continueGame)
			{
				try
				{
					playerInput = Console.ReadLine(); // Gets input from the player
				}
				catch
				{
					Console.WriteLine("Please enter a value");
				}
				
				int loopCounter = 0; // Creates a new variable to track how many loops the below for loop block does
				// Ensure numbers only
				int playerInputInt = 0;
				try
				{
					// Kyle: Store this so we don't have to parse it twice. Also When we were parsing below, if it threw
					// an exception it wouldn't be caught but this solves that problem too
					playerInputInt = Int32.Parse(playerInput);
				}
				catch 
				{
					Console.WriteLine("Please use a number between 1 and 9");
					// Kyle: Skip the rest of the loop if we didn't get a valid number
					continue;
				}

				// Kyle: The above doens't actually check if the number is between 1 and 9 just that it is actaully a number
				if (playerInputInt < 1 || playerInputInt > 9) {
					Console.WriteLine("Please use a number between 1 and 9");
					continue;
				}

				// Determine where the value is and if it is available
				for (int i = 0; i < gridNumbers.GetLength(0); i++)
				{
					for (int j = 0; j < 3; j++)
					{
						loopCounter++; // increases the loop counter for each pass
						if (gridNumbers[i, j] == playerInput) // if what the player inputted matches, mark the square and continue to the next player
						{
							gridNumbers[i, j] = playerMark;
							continueGame = true;
						}
						else if (loopCounter == playerInputInt) // if what the player inputted is already used, print this and require a new entry
						{
							Console.Write("This box was already picked, please select again! ");
						}
					}
				}
			}
		}

		static bool VictoryChecker()
		{
			// Check for victory on rows
			for (int i = 0; i < gridNumbers.GetLength(0); i++)
			{
				if (gridNumbers[0,i] == gridNumbers[1,i] && gridNumbers[2,i] == gridNumbers[1, i])
				{
					// Kyle: if we know that there's a victory no point in checking the rest of the rows or columns so
					// we can just return from the whole function early
					return true;
				}
				
				// Check for victory on columms
				for (int j = 0; j < gridNumbers.GetLength(0); j++)
				{
					if (gridNumbers[j, 0] == gridNumbers[j, 1] && gridNumbers[j, 2] == gridNumbers[j, 1])
					{
						// See above
						return true;
					}
				}
			}

			// Checks for diagnoal victories
			if (gridNumbers[0, 0] == gridNumbers[1, 1] && gridNumbers[2, 2] == gridNumbers[1, 1])
			{
				return true;
			}
			if (gridNumbers[0, 2] == gridNumbers[1, 1] && gridNumbers[2, 0] == gridNumbers[1,1])
			{
				return true;
			}

			return false;
		}
	}
}