using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpwordsWPF1.ViewModel;
using UpwordsWPF1.View;

namespace UpwordsWPF1.Model
{
    class BoardModel
    {
        static int BoardHeight = 10;
        static int BoardWidth = 10;

        private string[,] _board; // First index is row number, second index is column
        
        // Constructor
        public BoardModel()
        {

            _board = new string[BoardHeight, BoardWidth];

        }



        // Peek at one cell of the _board array
        public string Peek(int row, int column)
        {
            return _board[row, column];
        }
    }
}
