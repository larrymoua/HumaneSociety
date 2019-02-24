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
<<<<<<< HEAD
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal cat = db.Animals.Where(a => a.AnimalId == 17).Single();
            Query.GetShots(cat);
          
=======

            PointOfEntry.Run();            
>>>>>>> 517ab9f44f9f240966fe1d8a42e0a87f199b6144
        }
    }
}
