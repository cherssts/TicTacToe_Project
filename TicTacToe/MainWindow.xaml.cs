using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        /// <summary>
        /// Holds the current rsults of cels in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player1's turn(X) or Player2's turn(O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
            
            
        }


        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            //Create a new blank arrat of free cells
            mResults = new MarkType[9];


            for(var i =0; i< mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;

                //Make sure Player 1 Starts the game
                mPlayer1Turn = true;

                //interrate every button on the grid (Board)
                Board.Children.Cast<Button>().ToList().ForEach(Button => {
                    Button.Content = string.Empty;

                    // Change Background, foreground and content to default values.
                    Button.Background = Brushes.White;
                    Button.Foreground = Brushes.Blue;
                });

                //Make sure the game hasn't finished
                mGameEnded = false;
            }
        }


        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"> The button that was clicked</param>
        /// <param name="e"> The events of the clicks</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Starts a new gmae on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button) sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row*3);

            // Don't do anything if the cell is  already has a value in it!
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            //Set the cell value based on which players turn it is
            #region One Way to Check the player's turn
            if (mPlayer1Turn)
            {
                mResults[index] = MarkType.Cross;
            }
            else
            {
                mResults[index] = MarkType.Nought;
            }
            #endregion

            #region Compact way to write Code
            //Very Compact way to write code, Actually not recommended.

            //mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;


            //Set button text to the result

            //button.Content = mPlayer1Turn ? "X" : "O";
            #endregion


            #region If Way

            if (mPlayer1Turn)
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O";
            }
            #endregion


            //Change Noughts to green
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Toggle the PLayer's turns
            //if (mPlayer1Turn)
            //mPlayer1Turn= false;
            //else
            //mPlayer1Turn= true;

            //This is a very simple way to toggle boolean values
            mPlayer1Turn ^= true;

            //Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Check for a winner  with the condition 3 Croos or Nought in a line
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckForWinner()
        {
            #region Horizontal wins
            //Check for horizontal wins
            //
            //-Row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0]& mResults[1]&mResults[2])==mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //
            //-Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            //-Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical wins
            //Check for Vertical wins
            //
            //-Column 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            //-Column 1
            //
            if (mResults[0] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            //-Column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal wins
            // Check for Diagonal wins
            //
            //-Top Left Bottom Right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //
            //-Top Right Bottom Left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game Ends
                mGameEnded = true;

                //highlight the winning cells in Green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No winners
            //Check for no winner and full board
            if (!mResults.Any(fs=> fs == MarkType.Free))
            {
                //Game Ended
                mGameEnded=true;

                //Turn all cells Orange
                Board.Children.Cast<Button>().ToList().ForEach(Button => {
                    Button.Background = Brushes.Orange;
                });

            }

            #endregion
        }
    }
}
