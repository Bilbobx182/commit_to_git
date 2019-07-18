using System;

namespace cttm
{
    class Program
    {
        
        static void Main(string[] args)
        {
            io_util io_object = new io_util();
            io_object.write_dictionary_to_file("aa.txt",io_object.get_user_projects("bilbobx182"));
            Console.WriteLine("DONE");
        }
    }
}