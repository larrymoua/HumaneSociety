﻿using System;
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
<<<<<<< HEAD
            //HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            //Animal cat = db.Animals.Where(a => a.AnimalId == 17).Single();
            //Query.GetAnimalByID(18);     

            Query.GetCategoryId("Shark");
=======
<<<<<<< HEAD
            //Query.GetCategoryId("Cat");
            //Query.GetPendingAdoptions();
            //PointOfEntry.Run();
=======
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal cat = db.Animals.Where(a => a.AnimalId == 17).Single();
            Query.GetShots(cat);       
>>>>>>> 80ba7097f59b65ba26caf99fae17ce7893600cde
>>>>>>> ccd8ba84967d520a1ed748b8f7ecb9aa820e8aee
        }
    }
}
