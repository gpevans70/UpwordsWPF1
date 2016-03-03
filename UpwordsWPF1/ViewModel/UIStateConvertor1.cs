using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UpwordsWPF1.ViewModel

{
    public class UIStateConvertor1 : IValueConverter
    {

        public object Convert(object gamestate, Type targetType, object parameter, CultureInfo culture)
        {
            var UIStateLookup = new SortedList<GameStates, string> // For each gamestate<int> the string contains the tabs and controls which are enabled/checked
            {
                {   GameStates.T1_InProgress,"T1_Tab,T1_Continue,T5_Tab" },
                {   GameStates.T4_GoFirstPickIncomplete,"T4_Tab,T4_GoFirst,T5_Tab" },
                {   GameStates.T4_GoFirstTryAgain,"T4_Tab,T4_Continue,T4_GoFirstTryAgain,T5_Tab" },
                {   GameStates.T4_GoFirstPlayer1,"T4_Tab,T4_Continue,T4_GoFirstComputer,T5_Tab" },
                {   GameStates.T4_GoFirstPlayer2,"T4_Tab,T4_Continue,T4_GoFirstHuman,T5_Tab" },
                {   GameStates.T3_BoardPlayer2Thinking,"T2_Tab,T3_Tab,T3_Continue,T3_Thinking_Enabled,T3_Thinking_Checked,T5_Tab" }, // T3_Continue is temporary - later it will be enabled in code once the computer move is decided 
                {   GameStates.T3_BoardPlayer2MoveDecided,"T3_Tab" },
                {   GameStates.T3_BoardPlayer2MoveShown,"T3_Tab,T3_Continue" },
                {   GameStates.T2_ScorePlayer2,"T2_Tab,T2_Continue,T5_Tab" },
                {   GameStates.T4_BeginGamePlayer1PickingTiles,"T4_Tab,T4_HumanTiles,T5_Tab" },
                {   GameStates.T4_BeginGamePlayer1PickedTiles,"T4_Tab,T4_Continue,T4_HumanTiles,T5_Tab" },
                {   GameStates.T4_Player2PickingTiles,"T4_Tab,T4_ComputerTiles,T5_Tab" },
                {   GameStates.T4_Player2PickedTiles,"T4_Tab,T4_Continue,T4_ComputerTiles,T5_Tab" }, 
                {   GameStates.T3_BoardPlayer1Thinking,"T2_Tab,T3_Tab,T3_Continue,T3_Thinking_Enabled,T3_Thinking_Checked,T3_Play_Enabled,T5_Tab" }, // T3_Continue is temporary - later it will be enabled in code once the player move is decided
                {   GameStates.T3_BoardPlayer1MoveDecided,"T3_Tab" },
                {   GameStates.T3_BoardPlayer1MoveShown,"T2_Tab,T2_Continue,T3_Tab,T5_Tab" },
                {   GameStates.T2_ScorePlayer1,"T2_Tab,T2_Continue,T5_Tab" },
                {   GameStates.T4_BeginGamePlayer2PickingTiles,"T4_Tab,T4_ComputerTiles,T5_Tab" },
                {   GameStates.T4_BeginGamePlayer2PickedTiles,"T4_Tab,T4_Continue, T4_ComputerTiles,T5_Tab" },
                {   GameStates.T4_Player1PickingTiles,"T4_Tab,T4_HumanTiles,T5_Tab" },
                {   GameStates.T4_Player1PickedTiles,"T4_Tab,T4_Continue,T4_HumanTiles,T5_Tab" } 
            };

            string statusString;

            if (!(gamestate is GameStates))
            {
                throw new NotImplementedException();
            }

            if (!(parameter is string))
            {
                throw new NotImplementedException();
            }

            if (UIStateLookup.TryGetValue((GameStates)gamestate,out statusString) )
            { return statusString.Contains((string)parameter); };

            return false;      // This could be an exception as it will only happen if gamestate is an unexpected vale
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
}
