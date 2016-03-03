using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpwordsWPF1.Model
{
    public class TileBag
    {
        List<Tile> bag;
        //int bagIndex,bagLength,swapIndex;
        Random r;

        ////Contructor
        //public TileBag()
        //{

        //}

        public void FillBag 
        (string Content = "E,E,E,E,E,E,E,E,A,A,A,A,A,A,A,I,I,I,I,I,I,I,O,O,O,O,O,O,O,S,S,S,S,S,S,D,D,D,D,D,L,L,L,L,L,M,M,M,M,M,N,N,N,N,N,R,R,R,R,R,T,T,T,T,T,U,U,U,U,U,C,C,C,C,B,B,B,F,F,F,G,G,G,H,H,H,P,P,P,K,K,W,W,Y,Y,J,Qu,V,X,Z")
        {
            string[] temp = Content.Split(',');
            bag = new List<Tile>();

            foreach (var onetile in temp)
            {
                bag.Add(new Tile(onetile, 1));
            }
        }

        public void ShakeBag(int? randomize = 1)
        {
            if (!randomize.HasValue) // Randomise based on system clock if parameter is null
                r = new Random();
            else
                r = new Random(randomize.Value);

            //for (int i=bagIndex; i < bagLength-1; i++) 
            //{
            //    swapIndex = i+r.Next(0,(bagLength-1)-i);
            //    temp = bag[i];
            //    bag[i] = bag[swapIndex];
            //    bag[swapIndex] = temp;             
            //}
        }

        public Tile GetTile()
        {
            var result = from t in bag
                         where t.Location == Locations.Bag
                         select t;

            Tile tt = result.ElementAt(r.Next(0,result.Count()));

            return tt;

            //temp = bag[bagIndex];
            //bag[bagIndex++] = "";
            //return temp;

        }

        public void ReturnTile(Tile t)
        {
            t.Location = Locations.Bag;
        }






    }
}
