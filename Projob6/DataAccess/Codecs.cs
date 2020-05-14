using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Damian Bis


namespace TravelAgencies.DataAccess
{

    abstract class CodecBase
    {
        protected string data;
        public CodecBase(string s)
        {
            this.data = s;
        }
        public abstract string Code();
    }

    class FrameCodec : CodecBase
    {
        protected int n;
        public FrameCodec(string s, int n) : base(s)
        {
            if (n > 9 || n < -9) throw new ArgumentException("Unavailable frame size");            
            this.n = n;
        }
        public override string Code()
        {
            if (n>=0)
            {
                StringBuilder sb = new StringBuilder(data);
                for (int i = n; i > 0; i--)
                {
                    sb.Insert(0, i.ToString(), 1);
                    sb.Append(i.ToString());
                }
                return sb.ToString();
            }
            else // n<0
            {
                int m = -n;
                StringBuilder sb = new StringBuilder(data);
                sb.Remove(0, m);
                sb.Remove(data.Length - 2 * m, m);
                return sb.ToString();
            }

        }
    }

    class PushCodec : CodecBase
    {
        protected int n;
        public PushCodec(string s, int n) : base(s) { this.n = n; }
        public override string Code()
        {
            int m = n % data.Length;
            if (n >= 0)
            {
                return data.Substring(data.Length - m) + data.Substring(0, data.Length - m);
            }
            else
            {
                return data.Substring(-m) + data.Substring(0, -m);
            }
        }
    }

    class CesarCodec : CodecBase
    {
        protected int n;
        public CesarCodec(string s, int n) : base(s) { this.n = n; }
        public override string Code()
        {
            StringBuilder sb = new StringBuilder();
            int tmp;
            for (int i =0; i< data.Length; i++)
            {
                tmp = int.Parse(new string(data[i], 1));
                tmp = (tmp + n) % 10;
                sb.Append(tmp);
            }
            return sb.ToString();
        }
    }

    class ReverseCodec : CodecBase
    {
        public ReverseCodec(string s) : base(s) { }
        public override string Code()
        {

            char[] tmp = data.ToCharArray();
            Array.Reverse(tmp);
            return new string(tmp);
        }
    }

    class SwapCodec : CodecBase
    {
        public SwapCodec(string s) : base(s) { }
        public override string Code()
        {
            StringBuilder sb = new StringBuilder();
            char[] evenCharacters = new char[data.Length / 2 + data.Length % 2];
            char[] oddCharacters = new char[data.Length / 2];
            for (int i = 0; i < data.Length; i++)
            {
                if (i % 2 == 0)
                {
                    evenCharacters[i / 2] = data[i];
                }
                else
                {
                    oddCharacters[i / 2] = data[i];
                }
            }
            for (int i = 0; i < oddCharacters.Length; i++)
            {
                sb.Append(oddCharacters[i]);
                sb.Append(evenCharacters[i]);
            }
            if (oddCharacters.Length < evenCharacters.Length)
            {
                sb.Append(evenCharacters[evenCharacters.Length - 1]);
            }
            return sb.ToString();
        }
    }




}
