using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace GenericCrudOperations
{
    class Program
    {
        static List<object> _dtoList;
        static List<string> _command = new List<string>() { "Select", "Insert", "Update", "Delete" };
        static DataSet ds = new DataSet();
        static void Main(string[] args)
        {
            GenericCrud _gCrud = new GenericCrud();
            Console.WriteLine("Available Tables..");
            //listing all dtos
            UsersDto _userDto = new UsersDto();
            EmployeeDTO _empDto = new EmployeeDTO();
            _dtoList = new List<object>() { _userDto, _empDto };
            int i = 0;
            foreach (var obj in _dtoList) Console.Write((i++) + " " + obj.GetType().Name + "\t");

            var _dtoChoice = Convert.ToInt32(Console.ReadLine());

            //stores the name of selected dto
            var _selectedDto = _dtoList[_dtoChoice].GetType();

            //listing all operations
            int j = 0;
            foreach (var obj in _command) Console.Write((j++) + " " + obj + "\t");

            //reading operation choice
        

            //creating instance for dto
            var dtoObj = Activator.CreateInstance(_selectedDto);
            var _operationChoice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(_selectedDto.Name);

            // string MethodName = _command[_operationChoice];
            // Console.WriteLine(_command[_operationChoice]);
            MethodInfo methodName = typeof(GenericCrud).GetMethod(_command[_operationChoice]);
             methodName = methodName.MakeGenericMethod(dtoObj.GetType());
          
        //    MethodInfo methodName = typeof(GenericCrud).GetMethod(_command[_operationChoice]);
            methodName.Invoke(_gCrud, new[] { dtoObj });

            //while (true)
            //{
            //    Console.WriteLine("\nenter your choice");
            //    var _operationChoice = Convert.ToInt32(Console.ReadLine());
            //    switch (_operationChoice)
            //    {
            //        case 0: { _gCrud.Select(dtoObj); } break;
            //        case 1: { _gCrud.Insert(dtoObj); _gCrud.Select(dtoObj); } break;
            //        case 2: { _gCrud.Select(dtoObj); _gCrud.Update(dtoObj); _gCrud.Select(dtoObj); } break;
            //        case 3: { _gCrud.Select(dtoObj); _gCrud.Delete(dtoObj); _gCrud.Select(dtoObj); } break;
            //        case 4: Environment.Exit(0); break;
            //    }
            //}
            Console.Read();
        }
    }
}
