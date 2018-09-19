using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Security
{
    public interface ISecurity
    {
        string Encrypt(byte[] source);
        string Decrypt(byte[] encrypted);
    }
}
