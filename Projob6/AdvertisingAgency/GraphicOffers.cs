using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis

namespace TravelAgencies.AdvertisingAgency
{
    abstract class GraphicOffer : IOffer
    {
        protected ITrip trip;
        protected IPhoto[] photo;
        public virtual void ShowInfoAboutTrip()
        {
            trip.PrintData();
            Console.WriteLine();
            Console.WriteLine();
            for (int i=0; i<photo.Length; i++)
            {
                photo[i].PrintData();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
    class GraphicOfferSimple : GraphicOffer
    {
        public GraphicOfferSimple(ITravelAgency agency, int n)
        {
            this.trip = agency.CreateTrip();
            this.photo = new IPhoto[n];
            for (int i=0; i<n; i++)
            {
                photo[i] = agency.CreatePhoto();
            }
        }

    }
    class GraphicOfferTemporary : GraphicOffer
    {
        int life;

        public GraphicOfferTemporary(ITravelAgency agency,int n, int life)
        {
            this.trip = agency.CreateTrip();
            this.photo = new IPhoto[n];
            for (int i = 0; i < n; i++)
            {
                photo[i] = agency.CreatePhoto();
            }
            this.life = life;
        }

        public override void ShowInfoAboutTrip()
        {
            if (life-- > 0)
            {
                base.ShowInfoAboutTrip();
            }
            else
            {
                Console.WriteLine("\n\n\nThis offer is expired\n\n\n");
            }
        }
    }
}


