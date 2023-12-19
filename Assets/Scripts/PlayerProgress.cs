using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Player Progress Baru",
    menuName = "Game Kuis/Player Progress"
)]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int>progressLevel;
    }

    public string fileName = "save.txt";
    public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        // Sampel data
        progressData.koin = 200;
        if (progressData.progressLevel == null)
            progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        // Informasi penyimpanan data
        string directory = Application.dataPath + "/Temp";
        string path = directory + "/" + fileName;

        // Membuat directory temp
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Creted: " + directory);
        }

        // Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File Created: " + path);
        }

        // Menyimpan data ke dalam file menggunakan binary formatter
        var fileStream = File.Open(path, FileMode.Open);
        //var formatter = new BinaryFormatter();

        fileStream.Flush();

        // Menyimpan data ke dalam file menggunakan binary writer
        //formatter.Serialize(fileStream, progressData);
        var writer = new BinaryWriter(fileStream);
        
        writer.Write(progressData.koin);

        //string kontenData = $"{progressData.koin}\n";

        foreach (var i in progressData.progressLevel)
        {
            //kontenData += $"{i.Key} {i.Value}\n";
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        //File.WriteAllText(path, kontenData);
        
        // Putuskan aliran memori dengan File
        writer.Dispose();
        fileStream.Dispose();

        Debug.Log("Data Saved to File: " + path);
    }

    public bool MuatProgress()
    {
        // Informasi untuk memuat data
        string directory = Application.dataPath + "/Temp/";
        string path = directory + fileName;

        // Buka file dengan Filestream
        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);

            try
            {
                progressData.koin = reader.ReadInt32();
                if (progressData.progressLevel == null)
                    progressData.progressLevel = new();
                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progressData.progressLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }

                // Putuskan aliran memori dengan file
                reader.Dispose();
            }
            catch (System.Exception e)
            {
                Debug.Log($"Error: Terjadi kesalahan saat memuat progress\n{e.Message}");

                // Putuskan aliran memori dengan File
                reader.Dispose();
                fileStream.Dispose();

                return false;
            }

            // // Memuat data dari file menggunakan binary formatter
            // var formatter = new BinaryFormatter();

            // progressData = (MainData)formatter.Deserialize(fileStream);

            // Putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"{progressData.koin}; {progressData.progressLevel.Count}"); 

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error: Terjadi kesalahan saat memuat progress\n{e.Message}");

            // Putuskan aliran memori dengan File
            fileStream.Dispose();

            return false;
        }


    }
}
