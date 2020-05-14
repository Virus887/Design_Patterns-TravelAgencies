using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.AdvertisingAgency
{
    abstract class TextOffer : IOffer
    {
        protected ITrip trip;
        protected IReview[] review;
        public virtual void ShowInfoAboutTrip()
        {
            trip.PrintData();
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < review.Length; i++)
            {
                review[i].PrintData();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
    class TextOfferSimple : TextOffer
    {
        public TextOfferSimple(ITravelAgency agency, int n)
        {
            trip = agency.CreateTrip();
            review = new IReview[n];
            for (int i = 0; i < n; i++)
            {
                review[i] = agency.CreateReview();
            }
        }
    }

    class TextOfferTemporary : TextOffer
    {
        int life;

        public TextOfferTemporary(ITravelAgency agency, int n, int life)
        {
            this.trip = agency.CreateTrip();
            this.review = new IReview[n];
            for (int i = 0; i < n; i++)
            {
                review[i] = agency.CreateReview();
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
                Console.WriteLine("This offer is expired");
            }
        }
    }
}
