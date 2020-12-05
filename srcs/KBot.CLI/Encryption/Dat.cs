using System.Collections.Generic;

namespace KBot.CLI.Encryption
{
    public static class Dat
    {
        private static readonly byte[] CryptoArray = { 0x00, 0x20, 0x2D, 0x2E, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x0A, 0x00 };
        
        public static byte[] Decrypt(byte[] array)
        {
            var decrypted = new List<byte>();
            int currentIndex = 0;

            while (currentIndex < array.Length)
            {
                byte currentByte = array[currentIndex];
                currentIndex++;
                if (currentByte == 0xFF)
                {
                    decrypted.Add(0xD);
                    continue;
                }

                int validate = currentByte & 0x7F;

                if ((currentByte & 0x80) != 0)
                {
                    for (; validate > 0; validate -= 2)
                    {
                        if (currentIndex >= array.Length)
                        {
                            break;
                        }

                        currentByte = array[currentIndex];
                        currentIndex++;

                        byte firstByte = CryptoArray[(currentByte & 0xF0) >> 4];
                        decrypted.Add(firstByte);

                        if (validate <= 1)
                        {
                            break;
                        }

                        byte secondByte = CryptoArray[currentByte & 0xF];

                        if (secondByte == 0)
                        {
                            break;
                        }

                        decrypted.Add(secondByte);
                    }
                }
                else
                {
                    for (; validate > 0; --validate)
                    {
                        if (currentIndex >= array.Length)
                        {
                            break;
                        }

                        currentByte = array[currentIndex];
                        currentIndex++;

                        decrypted.Add((byte)(currentByte ^ 0x33));
                    }
                }
            }

            return decrypted.ToArray();
        }
    }
}