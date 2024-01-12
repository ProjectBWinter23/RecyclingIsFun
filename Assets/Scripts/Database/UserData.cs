using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Database
{
    /// <summary>
    /// User Data Class, Local storage support
    /// </summary>

    [Serializable]
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserData() { }

        public void Save()
        {
            // Serialize the ConfigurationBase data to JSON.
            string userData = JsonConvert.SerializeObject(this);

            // Write the JSON string to the file.
            File.WriteAllText(DatabaseManager.UserFilePath, userData);

            Debug.Log("JSON data written to: " + DatabaseManager.UserFilePath);
        }

        public static UserData Get()
        {
            string userData = File.ReadAllText(DatabaseManager.UserFilePath);
            return JsonConvert.DeserializeObject<UserData>(userData);
        }

        public static void Save_Example()
        {
            UserData data = new UserData()
            {
                FirstName = "Firstname",
                LastName = "LastName",
                Email = "Email@gmail.com",
                Password = "P@ssw0rd",
                Age = 12,
            };

            // Serialize the ConfigurationBase data to JSON.
            string json = JsonConvert.SerializeObject(data);

            // Write the JSON string to the file.
            File.WriteAllText(DatabaseManager.UserFilePath, json);

            Debug.Log("JSON data written to: " + DatabaseManager.UserFilePath);
        }
    }
}
