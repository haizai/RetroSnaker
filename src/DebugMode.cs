// debug
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RetroSnaker {
    class DebugMode {
        public static void Debug(IDebug d) {
            string s = JsonSerializer.Serialize(d.Debug(),new JsonSerializerOptions{
                WriteIndented = true
            });
            WriteLine(s);
        }
        public static void WriteLine(string s) {
            string path = "debug.txt";
            using (StreamWriter sw = File.AppendText(path)) {
                sw.WriteLine(s);
            }
        }
        public static void DebugArg(IDebug[] ds) {
            var list = new List<Object>();
            foreach(var d in ds) {
                list.Add(d.Debug());
            }
            string s = JsonSerializer.Serialize(list,new JsonSerializerOptions{
                WriteIndented = true
            });
            WriteLine(s);
        }
    }
    interface IDebug {
        Object Debug();
    }
}
