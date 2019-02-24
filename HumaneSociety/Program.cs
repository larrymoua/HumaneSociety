using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            //HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            //Animal cat = db.Animals.Where(a => a.AnimalId == 17).Single();
            //Query.GetAnimalByID(18);     

            Query.GetCategoryId("Shark");
        }
    }
}
