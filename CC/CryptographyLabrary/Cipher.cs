using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public interface Cipher
    {
        char[] Alphabet { get; set; }

        string Encryption(string text);
        string Decryption(string text);
    }
}