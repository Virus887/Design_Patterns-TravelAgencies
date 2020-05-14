using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.AdvertisingAgency
{
    class OfferWebsite
    {
        List<IOffer> offers;

        public OfferWebsite()
        {
            this.offers = new List<IOffer>();
        }

        public void AddOffer(IOffer offer)
        {
            offers.Add(offer);
        }

        public void Present()
        {
            foreach(IOffer offer in offers)
            {
                offer.ShowInfoAboutTrip();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public void Clear()
        {
            offers = new List<IOffer>();
        }

    }

    class GraphicAgency
    {
        public IOffer createSimpleOffer(ITravelAgency agency, int n)
        {
            return new GraphicOfferSimple(agency, n);
        }
        public IOffer createTemporaryOffer(ITravelAgency agency, int n, int life)
        {
            return new GraphicOfferTemporary(agency, n, life);
        }
    }

    class TextAgency 
    {
        public IOffer createSimpleOffer(ITravelAgency agency, int n)
        {
            return new TextOfferSimple(agency, n);        
        }
        public IOffer createTemporaryOffer(ITravelAgency agency, int n, int life)
        {
            return new TextOfferTemporary(agency, n, life);
        }
    }

    interface IOffer
    {
        void ShowInfoAboutTrip();
    }


}
