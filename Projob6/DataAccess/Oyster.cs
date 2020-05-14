using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis

namespace TravelAgencies.DataAccess
{
	class BSTNode
	{
		public string Review { get; set; }
		public string UserName { get; set; }
		public BSTNode Left { get; set; }
		public BSTNode Right { get; set; }
	}
	class OysterDatabase : IIterableCollection<BSTNode>
	{
		public BSTNode Reviews { get; set; }

        public Iterator<BSTNode> GetIterator()
        {
            return new OysterDatabaseIterator(this);
        }
    }
}
