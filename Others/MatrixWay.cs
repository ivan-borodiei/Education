using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Others
{
    struct Point
    {
        public int i { get; set; }
        public int j { get; set; }        
    }

    class MatrixWay
    {
        int[,] mtx = { { 3, 5, 5 }, { 2, 1, 4 }, { 7, 2, 3 } };
        //int[,] mtx = { { 4, 3, 1, 2, 4 }, { 2, 8, 7, 9, 5 }, { 7, 2, 1, 8, 7 }, { 4, 3, 2, 5, 6 }, { 5, 4, 3, 4, 7 } };


        List<List<Point>> pathList = new List<List<Point>>();

        public void ShowMatrix()
        {
            Console.WriteLine("Income matrix:");
            for (int i = 0; i < mtx.GetLength(0); i++)
            {
                for (int j = 0; j < mtx.GetLength(1); j++)                
                    Console.Write(String.Format("{0,3}", mtx[i, j]));                
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ShowPathLists()
        {
            Console.WriteLine("All the ways:");
            foreach (var list in pathList)
            {
                foreach(var p in list)                
                    Console.Write("{0,3}", mtx[p.i, p.j]);
                
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ShowLongestPath()
        {
            Console.WriteLine("The longest way is:");
            foreach (var list in pathList.Where(x => x.Count == pathList.Max(y => y.Count)))
            {
                foreach(var p in list)                
                    Console.Write("{0,3}", mtx[p.i, p.j]);
                
                Console.WriteLine();
            }

             
            Console.WriteLine();
        }

        public void FillWays(int i, int j)
        {
            List<Point> path = new List<Point>();
            path.Add(new Point() { i = i, j = j });            
            GetLongestWay(i, j, path);
        }

        public void GetLongestWay(int i, int j, List<Point> incomePath)
        {
            pathList.Add(incomePath);
            //go up
            if ((i-1 >= 0) && (mtx[i-1, j] == mtx[i,j]+1))
            {
                List<Point> newPath = ClonePath(incomePath);  
                newPath.Add(new Point() { i = i - 1, j = j });
                GetLongestWay(i - 1, j, newPath);
            }

            //go right
            if ((j + 1 < mtx.GetLength(1)) && (mtx[i, j + 1] == mtx[i, j] + 1))
            {
                List<Point> newPath = ClonePath(incomePath);  
                newPath.Add(new Point() { i = i, j = j + 1 });
                GetLongestWay(i, j + 1, newPath);
            }

            //go down
            if ((i + 1 < mtx.GetLength(0)) && (mtx[i + 1, j] == mtx[i, j] + 1))
            {
                List<Point> newPath = ClonePath(incomePath);  
                newPath.Add(new Point() { i = i + 1, j = j });
                GetLongestWay(i + 1, j, newPath);
            }

            //go left
            if ((j - 1 >= 0) && (mtx[i, j-1] == mtx[i, j] + 1))
            {
                List<Point> newPath = ClonePath(incomePath);                
                newPath.Add(new Point() { i = i, j = j-1 });
                GetLongestWay(i, j-1, newPath);
            }
        }

        private List<Point> ClonePath(List<Point> list)
        {
            return ((Point[])list.ToArray().Clone()).ToList();
        }
    }
}
