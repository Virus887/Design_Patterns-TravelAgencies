using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.DataAccess
{
	class PhotMetadata 
	{
		public string Name { get; set; }
		public string Camera { get; set; }
		public double[] CameraSettings { get; set; }
		public DateTime Date { get; set; }
		public string WidthPx { get; set; }//Encrypted
		public string HeightPx { get; set; }//Encrypted
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}

	class ShutterStockDatabase : IIterableCollection<PhotMetadata>
	{
		public PhotMetadata[][][] Photos;

        public Iterator<PhotMetadata> GetIterator()
        {
            return new ShutterStockDatabaseIterator(this);
        }
    }
}
