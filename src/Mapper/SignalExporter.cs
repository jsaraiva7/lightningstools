using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Common.MacroProgramming;

namespace Mapper
{
    public class SignalExporter
    {
        public static void Export(List<Signal> SignalList )
        {
            var loc = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("\\Mapper.exe", "");

            var analog = ToCsv(";",SignalList.OfType<AnalogSignal>().ToList());
            var digital = ToCsv(";", SignalList.OfType<DigitalSignal>().ToList());
            var text = ToCsv(";", SignalList.OfType<TextSignal>().ToList());


            var save1 = loc + "\\analog.csv";
            var save2 = loc + "\\digital.csv";
            var save3 = loc + "\\text.csv";
            
            using (StreamWriter sw = File.AppendText(save1))
            {
                sw.Write(analog);
            }
            using (StreamWriter sw = File.AppendText(save2))
            {
                sw.Write(digital);
            }
            using (StreamWriter sw = File.AppendText(save3))
            {
                sw.Write(text);
            }

        }
        public static string ToCsv<T>(string separator, IEnumerable<T> objectlist)
        {
            Type t = typeof(T);
            var fields = t.GetProperties().Where(x=> !x.Name.Equals("Inverse") && !x.Name.Equals("PublisherObject") && !x.Name.Equals("Source")).ToArray();

            string header = String.Join(separator, fields.Select(f => f.Name).ToArray());

            StringBuilder csvdata = new StringBuilder();
            csvdata.AppendLine(header);

            foreach (var o in objectlist)
                csvdata.AppendLine(ToCsvFields(separator, fields, o));

            return csvdata.ToString();
        }

        public static string ToCsvFields(string separator, PropertyInfo[] fields, object o)
        {
            StringBuilder linie = new StringBuilder();

            foreach (var f in fields)
            {
                if (linie.Length > 0)
                    linie.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    linie.Append(x.ToString());
            }

            return linie.ToString();
        }
    }
}