using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns
{
    interface IComponent
    {
        string Name { get; }
        int Price { get; }
    }

    class Product : IComponent
    {
        int price;
        string name;

        public int Price
        {
            get
            {
                return price;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Product(string name, int price)
        {
            this.price = price;
        }
    }

    class Box: IComponent
    {
        string name;
        List<IComponent> componentList = new List<IComponent>();

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Price
        {
            get
            {
                return componentList.Sum(c => c.Price);
            }
        }

        public Box(string name)
        {
            this.name = name;
        }

        public void Add(IComponent component)
        {
            componentList.Add(component);
        }

    }

    //---------------------------------------------------------------------------------------------------
    interface IComposite
    {
        string Name { get; set; }
        int Price { get; set; }
        void AddComponent(IComposite component);
        void RemoveComponent(IComposite component);

        IEnumerator<IComposite> GetIterator();

        void PrintInfo(int spaceCount = 0);
        void PrintThroghIterator();
    }

    class Menu: IComposite
    {
        public string Name { get; set; }
        public int Price { get; set; }
        private IList<IComposite> ComponentList;

        IEnumerator<IComposite> iterator;
        public IEnumerator<IComposite> GetIterator()
        {
            if (iterator == null)
                iterator = new MenuIterator(ComponentList.GetEnumerator());
            return iterator;
        }

        public Menu()
        {
            ComponentList = new List<IComposite>();            
        }

        public void AddComponent(IComposite component)
        {
            ComponentList.Add(component);
        }

        public void RemoveComponent(IComposite component)
        {
            ComponentList.Remove(component);
        }

        public void PrintInfo(int spaceCount)
        {
            if (ComponentList.Count > 0)
            {
                Console.WriteLine("{0}Menu: {1}", new string(' ', spaceCount), Name);
                spaceCount += 3;
            }
            foreach (IComposite c in ComponentList)
            {               
                c.PrintInfo(spaceCount);
            }
        }

        public void PrintThroghIterator()
        {
            Console.WriteLine("Name: {0}", Name);
            IEnumerator<IComposite> iterator = GetIterator();
            while (iterator.MoveNext())
                Console.WriteLine("Name: {0}", iterator.Current.Name);
        }
    }

    class MenuItem: IComposite
    {
        public string Name { get; set; }

        public int Price { get; set; }        

        public void AddComponent(IComposite component)
        {
            throw new NotImplementedException();
        }

        public void RemoveComponent(IComposite component)
        {
            throw new NotImplementedException();
        }

        public void PrintInfo(int spaceCount)
        {
            Console.WriteLine("{0}MenuItem: {1}; Price = {2:C}", new string(' ', spaceCount), Name, Price);
        }

        public IEnumerator<IComposite> GetIterator()
        {
            return new NullIterator();
        }

        public void PrintThroghIterator() { }
    }

    class MenuIterator : IEnumerator<IComposite>
    {        
        Stack<IEnumerator<IComposite>> iteratorList;

        IEnumerator<IComposite> iterator;
        public MenuIterator(IEnumerator<IComposite> iterator)
        {                        
            iteratorList = new Stack<IEnumerator<IComposite>>();
            iteratorList.Push(iterator);
        }

        public IComposite Current
        {
            get { return iterator.Current; }
        }

        public void Dispose()
        {
            Reset();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {            
            if (iteratorList.Count > 0)
            {
                iterator = iteratorList.Peek();
                bool moveNext = iterator.MoveNext();
                if (moveNext)
                {
                    IEnumerator<IComposite> tmp = iterator.Current.GetIterator();
                    if (!(tmp is NullIterator))                    
                        iteratorList.Push(tmp);                                        
                    return true;
                }
                else
                {
                    iteratorList.Pop();                    
                    return MoveNext();
                }
            }
            else
                return false;
        }

        public void Reset()
        {
            iterator.Reset();
        }
    }

    class NullIterator : IEnumerator<IComposite>
    {        
        public IComposite Current
        {
            get { return null; }
        }

        public void Dispose()
        {
            Reset();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            return false;
        }

        public void Reset()
        { }
    }

}
