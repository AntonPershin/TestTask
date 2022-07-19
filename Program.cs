using System;
using System.IO;

namespace TestTask
{
    class Program
    {
       
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string command = args[0];
                string operation="";
                operation = Operations.operationConstr(args);
                switch (args[0])
                {
                    case "-add":
                        {
                            if (args.Length > 4)
                            {
                                Console.WriteLine("Неверный формат операции add. Пример использования: -add FirstName:John LastName:Doe Salary:100.50");
                            }
                            else
                            {
                               Operations.add(operation);
                            }
                            
                            break;
                        }
                    case "-update":
                        {

                            Operations.update(operation);
                            break;
                        }
                    case "-delete":
                        {
                            if (args.Length == 2)
                            {
                                int id = -1;
                                try
                                {
                                    id = int.Parse(args[1].Remove(0, args[1].IndexOf(':') + 1));
                                    Operations.delete(id);
                                }
                                catch
                                {
                                    Console.WriteLine("Пример использования: -delete Id:123");
                                }
                            }
                            else
                                Console.WriteLine("Неверный формат операции delete. Пример использования: -delete Id:123 ");
                            
                            break;
                        }
                    case "-get":
                        {
                            if (args.Length == 2)
                            {
                                int id = -1;
                                try
                                {
                                    id = int.Parse(args[1].Remove(0, args[1].IndexOf(':') + 1));
                                    Operations.get(id);
                                }
                                catch
                                {
                                    Console.WriteLine("Пример использования: -get Id:123");
                                }
                            }
                            else
                                Console.WriteLine("Неверный формат операции get. Пример использования: -get Id:123 ");

                            break;
                        }
                    case "-getall":
                        {
                            
                            Operations.getall();
                            break;
                        }
                }
               
            }
            else
            {
                Console.WriteLine("Command not found");
            }
            Console.WriteLine("Для завершения нажмите на любую клавишу");
            Console.ReadKey();
        }
    }
}
