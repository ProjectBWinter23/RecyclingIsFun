using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Database
{
    /// <summary>
    /// Waste Bins Data Class, Local storage support
    /// </summary>
    [Serializable]
    internal class WasteBinsData
    {
        public KeyValuePair<string, string> YellowBin;
        public KeyValuePair<string, string> BlueBin;
        public KeyValuePair<string, string> BrownBin;
        public KeyValuePair<string, string> GreenBin;
        public KeyValuePair<string, string> GrayBin;
        public WasteBinsData() { }
        public static WasteBinsData Get()
        {
            string wasterBins = File.ReadAllText(DatabaseManager.WasteBinsFilePath);
            return JsonConvert.DeserializeObject<WasteBinsData>(wasterBins);
        }

        public static void WriteFile()
        {
            WasteBinsData wasteBins = new WasteBinsData()
            {
                YellowBin = new KeyValuePair<string, string>("Yellow Bin (Gelber Sack)", "The yellow bin contents are packaging materials such as plastic, metal, and Tetra Paks. Items like plastic bottles, aluminum cans, yogurt containers, and Tetra Pak cartons go into the yellow bin"),
                BlueBin = new KeyValuePair<string, string>("Blue Bin (Papier or Altpapier)", "The blue bin contents are paper and cardboard waste. This includes newspapers, magazines, cardboard boxes, office paper, and other paper-based materials."),
                BrownBin = new KeyValuePair<string, string>("Brown Bin (Biomüll or Bioabfall)", "The brown bin contents are organic or biodegradable waste. This includes kitchen scraps (fruit and vegetable peels, etc.) and garden waste."),
                GreenBin = new KeyValuePair<string, string>("Green Bin or Glass Containers (Glascontainer)", "The green bin contants glass waste, often separated by color - green, brown, and white. This includes glass bottles and jars."),
                GrayBin = new KeyValuePair<string, string>("Gray Bin", "The gray bin contents items that cannot be recycled or composted go into the residual waste bin. This may include certain plastics, non-recyclable packaging, and other non-compostable/non-recyclable items.")
            };

            string userData = JsonConvert.SerializeObject(wasteBins);

            File.WriteAllText(DatabaseManager.WasteBinsFilePath, userData);

            Debug.Log("JSON data written to: " + DatabaseManager.WasteBinsFilePath);
        }
    }
}
