using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UpwordsWPF1.Model;
//using UpwordsWPF1.View;


namespace UpwordsWPF1.ViewModel
{
    using UpwordsWPF1.View;
    using System.Windows.Controls;

    public enum GameStates
    {
        //T1 is Game Setup Tab
        T1_InProgress, // Was 10

        // T2 is Score Tab
        T2_ScorePlayer1, // Was 80 (Human)
        T2_ScorePlayer2, // Was 50 (Computer)

        //T3 is Board Tab
        T3_BoardPlayer2Thinking, // Was 30 (Computer)
        T3_BoardPlayer2MoveDecided, // Was 32 (Computer)
        T3_BoardPlayer2MoveShown, // Was 40 (Computer)

        T3_BoardPlayer1Thinking, // Was 70 (Human)
        T3_BoardPlayer1MoveDecided, // Was 71 (Human)
        T3_BoardPlayer1MoveShown, // Was 72 (Human)

        //T4 is Tile Bag Tab
        T4_GoFirstPickIncomplete, // Was 20
        T4_GoFirstTryAgain, // Was 22
        T4_GoFirstPlayer1, // Was 24 (Human)
        T4_GoFirstPlayer2, // Was 26 (Computer)
        T4_BeginGamePlayer1PickingTiles, // Was 58 (Human)
        T4_BeginGamePlayer1PickedTiles, // Was 59 (Human)
        T4_BeginGamePlayer2PickingTiles, // Was 88 (Computer)
        T4_BeginGamePlayer2PickedTiles, // Was 89 (Computer)
        T4_Player1PickingTiles, // was 90 (Human)
        T4_Player1PickedTiles, // Was 92 (Human)
        T4_Player2PickingTiles, // Was 60 (Computer)
        T4_Player2PickedTiles, // Was 62 (Computer)
    }

    class Controller : INotifyPropertyChanged
    {            
        //public event PropertyChangedEventHandler PropertyChanged;

        public static int BoardHeight = 10;
        public static int BoardWidth = 10;
        public static int MaxTiles = 7; //Maximum number of tiles a player may hold (NB also baked into the XAML)
        private GameStates gameState = GameStates.T1_InProgress;
        public TileBag TheBag;

        //public static string[,] MainBoardContents;

        //static BoardModel MainBoard;

        private List<Label> testRack2 = new List<Label>();
        private List<string> testRack3 = new List<string>();

        //Constructor
        public Controller()
        {
            TheBag = new TileBag();
        }

        public GameStates GameState

        {   get {return gameState;}
            set {gameState = value;}
        }

        public List<Label> TestRack2
        {
            get
            {
                return testRack2;
            }

            set
            {
                testRack2 = value;
            }
        }

        public List<string> TestRack3
        {
            get
            {
                return testRack3;
            }

            set
            {
                testRack3 = value;
            }
        }

        public int processDisabledTab(int TabNumber)
        {
            switch (GameState)
            {
                case GameStates.T1_InProgress: return 1; // Game Setup
                case GameStates.T4_GoFirstPickIncomplete: return 4; // Tile Bag
                case GameStates.T4_GoFirstTryAgain: return 4; // Tile Bag
                case GameStates.T4_GoFirstPlayer1: return 4; // Tile Bag
                case GameStates.T4_GoFirstPlayer2: return 4; // Tile Bag
                case GameStates.T3_BoardPlayer2Thinking: return 3; // Board
                case GameStates.T3_BoardPlayer2MoveDecided: return 3; // Board
                case GameStates.T3_BoardPlayer2MoveShown: return 3; // Board
                case GameStates.T2_ScorePlayer2: return 2; // Score
                case GameStates.T4_BeginGamePlayer1PickingTiles: return 4; // Tile Bag (tiles for human at the start of the game)
                case GameStates.T4_BeginGamePlayer1PickedTiles: return 4; // Tile Bag (tiles for human at the start of the game - continue enabled)
                case GameStates.T4_Player2PickingTiles: return 4; // Tile Bag (tiles for computer)
                case GameStates.T4_Player2PickedTiles: return 4; // Tile Bag (tiles for computer - continue enabled)
                case GameStates.T3_BoardPlayer1Thinking: return 3; // Board
                case GameStates.T3_BoardPlayer1MoveDecided: return 2; // Score
                case GameStates.T3_BoardPlayer1MoveShown: return 2; // Score
                case GameStates.T2_ScorePlayer1: return 2; // Score
                case GameStates.T4_BeginGamePlayer2PickingTiles: return 4; // Tile Bag (tiles for computer at the start of the game)
                case GameStates.T4_BeginGamePlayer2PickedTiles: return 4; // Tile Bag (tiles for computer at the start of the game - continue enabled)
                case GameStates.T4_Player1PickingTiles: return 4; // Tile Bag (tiles for human)
                case GameStates.T4_Player1PickedTiles: return 4; // Tile Bag (tiles for human - continue enabled)
                default: throw new NotImplementedException();
            }
        }

        public void processUIAction(string Requestor)
        {
            // These actions are taking place as we leave the state being switched on

            switch (GameState)
            {
                // GameState T1_InProgress - the intial Setup Tab (T1)
                case GameStates.T1_InProgress: 
                    TheBag.FillBag();
                    TheBag.ShakeBag(null); // 1 makes Computer go first ; 2 makes Human go first (by coincidence of the sequences)
                    GameState = GameStates.T4_GoFirstPickIncomplete; break;

                // GameState T4_GoFirstPickIncomplete - the TileBag Tab (T4) to decide who goes first

                case GameStates.T4_GoFirstPickIncomplete: 
                    switch (Requestor)
                    {
                        case "T4GoFirstTryAgain": GameState = GameStates.T4_GoFirstTryAgain; break;
                        case "T4GoFirstComputer": GameState = GameStates.T4_GoFirstPlayer1; break;
                        case "T4GoFirstHuman": GameState = GameStates.T4_GoFirstPlayer2; break;

                        default: break; // Could throw an exception here
                    }
                    // Could throw an exception here
                    break;

                //Gamestate T4_GoFirstTryAgain - T4 Continue enabled (must try again to determine who goes first) - when pressed go back to Gamestate 20 

                case GameStates.T4_GoFirstTryAgain: 
                    GameState = GameStates.T4_GoFirstPickIncomplete; break;

                // GameState T4_GoFirstPlayer1 - T4 Continue enabled (Computer goes first) - when pressed move on to GameState 88. 
                // Get tiles for Computer (88), enable T4 Continue (89), get tiles for Human (90), enable T4 Continue (92), then move on to Computer's move (30).

                case GameStates.T4_GoFirstPlayer1:
                    TheBag.FillBag();
                    TheBag.ShakeBag(null);
                    GameState = GameStates.T4_BeginGamePlayer2PickingTiles; break;

                // GaneState T4_GoFirstPlayer2 - T4 Continue enabled (Human goes first) - when pressed move on to GameState 58.
                // Get tiles for Human (58), enable T4 Continue (59), get tiles for Computer (60), enable T4 Continue (62), then move on to Human's move (70)

                case GameStates.T4_GoFirstPlayer2:
                    TheBag.FillBag();
                    TheBag.ShakeBag(null);
                    GameState = GameStates.T4_BeginGamePlayer1PickingTiles; break;

                case GameStates.T3_BoardPlayer2Thinking: // Actually goes via 32
                    GameState = GameStates.T3_BoardPlayer2MoveShown; break;

                case GameStates.T3_BoardPlayer2MoveShown:
                    GameState = GameStates.T2_ScorePlayer2; break;

                case GameStates.T2_ScorePlayer2:
                    GameState = GameStates.T4_Player2PickingTiles; break;

                case GameStates.T4_BeginGamePlayer1PickingTiles:
                    GameState = GameStates.T4_BeginGamePlayer1PickedTiles; break;

                case GameStates.T4_BeginGamePlayer1PickedTiles:
                    GameState = GameStates.T4_Player2PickingTiles; break;

                case GameStates.T4_Player2PickingTiles:
                    GameState = GameStates.T4_Player2PickedTiles; break;

                case GameStates.T4_Player2PickedTiles:
                    GameState = GameStates.T3_BoardPlayer1Thinking; break;

                case GameStates.T3_BoardPlayer1Thinking: // Goes via 71 and 72 and may go back to 70
                    GameState = GameStates.T2_ScorePlayer1; break;

                case GameStates.T2_ScorePlayer1:
                    GameState = GameStates.T4_Player1PickingTiles; break;

                case GameStates.T4_BeginGamePlayer2PickingTiles:
                    GameState = GameStates.T4_BeginGamePlayer2PickedTiles; break;

                case GameStates.T4_BeginGamePlayer2PickedTiles:
                    GameState = GameStates.T4_Player1PickingTiles; break;

                case GameStates.T4_Player1PickingTiles:
                    GameState = GameStates.T4_Player1PickedTiles; break;

                case GameStates.T4_Player1PickedTiles:
                    GameState = GameStates.T3_BoardPlayer2Thinking; break;

                default: break;
            }
            OnPropertyChanged("GameState");
        }

        public void Setup()
        {
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 0]);
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 1]);
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 2]);
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 3]);
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 4]);
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 5]);
            TestRack2.Add(View.UpwordsWindow.RackDisplayGridContents[0, 6]);

            TestRack3.Add("T");
            TestRack3.Add("E");
            TestRack3.Add("S");
            TestRack3.Add("-");
            TestRack3.Add("R");
            TestRack3.Add("C");
            TestRack3.Add("K");
        }

        //public void Setup(int boardWidth, int boardHeight)
        //{
        //    MainBoardContents = new string[BoardWidth, BoardHeight];
        //    for (int column = 0; column < BoardWidth; column++)
        //    {
        //        for (int row = 0; row < BoardHeight; row++)
        //        {
        //            MainBoardContents[row, column] = "?";
        //        }
        //    }
        //    MainBoard = new BoardModel(BoardWidth, BoardHeight);
        //    //TheBusinessRules = new BusinessRules();

        //}


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }





    }
}