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
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Adoption cat = db.Adoptions.Where(a => a.ClientId == 3).Single();
            //DietPlan dietPlan = new DietPlan();
            //dietPlan.Name = "hi";

            //Query.UpdateDietPlan(dietPlan);

            //PointOfEntry.Run();
        }
    }
}
