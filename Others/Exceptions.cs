using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.IO;
namespace Others
{
    class MyExceptions
    {       
        public static void GenerateException()
        {            
            try
            {
                FirstException();
            }
            catch (Exception e)
            {
                //throw;
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                if (e.InnerException != null)
                {
                    Console.WriteLine("InnerExceptions!");
                    Console.WriteLine(e.InnerException.Message);
                    Console.WriteLine(e.InnerException.StackTrace);
                }
            }
        }

        public static void FirstException()
        {
            try
            {
                SecondException();
            }
            catch (Exception ex)
            {
                //очищает StackTrace (цепочка всех вызовов внутри SecondException, что привело к исключению)
                throw new Exception("With inner ex", ex);

                //отображает всю цепочку вызовов в SecondException
                //throw;
            }
            finally
            {
                Console.WriteLine("First finally;");
            }
        }

        public static void SecondException()
        {
            try
            {
                ThirdException();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Second finally;");
            }
        }

        public static void ThirdException()
        {
            try
            {
                throw new Exception("Third");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Third finally;");
            }
        }

    }
}
