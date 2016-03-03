using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpwordsWPF1.Model
{
    public enum Locations
    {
        Bag,
        Player1Rack,
        Player2Rack,
        Board
    }

    public class Tile
    {
        public string Face { get; }
        public int Score { get; }
        private Locations location= Locations.Bag;
        private int xCoordinate = 0;
        private int yCoordinate = 0;
        private int zCoordinate = 0;

        public Tile(string face, int score)
        {
            Face = face;
            Score = score;
        }

        public override string ToString()
        {
            return Face;
        }



        internal Locations Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public int XCoordinate
        {
            get
            {
                return xCoordinate;
            }

            set
            {
                xCoordinate = value;
            }
        }

        public int YCoordinate
        {
            get
            {
                return yCoordinate;
            }

            set
            {
                yCoordinate = value;
            }
        }

        public int ZCoordinate
        {
            get
            {
                return zCoordinate;
            }

            set
            {
                zCoordinate = value;
            }
        }
    }
}
