using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Serialization
{
    [Serializable]
    public class BaseClass
    {
        //private int privateField;
        
        protected string strField = "default";
        public string strProp 
        { 
            get { return strField; }
            set { strField = value; }
        }
    }

    [Serializable]
    public class DerivedClass : BaseClass, ISerializable
    {        
        public int intProp { get; set; }
        public IList<string> ListStr { get; set; }
        public DerivedClass()
        {            
        }

        public DerivedClass(SerializationInfo info, StreamingContext context)
        {
            strField = info.GetString("strField");
            intProp = info.GetInt32("intProp");
            ListStr = (List<string>)info.GetValue("ListStr", typeof(List<string>));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("strField", strField);
            info.AddValue("intProp", intProp);
            info.AddValue("ListStr", ListStr);
        }

        [OnSerializing]
        private void OnDeserializing(StreamingContext context)
        {
        }
        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        { 
        }
    }
    


    class Program
    {
        static void Main(string[] args)
        {
            Serialization();
            DeSerialization();

            Console.ReadLine();
        }

        static void Serialization()
        {
            DerivedClass dc = new DerivedClass() { intProp = 15, strProp = "ChangedValue" };
            dc.ListStr = new List<string>();
            dc.ListStr.Add("dsa1");
            dc.ListStr.Add("dsa2");
            dc.ListStr.Add("dsa3");

            string path = @"d:\serialize.txt";
            File.Delete(path);
            using (MemoryStream mem = new MemoryStream())
            {
                BinaryFormatter f = new BinaryFormatter();
                f.Serialize(mem, dc);

                FileStream file = new FileStream(path, FileMode.OpenOrCreate);
                
                mem.Position = 0;
                mem.CopyTo(file);
                file.Close();
            }
        }

        static void DeSerialization()
        {
            DerivedClass dc = new DerivedClass();

            using (Stream file = new FileStream(@"d:\serialize.txt", FileMode.OpenOrCreate))
            {
                BinaryFormatter f = new BinaryFormatter();                                
                dc = (DerivedClass)f.Deserialize(file);
                
                Console.WriteLine("SerializedInfo:\nint = {0}", dc.intProp);
                Console.WriteLine("str = {0}", dc.strProp);
                foreach (string s in dc.ListStr)
                    Console.WriteLine("list[{1}] = {0}", s, dc.ListStr.IndexOf(s));
            }
        }

    }
}
