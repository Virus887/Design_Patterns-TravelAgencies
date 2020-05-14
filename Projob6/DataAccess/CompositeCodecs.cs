using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.DataAccess
{
    class ShutterDecoder
    {
        public string Decode(string s)
        {
            string str = new CesarCodec(new FrameCodec(new PushCodec(new ReverseCodec(s).Code(), 3).Code(), -1).Code(), -4).Code();
            return str;
        }

    }
    class BookingDecoder
    {
        public string Decode(string s)
        {
            string str = new FrameCodec( new ReverseCodec( new CesarCodec (new SwapCodec(s).Code(), 1).Code()).Code() , -2).Code();
            return str;
        }
    }
    class TripAdvisorDecoder
    {
        public string Decode(string s)
        {
            string str = new PushCodec(new FrameCodec( new SwapCodec(new PushCodec(s, -3).Code()).Code() , -2).Code(), -3).Code();
            return str;
        }
    }
}
