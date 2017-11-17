using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Proyecto_Final
{
    public class RLE
    {
        struct Register
        {
            public char value;
            public int ammount;
        }
        public string Cipher(string s)
        {
            char current = s[0];
            int currentCounter = 0;
            List<Register> Registers = new List<Register>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == current)
                {
                    currentCounter++;
                    if (i + 1 == s.Length)
                    {
                        Register r = new Register();
                        r.value = current;
                        r.ammount = currentCounter;
                        Registers.Add(r);
                    }
                }
                else
                {
                    Register r = new Register();
                    r.value = current;
                    r.ammount = currentCounter;
                    Registers.Add(r);
                    current = s[i];
                    currentCounter = 1;
                    if(i+1 == s.Length)
                    {
                        Register r2 = new Register();
                        r2.value = current;
                        r2.ammount = currentCounter;
                        Registers.Add(r2);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach(Register r in Registers)
            {
                sb.Append(r.ammount);
                sb.Append("|");
                sb.Append(r.value);
                sb.Append("|");
            }
            return sb.ToString();
        }
        public string Decipher(string s)
        {
            string[] pairs = s.Split('|');
            List<Register> Registers = new List<Register>();
            for (int i = 0; i < pairs.Length-1; i += 2)
            {
                Register r = new Register();
                r.ammount = int.Parse(pairs[i]);
                r.value = char.Parse(pairs[i + 1]);
                Registers.Add(r);
            }
            StringBuilder sb = new StringBuilder();
            foreach (Register r in Registers)
            {
                for(int i = 0; i < r.ammount; i++)
                {
                    sb.Append(r.value);
                }
            }
            return sb.ToString();
        }

        public async Task<object> CipherD(object input)
        {
            return Cipher(input.ToString()).ToString();
        }
        public async Task<object> DecipherD(object input)
        {
            return Decipher(input.ToString()).ToString();
        }
    }
}
