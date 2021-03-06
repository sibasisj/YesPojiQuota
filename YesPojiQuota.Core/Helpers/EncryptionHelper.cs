﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace YesPojiQuota.Core.Helpers
{
    public static class EncryptionHelper
    {
        static String strDescriptor = "LOCAL=user";

        public static async Task<String> SampleProtectAsync(
            String strMsg,
            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8)
        {
            // Create a DataProtectionProvider object for the specified descriptor.
            DataProtectionProvider provider = new DataProtectionProvider(strDescriptor);

            // Encode the plaintext input message to a buffer.
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);

            // Encrypt the message.
            IBuffer buffProtected = await provider.ProtectAsync(buffMsg);

            // Execution of the SampleProtectAsync function resumes here
            // after the awaited task (Provider.ProtectAsync) completes.
            return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf16BE, buffProtected);
        }

        public static async Task<String> SampleUnprotectData(
            String stringProtected,
            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8)
        {
            // Create a DataProtectionProvider object.
            DataProtectionProvider provider = new DataProtectionProvider();

            IBuffer buffProtected = CryptographicBuffer.ConvertStringToBinary(stringProtected, BinaryStringEncoding.Utf16BE);

            // Decrypt the protected message specified on input.
            IBuffer buffUnprotected = await provider.UnprotectAsync(buffProtected);

            // Execution of the SampleUnprotectData method resumes here
            // after the awaited task (Provider.UnprotectAsync) completes
            // Convert the unprotected message from an IBuffer object to a string.
            String strClearText = CryptographicBuffer.ConvertBinaryToString(encoding, buffUnprotected);

            // Return the plaintext string.
            return strClearText;
        }

        public static string AES_Encrypt(string input, string pass)
        {
            SymmetricKeyAlgorithmProvider sap = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);
            CryptographicKey aes;
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash hash_AES = hap.CreateHash();

            string encrypted = "";
            try
            {
                byte[] hash = new byte[32];
                hash_AES.Append(CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(pass)));
                byte[] temp;
                CryptographicBuffer.CopyToByteArray(hash_AES.GetValueAndReset(), out temp);

                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);

                aes = sap.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(hash));

                IBuffer buffer = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(input));
                encrypted = CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(aes, buffer, null));

                return encrypted;
            }
            catch
            {
                return null;
            }
        }

        public static string AES_Decrypt(string input, string pass)
        {
            SymmetricKeyAlgorithmProvider sap = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);
            CryptographicKey aes;
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash hash_AES = hap.CreateHash();

            string decrypted = "";
            try
            {
                byte[] hash = new byte[32];
                hash_AES.Append(CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(pass)));
                byte[] temp;
                CryptographicBuffer.CopyToByteArray(hash_AES.GetValueAndReset(), out temp);

                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);

                aes = sap.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(hash));

                IBuffer Buffer = CryptographicBuffer.DecodeFromBase64String(input);
                byte[] Decrypted;
                CryptographicBuffer.CopyToByteArray(CryptographicEngine.Decrypt(aes, Buffer, null), out Decrypted);
                decrypted = Encoding.UTF8.GetString(Decrypted, 0, Decrypted.Length);

                return decrypted;
            }
            catch
            {
                return null;
            }
        }
    }
}
