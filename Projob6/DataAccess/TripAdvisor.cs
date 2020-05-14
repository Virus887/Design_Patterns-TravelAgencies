using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.DataAccess
{ 

    class AttractionData
    {
        public AttractionData(string name, string prices, string ratings, string countries)
        {
            Name = name;
            Prices = prices;
            Ratings = ratings;
            Countries = countries;
        }

        public string Name { get; set; }
        public string Prices { get; set; }//Encrypted
        public string Ratings { get; set; }//Encrypted
        public string Countries { get; set; }
    }

	class TripAdvisorDatabase : IIterableCollection<AttractionData>
	{
		public Guid[] Ids;
		public Dictionary<Guid, string>[] Names { get; set; }
		public Dictionary<Guid, string> Prices { get; set; }//Encrypted
		public Dictionary<Guid, string> Ratings { get; set; }//Encrypted
		public Dictionary<Guid, string> Countries { get; set; }


        public Iterator<AttractionData> GetIterator()
        {
            return new TripAdvisorDatabaseIterator(this);
        }
    }

}

