using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SOG.SaveManager {
  public static class SaveSytem {
    public static void SaveData(Data data, string path) {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Create);
      formatter.Serialize(stream, data);
      stream.Close();
    }
    public static Data LoadData(string path) {
      if (File.Exists(path)) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        Data data = formatter.Deserialize(stream) as Data; stream.Close();
        return data;
      } else return null;
    }
  }
}
