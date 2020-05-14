using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.DataAccess
{
    interface IIterableCollection<T>
    {
        Iterator<T> GetIterator();
    }

    class ListNode
    {
        public ListNode Next { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }//Encrypted
        public string Price { get; set; }//Encrypted
    }

    class BookingDatabase : IIterableCollection<ListNode>
	{
        public ListNode[] Rooms { get; set; }

        public Iterator<ListNode> GetIterator()
        {
            return new BookingDatabaseIterator(this);
        }
    }
}
