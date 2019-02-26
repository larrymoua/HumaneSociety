using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        internal static int GetCategoryId(string categoryName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var category = db.Categories.Where(c => c.Name == categoryName).Single();
            if (category == null)
            {
                Category newCategory = new Category();
                newCategory.Name = categoryName;

                db.Categories.InsertOnSubmit(newCategory);
                db.SubmitChanges();
            }
            return category.CategoryId;
        }

        internal static List<USState> GetStates()
        {
             HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            List<USState> allStates = db.USStates.ToList();

            return allStates;
        }
        internal static void RemoveAnimal(Animal animal)
        {
            
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            animal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Single();

            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        internal static Client GetClient(string userName, string password)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.AddressLine2 = null;
                newAddress.Zipcode = zipCode;
                newAddress.USStateId = stateId;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            // find corresponding Client from Db
            Client clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();

            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.AddressLine2 = null;
                newAddress.Zipcode = clientAddress.Zipcode;
                newAddress.USStateId = clientAddress.USStateId;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }
        internal static void UpdateDietPlan(DietPlan planWithUpdates)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();


            planWithUpdates.Name = UserInterface.GetStringData("the diet plan's", "name");
            planWithUpdates.FoodType = UserInterface.GetStringData("the diet plan's", "food type");
            planWithUpdates.FoodAmountInCups = UserInterface.GetIntegerData("the diet plan's", "amount of food in cups");

            db.SubmitChanges();
        }
        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if(employeeFromDb == null)
            {
                throw new NullReferenceException();            
            }
            else
            {
                return employeeFromDb;
            }            
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }
        internal static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal newAnimal = new Animal();

            newAnimal.Name = animal.Name;
            newAnimal.Age = animal.Age;
            newAnimal.Demeanor = animal.Demeanor;
            newAnimal.KidFriendly = animal.KidFriendly;
            newAnimal.PetFriendly = animal.PetFriendly;
            newAnimal.Weight = animal.Weight;
            newAnimal.CategoryId = animal.CategoryId;

            db.Animals.InsertOnSubmit(newAnimal);
            db.SubmitChanges();
        }
        internal static void AddDietPlan(DietPlan diet)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            DietPlan newDietPlan = new DietPlan();

            newDietPlan.Name = diet.Name;
            newDietPlan.FoodType = diet.FoodType;
            newDietPlan.FoodAmountInCups = diet.FoodAmountInCups;

            db.DietPlans.InsertOnSubmit(newDietPlan);
            db.SubmitChanges();
        }
        internal static List<Animal> SearchForAnimalByMultipleTraits()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Dictionary<int, string> keyValuePairs = UserInterface.GetAnimalCriteria();

            List<Animal> foundAnimals = new List<Animal>();

            foreach (KeyValuePair<int, string> entry in keyValuePairs)
            {
                switch (entry.Key)
                {
                    case 1:
                        var searchCategory = db.Categories.Where(c => c.Name == entry.Value);
                        foreach (var animals in searchCategory)
                        {
                            var searchAnimal = db.Animals.Where(a => a.CategoryId == animals.CategoryId );
                            foreach (var animal in searchAnimal)
                            {
                                foundAnimals.Add(animal);
                            }            
                        }          
                        break;
                    case 2:
                        var searchAnimalName = db.Animals.Where(n => n.Name == entry.Value);
                        foreach (var animal in searchAnimalName)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    case 3:
                        var searchAge = db.Animals.Where(a => a.Age == Convert.ToInt64(entry.Value));
                        foreach (var animal in searchAge)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    case 4:
                        var searchDemeanor = db.Animals.Where(d => d.Demeanor == entry.Value);
                        foreach (var animal in searchDemeanor)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    case 5:
                        var searchKidFriendly = db.Animals.Where(k => k.KidFriendly == Convert.ToBoolean(entry.Value));
                        foreach (var animal in searchKidFriendly)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    case 6:
                        var searchPetFriendly = db.Animals.Where(k => k.PetFriendly == Convert.ToBoolean(entry.Value));
                        foreach (var animal in searchPetFriendly)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    case 7:
                        var searchWeight = db.Animals.Where(w => w.Weight == Convert.ToInt64(entry.Value));
                        foreach (var animal in searchWeight)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    case 8:
                        var searchAnimalId = db.Animals.Where(a => a.AnimalId == Convert.ToInt64(entry.Value));
                        foreach (var animal in searchAnimalId)
                        {
                            foundAnimals.Add(animal);
                        }
                        break;
                    default:
                        break;
                }
            }
           var catchthing = foundAnimals.Distinct();

            return catchthing.ToList(); ;
        }
        internal static void Adopt(Animal animal, Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Adoption newAdoption = new Adoption();

            newAdoption.ClientId = client.ClientId;
            newAdoption.AnimalId = animal.AnimalId;
            newAdoption.ApprovalStatus = "pending";
            newAdoption.AdoptionFee = 75;
            newAdoption.PaymentCollected = false;

            db.Adoptions.InsertOnSubmit(newAdoption);

            db.SubmitChanges();

        }
        internal static void EnterAnimalUpdate(Animal animal, Dictionary<int, string> updates)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updateAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Single();


            foreach (KeyValuePair<int, string> entry in updates)
            {
             
                switch (entry.Key)
                {
                    case 1:
                        updateAnimal.CategoryId = Convert.ToInt32(entry.Value);
                        break;
                    case 2:
                        updateAnimal.Name = entry.Value;
                        break;
                    case 3:
                        updateAnimal.Age = Convert.ToInt32(entry.Value);
                        break;
                    case 4:
                        updateAnimal.Demeanor = entry.Value;
                        break;
                    case 5:
                        updateAnimal.KidFriendly = Convert.ToBoolean(entry.Value);
                        break;
                    case 6:
                        updateAnimal.PetFriendly = Convert.ToBoolean(entry.Value);
                        break;
                    case 7:
                        updateAnimal.Weight = Convert.ToInt32(entry.Value);
                        break;
                    default:
                        break;
                }
            }
            db.SubmitChanges();
        }
        internal static Animal GetAnimalByID(int id)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animal = db.Animals.Where(a => a.AnimalId == id).Single();

            return animal; 
        }
        internal static int GetDietPlanId(string dietPlanName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var dietPlanId = db.DietPlans.Where(d => d.Name == dietPlanName).Single();

            return dietPlanId.DietPlanId;
        }

        internal static List<Adoption> GetPendingAdoptions()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            List<Adoption> adoptionsList = new List<Adoption>();
            var pendingAdoptions = db.Adoptions.Where(p => p.ApprovalStatus == "pending");
            foreach (var adoption in pendingAdoptions)
            {
                adoptionsList.Add(adoption);
            }
            return adoptionsList;
        }
        internal static Room GetRoom(int animalID)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Room foundRoom = db.Rooms.Where(r => r.AnimalId.Equals(animalID)).Single();

            return foundRoom;          
        }
        internal static List<AnimalShot> GetShots(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            List<AnimalShot> shotList = new List<AnimalShot>();
            var shots = db.AnimalShots.Where(a => a.AnimalId.Equals(animal.AnimalId));
            foreach(var shot in shots)
            {
                shotList.Add(shot);
            }
            return shotList;
        }
      
        internal static void RunEmployeeQueries(Employee employee, string updated)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee findEmployee;
            switch (updated)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    break;
                case "delete":
                    db.Employees.DeleteOnSubmit(db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber && e.LastName == employee.LastName).Single());
                    break;
                case "read":
                    findEmployee = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).Single();
                    UserInterface.DisplayEmployee(findEmployee);
                    break;
                case "update":
                    findEmployee = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).Single();
                    findEmployee = employee;                  
                    break;
                default:
                    break;
            }

            db.SubmitChanges();
        }
        internal static void UpdateAdoption(bool trueOrFalse, Adoption adoption)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            if (trueOrFalse == true)
            {
                adoption.ApprovalStatus = "approved";
                adoption.Animal.AdoptionStatus = "adopted";
                adoption.PaymentCollected = true;

                db.SubmitChanges();
            }
            else
            {
                adoption.ApprovalStatus = "denied";
                adoption.Animal.AdoptionStatus = "not adopted";
               
            }
        }  
        internal static void UpdateShot(string booster, Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var updatedShot = db.Shots.Where(s => s.Name == booster);
            
        
        }

    }
}