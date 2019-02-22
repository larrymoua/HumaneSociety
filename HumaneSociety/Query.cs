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

        internal static int GetCategoryId()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            int Id = 0;
            return Id;
        }

        internal static List<USState> GetStates()
        {
             HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            List<USState> allStates = db.USStates.ToList();

            return allStates;
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

            Animal animalFromDb = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();

            animalFromDb = db.Animals.Where(n => n.Name == animal.Name).FirstOrDefault();

            animalFromDb = db.Animals.Where(a => a.Age == animal.Age).FirstOrDefault();
            animalFromDb = db.Animals.Where(a => a.Demeanor == animal.Demeanor).FirstOrDefault();
            animalFromDb = db.Animals.Where(a => a.KidFriendly == animal.KidFriendly).FirstOrDefault();
            animalFromDb = db.Animals.Where(a => a.PetFriendly == animal.PetFriendly).FirstOrDefault();
            animalFromDb = db.Animals.Where(a => a.Weight == animal.Weight).FirstOrDefault();

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

        }
        internal static void EnterAnimalUpdate(Animal animal, Dictionary<int,string> updates)
        {

        }
        internal static Animal GetAnimalByID(int id)
        {
            Animal animal = new Animal();
            return animal; 
        }
        internal static int GetDietPlanId()
        {
            int dietPlanId = 0;

            return dietPlanId;
        }
        internal static List<Adoption> GetPendingAdoptions()
        {
            List<Adoption> adoptionsList = new List<Adoption>();
            return adoptionsList;
        }
        internal static Room GetRoom(int animalID)
        {
            Room room = new Room();
            return room;

        }
        internal static List<AnimalShot> GetShots(Animal animal)
        {
            Shot shot = new Shot();
            List<AnimalShot> shotList = new List<AnimalShot>();

            return shotList;
        }
        internal static void RemoveAnimal(Animal animal)
        {

        }
        internal static void RunEmployeeQueries(Employee employee, string updated)
        {

        }
        internal static void UpdateAdoption(bool trueOrFalse, Adoption adoption)
        {

        }
        internal static void UpdateShot(string booster, Animal animal)
        {

        }
    }
}