using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Patterns
{
    class Garage: IEnumerable<Car>
    {
        Car[] cars = new Car[] { new Car() { Mark = "Fiat" }, new Car() { Mark = "Huyndai" } };

        public IEnumerator<Car> GetEnumerator()
        {            
            return new GarageIterator(cars);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class GarageIterator: IEnumerator<Car>
    {
        int index;
        Car[] cars;
        public GarageIterator(Car[] cars)
        {
            Reset();
            this.cars = cars;
        }

        public Car Current
        {
            get { return cars[index]; }
        }

        public void Dispose()
        {
            Reset();
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (index < cars.Count()-1)
            {
                index++;
                return true;
            }
            else
            {
                Reset();
                return false;
            }
        }

        public void Reset()
        {
            index = -1;
        }
    }
    
}
