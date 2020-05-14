using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.DataAccess
{
    abstract class Iterator<T>
    {
        public abstract void MoveNext();
        public abstract bool HasNext();
        public abstract T CurrentItem();
        public abstract void Reset();
    }

    class BookingDatabaseIterator : Iterator<ListNode>
    {
        BookingDatabase collection;
        int currentIndex;
        ListNode[] currentNodes;
        bool[] isListEmpty;
        int arrayLength;

        public BookingDatabaseIterator(BookingDatabase collection)
        {
            this.collection = collection;
            arrayLength = collection.Rooms.Length;
            currentNodes = new ListNode[arrayLength];
            isListEmpty = new bool[arrayLength];
            currentIndex = 0;
            for (int i=0; i<arrayLength; i++)
            {
                currentNodes[i] = collection.Rooms[i];
            }
        }

        public override ListNode CurrentItem()
        {
            return collection.Rooms[currentIndex];
        }


        public override bool HasNext()
        {
            if (arrayLength == 0) return false;
            return true;
        }

        public override void MoveNext()
        {
            if (!IsNextExisting())
            {
                Reset();
            }

            //currentNodes[currentIndex] = currentNodes[currentIndex].Next;   //było w złym miejscu
            int i = 1;
            while (currentNodes[(currentIndex + i)%arrayLength] == null)
            {            
                i++;
            }
            currentIndex = (currentIndex + i) % arrayLength;
            currentNodes[currentIndex] = currentNodes[currentIndex].Next; // Wystarczylo wstawić tu :)

        }

        public override void Reset()
        {
            currentIndex = 0;
            for (int j = 0; j < arrayLength; j++)
            {
                currentNodes[j] = collection.Rooms[j];
            }
            return;
        }

        private bool IsNextExisting()
        {
            int i = 1;
            while (currentNodes[(currentIndex + i) % arrayLength] == null)
            {
                i++;
                if (i >= arrayLength) return false;
            }
            return true;
        }

    }

    class OysterDatabaseIterator : Iterator<BSTNode>
    {
        OysterDatabase collection;
        BSTNode currentNode;
        List<BSTNode> currentPath;

        public OysterDatabaseIterator(OysterDatabase collection)
        {
            this.collection = collection;
            Reset();
        }

        public override BSTNode CurrentItem()
        { 
            return currentPath.ElementAt(currentPath.Count-1); 
        }

        public override bool HasNext()
        {
            if (currentPath.Count == 1 && currentPath.ElementAt(0).Right == null) return false;  
            if (currentPath.Count == 0) return false;
            return true;
        }

        public override void MoveNext()
        {
            currentNode = currentPath.ElementAt(currentPath.Count - 1).Right;
            currentPath.RemoveAt(currentPath.Count - 1);
            while (currentNode != null)
            {
                currentPath.Add(currentNode);
                currentNode = currentNode.Left;
            }
        }

        public override void Reset()
        {
            currentNode = collection.Reviews;
            currentPath = new List<BSTNode>();
            while (currentNode != null)
            {
                currentPath.Add(currentNode);
                currentNode = currentNode.Left;
            }
        }
    }

    class ShutterStockDatabaseIterator : Iterator<PhotMetadata> // brakowało sprawdzania czy tablica nie jest pusta
    {
        ShutterStockDatabase collection;
        int i, j, k;

        public ShutterStockDatabaseIterator(ShutterStockDatabase collection)
        {
            this.collection = collection;
            this.MoveNext();
            Reset();
        }


        public override PhotMetadata CurrentItem()
        {
            return collection.Photos[i][j][k];
        }

        public override bool HasNext()
        {
            int tmpi = i, tmpj = j, tmpk = k;
            if (!increaseIndexes(ref tmpi,ref tmpj,ref tmpk))
            {
                Reset();
                return false;
            }
            while (
                collection.Photos[tmpi] == null ||
                collection.Photos[tmpi].Length == 0 ||
                collection.Photos[tmpi][tmpj] == null ||
                collection.Photos[tmpi][tmpj].Length == 0 ||
                collection.Photos[tmpi][tmpj][tmpk] == null
                )
            {
                if (!increaseIndexes(ref tmpi,ref tmpj,ref tmpk))
                {
                    Reset();
                    return false;
                }
            }
            return true;
        }


        public override void MoveNext()
        {
            increaseIndexes(ref i,ref j,ref k);

            while ( collection.Photos[i] == null || collection.Photos[i].Length == 0 ||
                    collection.Photos[i][j] == null || collection.Photos[i][j].Length == 0 ||
                    collection.Photos[i][j][k] == null)
            {
                increaseIndexes(ref i,ref j,ref k);
            }

        }

        public override void Reset()
        {
            i = 0;
            j = 0;
            k = 0;
        }


        private bool increaseIndexes(ref int x1,ref int x2,ref int x3)
        {
            if (collection.Photos[x1] == null || collection.Photos[x1].Length == 0 ||
                collection.Photos[x1][x2] == null || collection.Photos[x1][x2].Length == 0)  return false;

            if (x3 + 1 < collection.Photos[x1][x2].Length)
            {
                x3++;
            }
            else
            {
                x3 = 0;
                if (x2 + 1 < collection.Photos[x1].Length)
                {
                    x2++;
                }
                else
                {
                    x2 = 0;
                    if (x1 + 1 < collection.Photos.Length)
                    {
                        x1++;
                    }
                    else
                    {
                        Reset();
                        return false;
                    }
                }
            }
            return true;
        }


    }

    class TripAdvisorDatabaseIterator : Iterator<AttractionData>
    {
        TripAdvisorDatabase collection;
        int currentIndex;

        public TripAdvisorDatabaseIterator(TripAdvisorDatabase collection)
        {           
            this.collection = collection;
            currentIndex = 0;
        }


        public override AttractionData CurrentItem()
        {
            Guid curr = collection.Ids[currentIndex];
            string name=""; 
            for (int i=0; i<collection.Names.Length; i++)
            {
                if (collection.Names[i].ContainsKey(curr))
                {
                    name = collection.Names[i][curr];
                    break;
                }
            }
            return new AttractionData(name, collection.Prices[curr], collection.Ratings[curr], collection.Countries[curr]);


        }

        public override bool HasNext()
        {
            int i = currentIndex + 1;
            while (i != collection.Ids.Length)
            {
                if (isTripValid(i))
                {
                    return true;
                }
                else i++;
            }
            return false;
        }

        public override void MoveNext()
        {
            int i = currentIndex + 1;
            while (i < collection.Ids.Length)
            {
                if (isTripValid(i))
                {
                    currentIndex = i;
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        public override void Reset()
        {
            currentIndex = 0;
        }

        private bool isTripValid(int index)
        {
            if (collection.Prices[collection.Ids[index]] == null) return false;
            if (collection.Ratings[collection.Ids[index]] == null) return false;
            if (collection.Countries[collection.Ids[index]] == null) return false;
            bool isCategoryInDictionary = false;
            {
                for(int i=0; i<collection.Names.Length; i++)
                {
                    if (collection.Names[i].ContainsKey(collection.Ids[index]))
                    {
                        isCategoryInDictionary = true;
                        break;
                    }
                }
            }
            return isCategoryInDictionary;
        }

    }
}
