using System.IO;
using UnityEngine;

namespace Assets.Scripts.Database
{
    /// <summary>
    /// Local Database Manager
    /// </summary>
    internal class DatabaseManager : MonoBehaviour
    {
        // Intializes path in persistant directory on user's machine.
        public static string UserFilePath = Path.Combine(Application.persistentDataPath.Replace('/', System.IO.Path.DirectorySeparatorChar), "UserData.json");
        public static string WasteBinsFilePath = Path.Combine(Application.persistentDataPath.Replace('/', System.IO.Path.DirectorySeparatorChar), "WasteBinsData.json");
    }
}
