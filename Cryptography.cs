using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAppLogs
{
    public static class Cryptography
    {
        private static string IV = "1a1a1a1a1a1a1a1a";
        private static string Key = "1a1a1a1a1a1a1a1a1a1a1a1a1a1a1a13";

        public static string Encrypt(string ValueToLock)
        {
            byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(ValueToLock);
            AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
            endec.BlockSize = 128;
            endec.KeySize = 256;
            endec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            endec.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            endec.Padding = PaddingMode.PKCS7;
            endec.Mode = CipherMode.CBC;
            ICryptoTransform icrypt = endec.CreateEncryptor(endec.Key, endec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();
            return Convert.ToBase64String(enc);
        }

        public static string Decrypt(string ValueToUnlock)
        {
            byte[] textbytes = Convert.FromBase64String(ValueToUnlock);
            AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
            endec.BlockSize = 128;
            endec.KeySize = 256;
            endec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            endec.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            endec.Padding = PaddingMode.PKCS7;
            endec.Mode = CipherMode.CBC;
            ICryptoTransform icrypt = endec.CreateDecryptor(endec.Key, endec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();
            return System.Text.ASCIIEncoding.ASCII.GetString(enc);
        }

        public static void EncryptFile(string inputFile, string outputFile)
        {
            //D:\MyFolder\Myfile
            try
            {
                string password = @"myKey123";
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);
                FileStream fsIn = new FileStream(inputFile, FileMode.Open);
                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);
                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {

            }
        }

        public static void DecryptFile(string inputFile, string outputFile)
        {
            string password = @"myKey123";
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            RijndaelManaged RMCrypto = new RijndaelManaged();
            CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);
            FileStream fsOut = new FileStream(outputFile, FileMode.Create);
            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);
            fsOut.Close();
            cs.Close();
            fsCrypt.Close();
        }

        //private static int _iterations = 2;
        //private static int _keySize = 256;

        //private static string _hash = "SHA1";
        //private static string _salt = "aselrias38490a32"; // Random
        //private static string _vector = "8947az34awl34kjq"; // Random


        //public static string Encrypt(string value, string password)
        //{
        //    return Encrypt<AesManaged>(value, password);
        //}
        //public static string Encrypt<T>(string value, string password)
        //        where T : SymmetricAlgorithm, new()
        //{
        //    byte[] vectorBytes = GetBytes<ASCIIEncoding>(_vector);
        //    byte[] saltBytes = GetBytes<ASCIIEncoding>(_salt);
        //    byte[] valueBytes = GetBytes<UTF8Encoding>(value);

        //    byte[] encrypted;
        //    using (T cipher = new T())
        //    {
        //        PasswordDeriveBytes _passwordBytes =
        //            new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
        //        byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

        //        cipher.Mode = CipherMode.CBC;

        //        using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
        //        {
        //            using (MemoryStream to = new MemoryStream())
        //            {
        //                using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
        //                {
        //                    writer.Write(valueBytes, 0, valueBytes.Length);
        //                    writer.FlushFinalBlock();
        //                    encrypted = to.ToArray();
        //                }
        //            }
        //        }
        //        cipher.Clear();
        //    }
        //    return Convert.ToBase64String(encrypted);
        //}

        //public static string Decrypt(string value, string password)
        //{
        //    return Decrypt<AesManaged>(value, password);
        //}

        //public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        //{
        //    byte[] vectorBytes = GetBytes<ASCIIEncoding>(_vector);
        //    byte[] saltBytes = GetBytes<ASCIIEncoding>(_salt);
        //    byte[] valueBytes = Convert.FromBase64String(value);

        //    byte[] decrypted;
        //    int decryptedByteCount = 0;

        //    using (T cipher = new T())
        //    {
        //        PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
        //        byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

        //        cipher.Mode = CipherMode.CBC;

        //        try
        //        {
        //            using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
        //            {
        //                using (MemoryStream from = new MemoryStream(valueBytes))
        //                {
        //                    using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
        //                    {
        //                        decrypted = new byte[valueBytes.Length];
        //                        decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return String.Empty;
        //        }

        //        cipher.Clear();
        //    }
        //    return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        //}
    }
}
