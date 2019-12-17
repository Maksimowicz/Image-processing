using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingMM.DataTypes;

namespace ImageProcessingMM.LINQExtensions
{
    public static class ImageProcessingExtensions
    {
 
        public static int Median(this IEnumerable<int> vs) //Method to allow calculate median in IEnumerables
        {
            int count = vs.Count();
            int returnValue;
            int index;
            
            if (count == 0)
            {
                throw new Exception("No elements in collection");
            }
           
            index = count / 2;
            var sorted = vs.OrderBy(x => x);
            if (count % 2 == 0)
            {
                returnValue = (sorted.ElementAt(index) + sorted.ElementAt(index + 1)) / 2;
            }
            else
            {
                returnValue = (sorted.ElementAt(index));
            }
           
            return returnValue;
        }

       
    }
}
