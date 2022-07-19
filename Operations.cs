using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace TestTask
{
    public class Operations
    {
        private static int CompareEmpById(Emp x,Emp y)
        {
            if (x.id == y.id)
                return 0;
            else
            {
                if (x.id > y.id)
                   return 1;
                else
                   return -1;
            }
        }

        public static List<Emp> JsonEmp()
        {
            List<Emp> currEmps=new();
            JsonTextReader jsonText = new JsonTextReader(new StreamReader("emp.json"));
            jsonText.SupportMultipleContent = true;
            while (jsonText.Read())
            {
                try {
                    currEmps.Add(new JsonSerializer().Deserialize<Emp>(jsonText));
                }
                catch
                {

                }
            }
            jsonText.Close();
            currEmps.Sort(CompareEmpById);
            return currEmps;

        }
       public static string operationConstr(string[] args)
        {
            string operation = "";
            for (int i = 1; i < args.Length; i++)
            {
                operation += args[i];
                operation += " ";
            }
            return operation;
        }
       public static void add(string operation)
        {
            string[] args = operation.Split(' ');
            Emp emp = new Emp();
            foreach (string arg in args)
            {
                string[] temp = arg.Split(':');
                switch (temp[0])
                {
                    case "FirstName":
                        {
                            emp.FirstName = temp[1];
                            break;
                        }
                    case "LastName":
                        {
                            emp.LastName = temp[1];
                            break;
                        }
                    case "Salary":
                        {
                            try
                            {
                                string temp1;
                                temp1 = temp[1].Replace('.', ',');
                                emp.Salary = Decimal.Parse(temp1);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка добавления сотрудника");
                                break;
                            }
                        }
                }
            }
            if (!File.Exists("emp.json")) {
               
                emp.id = 0;
                string addEmp = JsonConvert.SerializeObject(emp);
                Console.WriteLine(addEmp);
                File.WriteAllText("emp.json", addEmp);
            }
            else 
            {
                List<Emp> emps = JsonEmp();
                if (emps.Count != 0)
                {
                    emp.id = emps[emps.Count - 1].id + 1;
                    string addEmp = JsonConvert.SerializeObject(emp);
                    File.AppendAllText("emp.json", addEmp);
                }
                else
                {
                    emp.id = 0;
                    string addEmp = JsonConvert.SerializeObject(emp);
                    File.AppendAllText("emp.json", addEmp);
                }
            }
        }
        public static void update(string operation)
        {
            string[] args = operation.Split(' ');
            Emp emp = new Emp();
            foreach (string arg in args)
            {
                string[] temp = arg.Split(':');
                if (temp[0] == "id")
                {
                    List<Emp> emps = JsonEmp();
                    int s = 0; 
                   foreach(Emp emp1 in emps)
                   {
                        if (emp1.id == int.Parse(temp[1]))
                        {
                            
                            foreach (string arg1 in args) 
                            {
                                string[] temp1 = arg1.Split(':');
                                switch (temp1[0])
                                {
                                    case "FirstName":
                                        {
                                            emp1.FirstName = temp1[1];
                                            break;
                                        }
                                    case "LastName":
                                        {
                                            emp1.LastName = temp1[1];
                                            break;
                                        }
                                    case "Salary":
                                        {
                                            try
                                            {
                                               string temp2 = temp1[1].Replace('.', ',');
                                                
                                                emp1.Salary = Decimal.Parse(temp2);
                                                break;
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ошибка обновления сотрудника");
                                                break;
                                            }
                                        }
                                }
                                
                            }
                            delete(emp1.id);
                            if (emps.Count != 0)
                            {
                                emp = emp1;
                                string addEmp = JsonConvert.SerializeObject(emp);
                                File.AppendAllText("emp.json", addEmp);
                            }
                            else
                            {
                                emp.id = 0;
                                string addEmp = JsonConvert.SerializeObject(emp);
                                File.AppendAllText("emp.json", addEmp);
                            }
                            break;
                        }
                        else
                            s++;
                   }
                   if (s == emps.Count)
                    {
                        Console.WriteLine("ID не существует");
                        return;
                    }
                }
            }
            Console.WriteLine("Обновление успешно");
        }
       public static void get(int id)
        {
            if (File.Exists("emp.json"))
            {
                List<Emp> currEmps = JsonEmp();
                int s = 0;
                List<Emp> temp = currEmps;
                if (File.Exists("emp.json"))
                    foreach (Emp emp in currEmps)
                    {
                        if (emp.id == id)
                        {
                            Console.WriteLine($"Id = {emp.id}, FirstName = {emp.FirstName}, LastName = {emp.LastName},SalaryPerHour = {emp.Salary}");

                            break;
                        }
                        else s++;
                    }
                if (s == currEmps.Count)
                {
                    Console.WriteLine("ID не существует");
                    return;
                }
                string addEmp = JsonConvert.SerializeObject(currEmps);
            }
        }
       public static void delete(int id)
        {
            if (File.Exists("emp.json"))
            {
                List<Emp> currEmps = JsonEmp();
                int s = 0;
                List<Emp> temp = currEmps;
                foreach (Emp emp in currEmps)
                {
                    if (emp.id == id)
                    {
                        temp.Remove(emp);
                        break;
                    }
                    else s++;
                }
                if (s == currEmps.Count)
                {
                    Console.WriteLine("ID не существует");
                    return;
                }
                string addEmp = JsonConvert.SerializeObject(currEmps);
                File.WriteAllText("emp.json", string.Empty);
                File.AppendAllText("emp.json", addEmp);
                Console.WriteLine("Удаление успешно");
            }
        }
        public static void getall()
        {
            if (File.Exists("emp.json"))
            {
                List<Emp> currEmps = JsonEmp();
                foreach (Emp emp in currEmps)
                {
                    Console.WriteLine($"Id = {emp.id}, FirstName = {emp.FirstName}, LastName = {emp.LastName},SalaryPerHour = {emp.Salary}");
                }
            }
        }
    }
}
