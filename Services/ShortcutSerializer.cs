using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace SmartShortcuts.Model
{
    static class ShortcutSerializer
    {
        public static ObservableCollection<Group> Deserialize(string path)
        {
            if (!File.Exists(path))
                return null;
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<Group>>(json);
            }
        }

        public static void Serialize(ObservableCollection<Group> groups, string path)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + path;
            using (StreamWriter sw = new StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                sw.WriteLine(json);
            }
        }
    }
}
