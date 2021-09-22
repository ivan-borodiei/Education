using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using Patterns.Patterns;

namespace Patterns
{
    // entry point for viewing patterns
    public class Program
    {        
        public static void Main(string[] args)
        {
            //Stratagy();
            //Observer();
            //Decorator();
            //AbstractFactory();
            //FactoryMethod();
            //Command();            
            //Singletons();
            //Adapter();
            //Facade();
            //State();
            //TemplateMethod();
            //Builder();
            //Iterator();

            //Composite();
            Memento();

            Console.ReadLine();
        }

        /// <summary>
        /// Стратегия (поведенческий)
        /// </summary>
        static void Stratagy() 
        {
            Subject<int> subj = new Subject<int>(new SelectionSort());
            subj.collection = new int[] { 4, 6, 3, 76, 9, 4, 2 };
            subj.Sort();
            subj.ShowSortedCollection();
        }

        /// <summary>
        /// Издатель-подписчик (поведенческий)
        /// </summary>
        static void Observer()
        {
            DriveSport mag = new DriveSport();
            Person p = new Person(mag);
            mag.Notify();
        }

        /// <summary>
        /// Декоратор (структурный)
        /// </summary>        
        static void Decorator()
        {
            var config = new Config();
            Console.WriteLine($"OneDrive read from env vars: {config.GetSettingValue("OneDrive")}");
            Console.WriteLine($"OneDrive read from cache: {config.GetSettingValue("OneDrive")}");
            Console.WriteLine($"UnknownSetting read from DB: {config.GetSettingValue("UnknownSetting")}"); 
            Console.WriteLine($"UnknownSetting read from cache: {config.GetSettingValue("UnknownSetting")}");


            //Beverage beverage = new Coffee();
            //beverage = new Milk(beverage);
            //beverage = new Cream(beverage);
            //Console.WriteLine("{0} costs {1:C}$", beverage.Name, beverage.Cost());
        }

        /// <summary>
        /// Абстрактная фабрика (порождающий)
        /// </summary>
        static void AbstractFactory()
        {
            PizzaStore client = new UkrainianPizzaStore(new UkrainePizzaFactory());

            Pizza pizza = client.OrderPizza(); //with abstract factory
            Console.WriteLine("Pizza Name - {0}, PizzaWeight - {1}", pizza.Name, pizza.Weight);

        }

        /// <summary>
        /// Фабричный метод (порождающий)
        /// </summary>
        static void FactoryMethod()
        {
            PizzaStore client = new UkrainianPizzaStore(new UkrainePizzaFactory());

            Pizza pizza = client.OrderPizza("Papperony"); //with factory method
            Console.WriteLine("Pizza Name - {0}, PizzaWeight - {1}", pizza.Name, pizza.Weight);
        }

        /// <summary>
        /// Команда (поведенческий)
        /// </summary>
        static void Command()
        {
            Calculator calc = new Calculator();
            Console.Write("Add 10 = " + calc.Add(10));
            Console.Write(new String(' ', 3));
            Console.WriteLine("Add 5 = " + calc.Add(5));

            //Patterns.Additional.RemotePanel panel = new Patterns.Additional.RemotePanel();
            //panel.Mode = Patterns.Additional.RemotePanel.RemotePanelMode.LivingRoom;
            //panel.button1On.Execute("On");
            //panel.button1Off.Execute("Off");
            //panel.CancelCmd.Execute(panel.CancelCmd);
        }

        /// <summary>
        /// Singleton(одиночка) (порождающий)
        /// </summary>
        static void Singletons()
        {
            Singleton s1  = Singleton.Instance;
            Console.WriteLine("{0}: Singleton.Instance {1} Singleton.Instance", s1.text, ReferenceEquals(Singleton.Instance, Singleton.Instance) ? "=" : "!=");
        }

        /// <summary>
        /// Adapter (структурный)
        /// </summary>
        static void Adapter()
        {
            IAdapter adapter = new Adapter(new EuroPlug());
            Device d = new Device(adapter);
            d.UsePlug();

            
            SortedSet<AdapterInstance> manlist = new SortedSet<AdapterInstance>(AdapterHelper.GetComparer<AdapterInstance>((x,y) => x.Id.CompareTo(y.Id)));
            manlist.Add(new AdapterInstance() { Id = 3, Name = "Ivan" });
            manlist.Add(new AdapterInstance() { Id = 1, Name = "Andrew" });
            foreach (AdapterInstance a in manlist)
                Console.WriteLine("ID = {0}, Name = {1}", a.Id, a.Name);

        }

        /// <summary>
        /// Facade (структурный)
        /// </summary>
        static void Facade()
        {
            ProfessionalEducation prof = new ProfessionalEducation(new Reading(), new Listening(), new Watching());
            prof.Commmmon();
        }

        /// <summary>
        /// State (поведенческий)
        /// </summary>
        static void State()
        {
            AutomaticCar car = new AutomaticCar();
            car.Drive();
        }

        /// <summary>
        /// TempateMethod (поведенческий)
        /// </summary>
        static void TemplateMethod()
        {
            new BirthdayParty().ArrangeParty();
        }

        /// <summary>
        /// Builder (порождающий)
        /// </summary>
        static void Builder()
        {
            Director director = new Director(new TreeHouseBuilder());
            director.ConstructProduct();
            House house = director.GetHouse();
        }

        /// <summary>
        /// Iterator (поведенческий)
        /// </summary>
        static void Iterator()
        {
            Garage garage = new Garage();
            Console.WriteLine("There are some cars in the garage!");
            foreach (var c in garage)
            {
                Console.WriteLine("{0}{1}", new String(' ', 3), c.Mark);
            }
        }

        /// <summary>
        /// Composite (структурный)
        /// </summary>
        static void Composite()
        {
            IComposite menu = new Menu(){Name = "AllMenu"};

            //IComposite breakfastMenu = new Menu() { Name = "BreakfastMenu" };
            //breakfastMenu.AddComponent(new MenuItem() { Name = "FryAggs", Price = 10 });
            //breakfastMenu.AddComponent(new MenuItem() { Name = "Sosiges", Price = 20 });

            //IComposite dinnerMenu = new Menu() { Name = "DinnerMenu" };
            //dinnerMenu.AddComponent(new MenuItem() { Name = "GarlycSoump", Price = 15 });
            //dinnerMenu.AddComponent(new MenuItem() { Name = "BoiledPotato", Price = 30 });
            
            IComposite supperMenu = new Menu() { Name = "SupperMenu" };
            //supperMenu.AddComponent(new MenuItem() { Name = "Borsch", Price = 18 });
            //supperMenu.AddComponent(new MenuItem() { Name = "Pourige", Price = 17 });
            //supperMenu.AddComponent(new MenuItem() { Name = "Bread", Price = 3 });

            IComposite DesertMenu = new Menu() { Name = "DesertMenu" };
            DesertMenu.AddComponent(new MenuItem() { Name = "Milk", Price = 21 });
            DesertMenu.AddComponent(new MenuItem() { Name = "Biskuit", Price = 23 });

            //IComposite DesertMenuInner = new Menu() { Name = "BiskuitMenuInner" };
            //DesertMenuInner.AddComponent(new MenuItem() { Name = "MilkInner", Price = 21 });
            //DesertMenuInner.AddComponent(new MenuItem() { Name = "BiskuitInner", Price = 21 });
            //DesertMenu.AddComponent(DesertMenuInner);

            supperMenu.AddComponent(DesertMenu);
            //menu.AddComponent(breakfastMenu);
            //menu.AddComponent(dinnerMenu);
            menu.AddComponent(supperMenu);

            //menu.PrintInfo();
            menu.PrintThroghIterator();
        }

        private static void Memento()
        {
            var gamePlayer = new GamePlayer();
            gamePlayer.DisplayGameInfo();
            gamePlayer.PlayGame();
            gamePlayer.DisplayGameInfo();
            gamePlayer.PlayGame();
            gamePlayer.DisplayGameInfo();
            Console.WriteLine("Save game");
            gamePlayer.F5();
            gamePlayer.PlayGame();
            gamePlayer.DisplayGameInfo();
            gamePlayer.PlayGame();
            gamePlayer.DisplayGameInfo();
            Console.WriteLine("Load game");
            gamePlayer.F9();
            gamePlayer.DisplayGameInfo();

        }

    }
}
