using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;
using System.Threading.Tasks;

public static class CurrencyHandler
{
    // AES encryption key, must be 32 chars
    private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("pRGF5zmZUKtYjtFmMxn9bc5D5mP9j3kQ");


    private const string CurrencyKey = "HeartsCurrency";

    public static void SaveCurrency(int currency)
    {
        // local save
        PlayerPrefs.SetString(CurrencyKey, EncryptInt(currency));
        PlayerPrefs.Save();
    }

    // local currency load, not used
    
    public static int LoadCurrency()
    {
        string encryptedCurrency = PlayerPrefs.GetString(CurrencyKey, EncryptInt(0));
        return DecryptInt(encryptedCurrency);
    }

    // currently only used on application exit as a backup
    public static async void SaveCurrencyCloud()
    {
        await CloudSaveWrapper.Save<int>(CurrencyKey, LoadCurrency());
    }

    // tries to load online, then loads from local if fails
    public static async Task<int> LoadCurrencyCloud()
    {
        int currency = 0;

        // cloud load
        bool cloudLoadSuccessful = false;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            try
            {
                currency = await CloudSaveWrapper.Load<int>(CurrencyKey);
                cloudLoadSuccessful = true;
            }
            catch (Exception e)
            {
                Debug.Log("Failed to load from cloud: " + e.Message);
                cloudLoadSuccessful = false;
            }
        }

        // If cloud load fails, load from local
        if (!cloudLoadSuccessful)
        {
            string encryptedCurrency = PlayerPrefs.GetString(CurrencyKey);
            currency = DecryptInt(encryptedCurrency);
        }

        return currency;
    }

    // AES encryption for an integer value
    public static string EncryptInt(int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);

        using (var aes = Aes.Create())
        {
            aes.Key = EncryptionKey;
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] encrypted = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                byte[] combined = new byte[aes.IV.Length + encrypted.Length];
                Array.Copy(aes.IV, 0, combined, 0, aes.IV.Length);
                Array.Copy(encrypted, 0, combined, aes.IV.Length, encrypted.Length);

                return Convert.ToBase64String(combined);
            }
        }
    }

    // AES decryption for an int value
    public static int DecryptInt(string encryptedValue)
    {
        byte[] combined = Convert.FromBase64String(encryptedValue);

        using (var aes = Aes.Create())
        {
            aes.Key = EncryptionKey;
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherText = new byte[combined.Length - iv.Length];

            Array.Copy(combined, iv, iv.Length);
            Array.Copy(combined, iv.Length, cipherText, 0, cipherText.Length);

            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                return BitConverter.ToInt32(decrypted, 0);
            }
        }
    }
}
