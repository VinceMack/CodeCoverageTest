using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Security.Cryptography;

public class JsonDataService : IDataService
{

    public bool SaveData<T>(string relativePath, T data, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            // Delete old save if overwriting
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            // Create the new save
            using FileStream stream = File.Create(path);
            if(encrypted)
            {
                WriteEncryptedData(data, stream);
            }
            else
            {
                stream.Close();
                // Write data to new save
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
            }
            // Indicate save was succesful
            return true;
        }
        catch(Exception e)
        {
            // Print error
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            //Indicate save was unsuccessful
            return false;
        }
    }

    private void WriteEncryptedData<T>(T data, FileStream stream)
    {
        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(Constants.ENCRYPTION_KEY);
        aesProvider.IV = Convert.FromBase64String(Constants.ENCRYPTION_IV);
        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(
            stream,
            cryptoTransform,
            CryptoStreamMode.Write
        );

        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));
    }

    public T LoadData<T>(string relativePath, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;
        
        // If load file doesn't exist, print error and throw exception
        if(!File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}. File does not exist.");
            throw new FileNotFoundException();
        }

        try
        {
            T data;
            if(encrypted)
            {
                data = ReadEncryptedData<T>(path);
            }
            else
            {
                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            return data;
        }
        catch(Exception e)
        {
            Debug.LogError($"Cannot load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }

    private T ReadEncryptedData<T>(string path)
    {
        byte[] fileBytes = File.ReadAllBytes(path);
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(Constants.ENCRYPTION_KEY);
        aesProvider.IV = Convert.FromBase64String(Constants.ENCRYPTION_IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
            aesProvider.Key,
            aesProvider.IV
        );
        using MemoryStream decryptionStream = new MemoryStream(fileBytes);
        using CryptoStream cryptoStream = new CryptoStream(
            decryptionStream,
            cryptoTransform,
            CryptoStreamMode.Read
        );
        using StreamReader reader = new StreamReader(cryptoStream);

        string result = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<T>(result);
    }
}
