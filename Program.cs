using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
        static List<Employee> employees;
        static void Main(string[] args)
        {
            bool cont = true;
            GetData();
            while(cont)
            {
                Console.Clear();
                Console.WriteLine("Choose Role to Login");
                Console.WriteLine("\n1- Admin");
                Console.WriteLine("2- Employee");
                Console.WriteLine("3- Exit");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AdminLogin();
                        cont = false;
                        break;
                    case 2:
                        UserLogin();
                        cont = false;
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("------------THANK YOU-----------");
                        cont = false;
                        break;
                    default:
                        
                        cont = true;
                        break;

                }

            }
            using (StreamWriter writer= File.CreateText("employees.csv"))
            {
                writer.WriteLine("Id,name,password,address,type");
                foreach(Employee emp in employees)
                {
                    writer.WriteLine(emp.Id+","+emp.Name+","+emp.Password+","+emp.Address+","+emp.Type);
                }
            }
                Console.ReadKey();
        }

        private static void GetData()
        {
            var lineNumber = 0;
            employees = new List<Employee>();
            using (StreamReader reader = new StreamReader("employees.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (lineNumber != 0)
                    {
                        var values = line.Split(',');
                        employees.Add(new Employee { Id=values[0],Name=values[1],Password=values[2],Address=values[3],Type=values[4]});
                    }
                    lineNumber++;
                }
               
            }
        }
        private static void UserLogin()
        {
            Console.Write("Enter Login Id");
            string id = Console.ReadLine();
            Console.Write("Enter Password");
            string password = Console.ReadLine();
            foreach (Employee emp in employees)
            {
                if (emp.Type.Equals("emp") && emp.Id.Equals(id) && emp.Password.Equals(password))
                {
                    UserMenu(emp);
                    break;
                }
            }
        }

        private static void UserMenu(Employee emp)
        {
            Console.WriteLine("ID:{0}   Name:{1}   Password:{2}   Address:{3}", emp.Id, emp.Name, emp.Password, emp.Address);
            Console.WriteLine("Press 1 to update your details 2 to Exit");
            int choice =int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Write("\nEnter Your Name");
                    emp.Name = Console.ReadLine();
                    Console.Write("\nEnter Your Password");
                    emp.Password = Console.ReadLine();
                    Console.Write("\nEnter Your Address");
                    emp.Address = Console.ReadLine();
                    var empE = employees.FirstOrDefault(x => x.Id.Equals(emp.Id));
                    if(empE!=null)
                    {
                        empE.Name = emp.Name;
                        empE.Password = emp.Name;
                        empE.Address = emp.Address;
                    }
                    break;
                case 2:
                    Console.Clear();
                    break;
            }

            }

        private static void AdminLogin()
        {

           
           Console.Write("Enter Login Id");
            string id = Console.ReadLine();
            Console.Write("Enter Password");
            string password = Console.ReadLine();
            foreach(Employee emp in employees)
            {
                if(emp.Type.Equals("admin")&&emp.Id.Equals(id)&&emp.Password.Equals(password))
                {
                    AdminMenu();
                    break;
                }
            }
        }

        private static void AdminMenu()
        {
            bool cont = true;
            while (cont)
            {
                Console.Clear();
                Console.WriteLine("------Menu------");
                Console.WriteLine("\n1- Add Employee");
                Console.WriteLine("2- Remove Employee");
                Console.WriteLine("3- Exit");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        cont = true;
                        break;
                    case 2:
                        RemoveEmployee();
                        cont = true;
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("------------THANK YOU-----------");
                        cont = false;
                        break;
                    default:

                        cont = true;
                        break;

                }

            }
        }
        private static void RemoveEmployee()
        {
            Employee emp = new Employee();
            Console.Write("\nEnter Employee ID");
            emp.Id = Console.ReadLine();
            var empp = employees.FirstOrDefault(item => item.Id.Equals(emp.Id));
            if (empp != null)
            {
                employees.Remove(empp);
                Console.WriteLine("Emplyee removed success!");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Emplyee not exists");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddEmployee()
        {
            Employee emp = new Employee();
            Console.Write("\nEnter Employee ID");
            emp.Id = Console.ReadLine();
            var empp = employees.FirstOrDefault(item => item.Id.Equals(emp.Id));
            if(empp!=null)
            {
                Console.WriteLine("Emplyee alreay exists");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            Console.Write("\nEnter Employee Name");
            emp.Name = Console.ReadLine();
            Console.Write("\nEnter Employee Password");
            emp.Password = Console.ReadLine();
            Console.Write("\nEnter Employee Address");
            emp.Address = Console.ReadLine();
            Console.Write("Enter Employee Type?[admin or emp]");
            emp.Type = Console.ReadLine();
            employees.Add(emp);
            Console.WriteLine("Employee added success!");
            Console.ReadKey();
        }
    }
}
