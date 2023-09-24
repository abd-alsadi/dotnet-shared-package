﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkCommon.Shareds
{
   public class EncDecManagement
    {
        public class AsymanticAlgorithm
        {
            
        }
        public class SymanticAlgorithm
        {

        }
        public class DES : AsymanticAlgorithm
        {
            public static string encryptData(string data, string keyX)
            {
                byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
                byte[] inputByteArray;
                try
                {
                    byte[] key = Encoding.UTF8.GetBytes(keyX); //Encryption Key
                    // DESCryptoServiceProvider is a cryptography class defind in c#.
                    DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                    inputByteArray = Encoding.UTF8.GetBytes(data);
                    MemoryStream Objmst = new MemoryStream();
                    CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                    Objcs.FlushFinalBlock();
                    return Convert.ToBase64String(Objmst.ToArray());//encrypted string
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            public static string decryptData(string data, string keyX)
            {
                byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
                byte[] inputByteArray = new byte[data.Length];
                try
                {
                    byte[] key = Encoding.UTF8.GetBytes(keyX);
                    DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(data);
                    MemoryStream Objmst = new MemoryStream();
                    CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                    Objcs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    return encoding.GetString(Objmst.ToArray());
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        public class AES : SymanticAlgorithm
        {

            public static string encryptData(string data, string key)
            {
                RijndaelManaged objrij = new RijndaelManaged();
                //set the mode for operation of the algorithm
                objrij.Mode = CipherMode.CBC;
                //set the padding mode used in the algorithm.
                objrij.Padding = PaddingMode.PKCS7;
                //set the size, in bits, for the secret key.
                objrij.KeySize = 0x80;
                //set the block size in bits for the cryptographic operation.
                objrij.BlockSize = 0x80;
                //set the symmetric key that is used for encryption & decryption.
                byte[] passBytes = Encoding.UTF8.GetBytes(key);
                //set the initialization vector (IV) for the symmetric algorithm
                byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                //Creates symmetric AES object with the current key and initialization vector IV.
                ICryptoTransform objtransform = objrij.CreateEncryptor();
                byte[] textDataByte = Encoding.UTF8.GetBytes(data);
                //Final transform the test string.
                return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
            }


            public static string decryptData(string data, string key)
            {
                RijndaelManaged objrij = new RijndaelManaged();
                objrij.Mode = CipherMode.CBC;
                objrij.Padding = PaddingMode.PKCS7;
                objrij.KeySize = 0x80;
                objrij.BlockSize = 0x80;
                byte[] encryptedTextByte = Convert.FromBase64String(data);
                byte[] passBytes = Encoding.UTF8.GetBytes(key);
                byte[] EncryptionkeyBytes = new byte[0x10];
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
                return Encoding.UTF8.GetString(TextByte);  //it will return readable string
            }
        }
    }
}
