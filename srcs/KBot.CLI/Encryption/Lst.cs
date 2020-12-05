using System;
using System.Collections.Generic;
using System.Linq;

namespace KBot.CLI.Encryption
{
    public static class Lst
    {
        public static byte[] Decrypt(byte[] array)
        {
            List<byte> result = new List<byte>();
            if (array.Length > 0)
            {
                int lines = BitConverter.ToInt32(array.Take(4).ToArray(), 0);
                int pos = 4;

                for (int i = 0; i < lines; i++)
                {
                    int len = BitConverter.ToInt32(array.Skip(pos).Take(4).ToArray(), 0);
                    pos += 4;
                    byte[] bytes = array.Skip(pos).Take(len).ToArray();
                    pos += len;
                    result.AddRange(bytes.Select(b => (byte)(b ^ 0x1)));
                    result.Add((byte)'\n');
                }
            }

            return result.ToArray();
        }

    }
}