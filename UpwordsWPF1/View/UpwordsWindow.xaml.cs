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
using System.Windows.Shapes;
using UpwordsWPF1.ViewModel;
using UpwordsWPF1.Model;

namespace UpwordsWPF1.View
{
    /// <summary>
    /// Interaction logic for UpwordsWindow.xaml
    /// </summary>
    public partial class UpwordsWindow : Window
    {
        public static Label[,] MainBoardDisplayGridContents;
        public static Label[,] RackDisplayGridContents;
        public static Label[,] TileBagGridContents;

        public static Label[,] TileBagRacksContents;

        //public static Label[] TileBagHumanRackDisplayGridContents;
        //public static Label[] TileBagComputerRackDisplayGridContents;

        //public static Rectangle[,] MainBoardDisplayGridRectangles;
        //public static Rectangle[,] RackDisplayGridRectangles;
        //public static Rectangle[,] TileBagRectangles;

        public int RackSize = 10;

        Controller ViewModelInstance;

        string TileBagGoFirstOutcome;

        public static Brush notHighlighted = new SolidColorBrush(Colors.White);
        public static Brush Highlighted = new SolidColorBrush(Colors.Yellow);

        //int FirstPick = 0; // This is a fudge to force the first pick to always be equal

        public UpwordsWindow()
        {
            InitializeComponent();

            ViewModelInstance = FindResource("ViewModelKEY") as ViewModel.Controller;

            InitialiseGrid();
        }

        public void InitialiseGrid()
        {
            MainBoardDisplayGridContents = new Label[Controller.BoardHeight, Controller.BoardWidth];
            RackDisplayGridContents = new Label[1,RackSize];
            TileBagGridContents = new Label[Controller.BoardHeight, Controller.BoardWidth];
            TileBagRacksContents = new Label[2,Controller.MaxTiles];

            var thickness2 = new Thickness(1, 1, 1, 1);

            var blackbrush = new SolidColorBrush(Colors.Black);
            var lightgraybrush = new SolidColorBrush(Colors.LightGray);
            var whitebrush = new SolidColorBrush(Colors.White);
            var yellowbrush = new SolidColorBrush(Colors.Yellow);

            var fontbold = new FontWeight();
            fontbold = FontWeights.Bold;

            int temp;

            // Set up the labels for the main board display

            for (int row = 0; row < Controller.BoardHeight; row++)
            {
                for (int column = 0; column < Controller.BoardWidth; column++)
                {
                    MainBoardDisplayGridContents[row, column] = new Label();
                    MainBoardDisplayGridContents[row, column].Content = ".";
                    MainBoardDisplayGridContents[row, column].Background = notHighlighted;

                    MainBoardDisplayGridContents[row, column].BorderThickness = thickness2;
                    MainBoardDisplayGridContents[row, column].BorderBrush = blackbrush;
                    MainBoardDisplayGridContents[row, column].HorizontalContentAlignment = HorizontalAlignment.Center;
                    temp = (100 * row) + column;
                    MainBoardDisplayGridContents[row, column].Tag = temp.ToString();
                    MainBoardDisplayGridContents[row, column].FontSize = 14;

                    PuzzleGrid.Children.Add(MainBoardDisplayGridContents[row, column]);
                    Grid.SetColumn(MainBoardDisplayGridContents[row, column], column + 1);
                    Grid.SetRow(MainBoardDisplayGridContents[row, column], row + 1);
                }
            }

            // Set up the labels and highlight rectangles for the tile bag display

            for (int row = 0; row < Controller.BoardHeight; row++)
            {
                for (int column = 0; column < Controller.BoardWidth; column++)
                {
                    TileBagGridContents[row, column] = new Label();
                    TileBagGridContents[row, column].Content = "?";
                    TileBagGridContents[row, column].Background = notHighlighted;

                    TileBagGridContents[row, column].BorderThickness = thickness2;
                    TileBagGridContents[row, column].BorderBrush = blackbrush;
                    TileBagGridContents[row, column].HorizontalContentAlignment = HorizontalAlignment.Center;
                    temp = (100 * row) + column;
                    TileBagGridContents[row, column].Tag = temp.ToString();
                    TileBagGridContents[row, column].FontSize = 14;

                    TileBagGrid.Children.Add(TileBagGridContents[row, column]);
                    Grid.SetColumn(TileBagGridContents[row, column], column + 1);
                    Grid.SetRow(TileBagGridContents[row, column], row + 1);
                }
            }

            //Set up the labels for the tile rack displayed on the Board Tab

            //for (int row = 0; row<1; row++) // Only do it for row=0
            //{
            for (int column = 0; column < RackSize; column++)
            {
                RackDisplayGridContents[0, column] = new Label();

                switch (column)
                {
                    case 0: RackDisplayGridContents[0, column].Content = "G"; break;
                    case 1: RackDisplayGridContents[0, column].Content = "C"; break;
                    case 2: RackDisplayGridContents[0, column].Content = "E"; break;
                    case 3: RackDisplayGridContents[0, column].Content = "P"; break;
                    case 4: RackDisplayGridContents[0, column].Content = "Qu"; break;
                    case 5: RackDisplayGridContents[0, column].Content = "T"; break;
                    case 6: RackDisplayGridContents[0, column].Content = "W"; break;
                    case 7: RackDisplayGridContents[0, column].Content = "-"; break;
                    case 8: RackDisplayGridContents[0, column].Content = "-"; break;
                    case 9: RackDisplayGridContents[0, column].Content = "-"; break;
                    default:
                        break;
                }

                RackDisplayGridContents[0, column].Background = notHighlighted;
                RackDisplayGridContents[0, column].BorderThickness = thickness2;
                RackDisplayGridContents[0, column].BorderBrush = blackbrush;
                RackDisplayGridContents[0, column].HorizontalContentAlignment = HorizontalAlignment.Center;
                temp = (100 * 0) + column;
                RackDisplayGridContents[0, column].Tag = temp.ToString();
                RackDisplayGridContents[0, column].FontSize = 14;
                RackDisplayGridContents[0, column].MinWidth = 30;

                //BoardRack.Children.Add(RackDisplayGridContents[0, column]);
            }

            //Create the labels for the tiles on the racks on the Tile Bag tab

            for (int row = 0; row < 2; row++)
            {
                for (int column = 0; column < Controller.MaxTiles; column++)
                {
                    TileBagRacksContents[row, column] = new Label();
                    TileBagRacksContents[row, column].Content = "-";
                    TileBagRacksContents[row, column].Background = notHighlighted;
                    TileBagRacksContents[row, column].MinWidth = 30;

                    TileBagRacksContents[row, column].BorderThickness = thickness2;
                    TileBagRacksContents[row, column].BorderBrush = blackbrush;
                    TileBagRacksContents[row, column].HorizontalContentAlignment = HorizontalAlignment.Center;
                    temp = 100 * row + column;
                    TileBagRacksContents[row, column].Tag = temp.ToString();
                    TileBagRacksContents[row, column].FontSize = 14;

                    if (row==0) Player1Rack.Children.Add(TileBagRacksContents[row, column]);
                    if (row==1) Player2Rack.Children.Add(TileBagRacksContents[row, column]);
                }
            }



            ViewModelInstance.Setup();

            TileBagGoFirstOutcome = "PickIncomplete";
        }

        // This gets used when the human is planning his/her move
        private void SelectThinking(object sender, RoutedEventArgs e)
        {

            //Thinking.Content = "Selected";
            //Play.Content = "Thinking Clicked";
        }

        // This gets used when the human is planning his/her move
        private void SelectPlay(object sender, RoutedEventArgs e)
        {

            //Thinking.Content = "Play Clicked";
            //Play.Content = "Selected";
        }

        private void OnMainBoardPressed(object sender, RoutedEventArgs e) //Mainboard
        {
            if (Play.IsChecked != true) return; // Can't highlight squares on the board if not in 'Play' mode

            // Check that we can find the pressed square and return if not
            int coordinates;
            string content = (string)((Label)e.Source).Tag;
            if (!int.TryParse(content, out coordinates)) return;

            // Check there is exactly one tile highlighted on the rack and return if not
            int? firstTileHighlighted, secondTileHighlighted;
            if (CountHighlightsOnRack(out firstTileHighlighted, out secondTileHighlighted) != 1) return;

            // Put the tile on the board
            int row, column;
            row = coordinates / 100;
            column = coordinates - 100 * row;
            MainBoardDisplayGridContents[row, column].Content = RackDisplayGridContents[0, (int)firstTileHighlighted].Content;

            //Remove the tile from the rack and unhighlight it
            RackDisplayGridContents[0, (int)firstTileHighlighted].Content = "-";
            RackDisplayGridContents[0, (int)firstTileHighlighted].Background = notHighlighted;

            //int? rowHighlighted, columnHighlighted;
            //int boardHighlightedCount = CountHighlightsOnBoard(out rowHighlighted, out columnHighlighted);

            //// Get the coordinates of the pressed square
            //int  row, column;
            //row = coordinates / 100;
            //column = coordinates - 100 * row;

            //// Further actions depend upon the number of squares already highlighted
            //if (boardHighlightedCount == 0) // If none are already highlighted, then we'll highlight the square
            //{
            //    MainBoardDisplayGridContents[row, column].Background = Highlighted;
            //}

            //else if (boardHighlightedCount == 1) // If one is already highlighted......
            //{
            //    if ()
            //}


            //int coordinates, row, column;
            //string content = (string)((Label)e.Source).Tag;
            //if (int.TryParse(content, out coordinates))
            //{
            //    row = coordinates / 100;
            //    column = coordinates - 100 * row;
            //    if (MainBoardDisplayGridContents[row, column].Background == Highlighted)
            //    {
            //        MainBoardDisplayGridContents[row, column].Background = notHighlighted;
            //    }
            //    else
            //    {
            //        MainBoardDisplayGridContents[row, column].Background = Highlighted;
            //    }
            //}
        }

        private void OnBoardRackPressed(object sender, RoutedEventArgs e) //Rack on mainboard
        {
            int coordinates, row, column, rackHighlightedCount, firstHighlighted, secondHighlighted;
            string content = (string)((Label)e.Source).Tag;
            if (int.TryParse(content, out coordinates))
            {
                row = coordinates / 100;
                column = coordinates - 100 * row;
                if (RackDisplayGridContents[row, column].Background == Highlighted)
                {
                    RackDisplayGridContents[row, column].Background = notHighlighted;
                }
                else
                {
                    RackDisplayGridContents[row, column].Background = Highlighted;
                }

                rackHighlightedCount = 0;
                firstHighlighted = 0; secondHighlighted = 0;
                for (int i = 0; i < RackSize; i++)
                {
                    if (RackDisplayGridContents[row, i].Background == Highlighted)
                    {
                        rackHighlightedCount++;
                        if (rackHighlightedCount == 1) firstHighlighted = i;
                        if (rackHighlightedCount == 2) secondHighlighted = i;
                    }
                }
                if (rackHighlightedCount==2)
                {
                    object temp = RackDisplayGridContents[row, firstHighlighted].Content;
                    RackDisplayGridContents[row, firstHighlighted].Content = RackDisplayGridContents[row, secondHighlighted].Content;
                    RackDisplayGridContents[row, secondHighlighted].Content = temp;
                }
                if (rackHighlightedCount >= 2)
                {
                    for (int i = 0; i < RackSize; i++)
                    {
                        RackDisplayGridContents[row, i].Background = notHighlighted;
                    }
                }

            }
        }


        private void ClearHighlightsOnBoard()
        {
            for (int row = 0; row < Controller.BoardHeight; row++)
            {
                for (int column = 0; column < Controller.BoardWidth; column++)
                {
                    MainBoardDisplayGridContents[row, column].Background = notHighlighted;
                }
            }
        }

        private int CountHighlightsOnBoard(out int? rowHighlighted, out int? columnHighlighted)
        {
            int highlightCount = 0;
            rowHighlighted = null;
            columnHighlighted = null;

            for (int row = 0; row < Controller.BoardHeight; row++)
            {
                for (int column = 0; column < Controller.BoardWidth; column++)
                {
                    if (MainBoardDisplayGridContents[row, column].Background == Highlighted)
                    {
                        highlightCount++;
                        rowHighlighted = row;
                        columnHighlighted = column;
                    }
                }
            }
            return highlightCount;
        }

        private void ClearHighlightsOnRack()
        {
            int playerNumber = 0;
            for (int column = 0; column < RackSize; column++)
            {
                RackDisplayGridContents[playerNumber, column].Background = notHighlighted;
            }
        }

        private int CountHighlightsOnRack(out int? tile1Highlighted, out int? tile2Highlighted)
        {
            int playerNumber = 0;
            int highlightCount = 0;
            tile1Highlighted = null;
            tile2Highlighted = null;

            for (int column = 0; column < RackSize; column++)
            {
                if (RackDisplayGridContents[playerNumber, column].Background == Highlighted)
                {
                    highlightCount++;
                    if (highlightCount == 1) tile1Highlighted = column;
                    if (highlightCount == 2) tile2Highlighted = column;
                }
            }
            return highlightCount;
        }

        private void TileBagPressed(object sender, RoutedEventArgs e)
        {
            int coordinates, row, column;
            string s0, s1, s2;
            Tile pickedTile;

            if (!(e.Source is Label)) return; // Sometimes the click will hit the underlying grid rather than the labels on it
            if (T4_Continue.IsEnabled) return; // You can only draw tiles if "Continue" is not enabled

            string content = (string)((Label)e.Source).Tag;
            if (int.TryParse(content, out coordinates))
            {
                row = coordinates / 100;
                column = coordinates - 100 * row;
                s0 = (string)TileBagGridContents[row, column].Content;
                if (s0 != "?") return; //Can't pick a tile that isn't there (indicated by a "?")

                TileBagGridContents[row, column].Content = "";
                pickedTile = ViewModelInstance.TheBag.GetTile();

                //if (FirstPick < 2)
                //{ pickedTile = "A"; FirstPick++; } // A little fudge to force an equal pick first time through                

                if (GoFirst.IsChecked == true)
                {
                    s1 = (string)TileBagRacksContents[0,0].Content;
                    s2 = (string)TileBagRacksContents[1,0].Content;

                    if (TileBagGoFirstOutcome == "PickIncomplete")
                    {
                        if (s1 == "-") //First letter picked, just drop it into the first rack
                        {
                            TileBagRacksContents[0,0].Content = pickedTile.Face;
                            pickedTile.Location = Locations.Player1Rack;
                            return;
                        }
                        if (s2 == "-") //Second letter picked, now we have to compare and decide
                        {
                            TileBagRacksContents[1,0].Content = pickedTile.Face;
                            pickedTile.Location = Locations.Player2Rack;

                            s2 = (string)TileBagRacksContents[1,0].Content;

                            int comparisonResult = String.Compare(s1, s2, StringComparison.Ordinal);
                            if (comparisonResult > 0)
                            {
                                TileBagGoFirstOutcome = "ComputerGoesFirst";
                                OutcomeLabel.Content = "Computer (Player 2) goes first - press Continue to begin the game";
                                OutcomeLabel.Visibility = Visibility.Visible;
                                ViewModelInstance.processUIAction("T4GoFirstComputer");
                                return;
                            }
                            else if (comparisonResult < 0)
                            {
                                TileBagGoFirstOutcome = "HumanGoesFirst";
                                OutcomeLabel.Content = "Human (Player 1) goes first - press Continue to begin the game";
                                OutcomeLabel.Visibility = Visibility.Visible;
                                ViewModelInstance.processUIAction("T4GoFirstHuman");
                                return;
                            }

                            // If we drop through to here, the two letters are the same

                            // TileBagGoFirstOutcome remains "PickIncomplete"
                            OutcomeLabel.Content = "The two letters are the same - press Continue to try again";
                            OutcomeLabel.Visibility = Visibility.Visible;
                            ViewModelInstance.processUIAction("T4GoFirstTryAgain");

                            return;
                        }

                        // If we get to here, s1 and s2 are both not "-", so something has gone wrong

                        throw new NotImplementedException();
                    }
                }
                else if (HumanTiles.IsChecked == true)
                {
                    int firstEmpty = -1;
                    for(int i=0; i<Controller.MaxTiles; i++)
                    {
                        if ((string)TileBagRacksContents[0, i].Content == "-")
                        { firstEmpty = i; break; }
                    }

                    if (firstEmpty == -1) throw new NotImplementedException();

                    TileBagRacksContents[0,firstEmpty].Content = pickedTile.Face;
                    pickedTile.Location = Locations.Player1Rack;

                    if (firstEmpty < 6) return;
                    OutcomeLabel.Content = "Human (Player 1) has enough tiles - press Continue";
                    OutcomeLabel.Visibility = Visibility.Visible;
                    ViewModelInstance.processUIAction("HumanRackFilled");
                }
                else if (ComputerTiles.IsChecked == true)
                {
                    int firstEmpty = -1;
                    for (int i = 0; i < Controller.MaxTiles; i++)
                    {
                        if ((string)TileBagRacksContents[1, i].Content == "-")
                        { firstEmpty = i; break; }
                    }
                    if (firstEmpty == -1) throw new NotImplementedException();

                    TileBagRacksContents[1,firstEmpty].Content = pickedTile.Face;
                    pickedTile.Location = Locations.Player2Rack;

                    if (firstEmpty < 6) return;
                    OutcomeLabel.Content = "Computer (Player 2) has enough tiles - press Continue";
                    OutcomeLabel.Visibility = Visibility.Visible;
                    ViewModelInstance.processUIAction("ComputerRackFilled");
                }
            }
        }


        private void Tab1IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var thisTab = (TabItem)sender;
            if (thisTab.IsEnabled == true)
            { return; }
            else
            {   if (ViewModelInstance != null)
                { EnableTab(ViewModelInstance.processDisabledTab(1)); }
            }
        }

        private void Tab2IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var thisTab = (TabItem)sender;
            if (thisTab.IsEnabled == true)
            { return; }
            else
            {
                if (ViewModelInstance != null)
                { EnableTab(ViewModelInstance.processDisabledTab(2)); }
            }
        }

        private void Tab3IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var thisTab = (TabItem)sender;
            if (thisTab.IsEnabled == true)
            { return; }
            else
            {
                if (ViewModelInstance != null)
                { EnableTab(ViewModelInstance.processDisabledTab(3)); }
            }
        }

        private void Tab4IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var thisTab = (TabItem)sender;
            if (thisTab.IsEnabled == true)
            { return; }
            else
            {
                if (ViewModelInstance != null)
                { EnableTab(ViewModelInstance.processDisabledTab(4)); }
            }
        }

        private void Tab5IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var thisTab = (TabItem)sender;
            if (thisTab.IsEnabled == true)
            { return; }
            else
            {
                if (ViewModelInstance != null)
                { EnableTab(ViewModelInstance.processDisabledTab(5)); }
            }
        }

        private void EnableTab (int tabNumber)
        {
            switch (tabNumber)
            {           
                case 1: T1.IsSelected = true; break;
                case 2: T2.IsSelected = true; break;
                case 3: T3.IsSelected = true; break;
                case 4: T4.IsSelected = true; break;
                case 5: T5.IsSelected = true; break;
                default: throw new NotImplementedException();
            }
        }

        private void T1Continue(object sender, RoutedEventArgs e)
        {
            ViewModelInstance.processUIAction("T1Continue");
        }

        private void T2Continue(object sender, RoutedEventArgs e)
        {
            ViewModelInstance.processUIAction("T2Continue");
        }

        private void T3Continue(object sender, RoutedEventArgs e)
        {
            ViewModelInstance.processUIAction("T3Continue");
        }

        private void T4Continue(object sender, RoutedEventArgs e)
        {
            switch (TileBagGoFirstOutcome)
            {   case "PickIncomplete":
                    ViewModelInstance.processUIAction("T4GoFirstTryAgain");

                    TileBagRacksContents[0,0].Content = "-";
                    TileBagRacksContents[1,0].Content = "-";
                    OutcomeLabel.Visibility = Visibility.Hidden;

                    for (int r = 0; r < Controller.BoardHeight; r++)
                    {
                        for (int c = 0; c < Controller.BoardWidth; c++)
                        {
                            TileBagGridContents[r, c].Content = "?";
                        }
                    }
                    return;
                case "ComputerGoesFirst":
                    ViewModelInstance.processUIAction("T4ComputerFirstRack");
                    TileBagGoFirstOutcome = "Completed";
                    break;
                case "HumanGoesFirst":
                    ViewModelInstance.processUIAction("T4HumanFirstRack");
                    TileBagGoFirstOutcome = "Completed";
                    break;
                case "Completed":
                    ViewModelInstance.processUIAction("T4Continue");
                    OutcomeLabel.Visibility = Visibility.Hidden;
                    return;
                default: break;
            }
            TileBagRacksContents[0,0].Content = "-";
            TileBagRacksContents[1,0].Content = "-";
            OutcomeLabel.Visibility = Visibility.Hidden;

            for (int r = 0; r < Controller.BoardHeight; r++)
            {
                for (int c = 0; c < Controller.BoardWidth; c++)
                {
                    TileBagGridContents[r, c].Content = "?";
                }
            }
        }

        //private void BoardRack0MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    var thisButton = (Button)sender;
        //    if (thisButton.Background == Brushes.White)
        //    {
        //        thisButton.Background = Brushes.LightGray;
        //    }
        //    else
        //    {
        //        thisButton.Background = Brushes.White;
        //    }
        //}

        //private void BoardRack1MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    var thisButton = (Button)sender;
        //    if(thisButton.Background == Brushes.White)
        //    {
        //        thisButton.Background = Brushes.LightGray;
        //    }
        //    else
        //    {
        //        thisButton.Background = Brushes.White;
        //    }
        //}

        //private void BoardRack2MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack3MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack4MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack5MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack6MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack7MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack8MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack9MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void BoardRack0Click(object sender, RoutedEventArgs e)
        //{
        //    var thisButton = (Button)sender;
        //    if (thisButton.Background == Brushes.White)
        //    {
        //        thisButton.Background = Brushes.LightGray;
        //    }
        //    else
        //    {
        //        thisButton.Background = Brushes.White;
        //    }
        //}



        //private void SCBAction(object sender, RoutedEventArgs e)
        //{

        //    SCB.IsEnabled = false;

        //    Controller.TheController.runSCB();

        //    for (int row = 0; row < 15; row++)
        //    {
        //        for (int column = 0; column < 15; column++)
        //        {
        //            MainBoardDisplayGridContents[row, column].Content = Controller.MainBoardContents[row, column];

        //        }
        //    }
        //}

        //private void ToggleBlack(object sender, RoutedEventArgs e)
        //{
        //    if ((String)ShowBlack.Content == "Show Black")
        //    {
        //        ShowBlack.Content = "Hide Black";
        //        for (int row = 0; row < 15; row++)
        //        {
        //            for (int column = 0; column < 15; column++)
        //            {
        //                if ((String)MainBoardDisplayGridContents[row, column].Content == "X")
        //                {
        //                    MainBoardDisplayGridRectangles[row, column].Opacity = 0.9;
        //                }
        //                else
        //                {
        //                    MainBoardDisplayGridRectangles[row, column].Opacity = 0;
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        ShowBlack.Content = "Show Black";
        //        for (int row = 0; row < 15; row++)
        //        {
        //            for (int column = 0; column < 15; column++)
        //            {
        //                MainBoardDisplayGridRectangles[row, column].Opacity = 0;
        //            }
        //        }

        //    }

        //}


    }
}
