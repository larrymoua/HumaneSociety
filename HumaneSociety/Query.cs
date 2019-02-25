using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {

        //TO DO: make GetRoom(animal) used in UserEmployee
        //TO DO: make GetPendingAdoptions() used in UserEmployee
        //TO DO: make UpdateAdoption(bool, adoption) in UserEmployee
        //TO DO: SearchForAnimalByMultipleTraits() in UserEmployee
        //TO DO: GetShots(animal)
        //TO DO: UpdateShots(string, animal)
        //TO DO: EnterAnimalUpdate(animal, updates)
        //TO DO: GetCategoryId

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

            db.Animals.InsertOnSubmit(newAnimal);
            db.SubmitChanges();
        }
     
        internal static List<Animal> SearchForAnimalByMultipleTraits()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            List<Animal> foundAnimals = new List<Animal>();
            return foundAnimals;
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
        internal static void EnterAnimalUpdate(Animal animal, Dictionary<int,string> updates)
        {
            
        }
        internal static Animal GetAnimalByID(int id)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animal = db.Animals.Where(a => a.AnimalId == id).Single();

            return animal; 
        }
        internal static int GetDietPlanId()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var dietPlanId = db.Animals.Select(d => d.DietPlanId);
           

            return Convert.ToInt32(dietPlanId);
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

        }
        internal static void UpdateAdoption(bool trueOrFalse, Adoption adoption)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            if (trueOrFalse == true)
            { 
                adoption.ApprovalStatus = "approved";
                adoption.Animal.AdoptionStatus = "adopted";
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

            
        }
    }
}