using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using static MCSHPHMI.Core.Globals;

namespace MCSHPHMI.Core
{
    public class FileLoader
    {
        /// <summary>
        /// Modifies the existing process variables with the settings from the
        /// specified file.
        /// </summary>
        public static void LoadProcessVariables(string filename = "ProcessVariables.csv")
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ProcVarRecord>();
                    foreach (ProcVarRecord record in records)
                    {
                        if(ProcVarDict.ContainsKey(record.ID))
                            record.ApplyTo(ProcVarDict[record.ID]);
                        else
                            Console.WriteLine($"Process Variable {record.ID} from .csv does not exist in Dictionary.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load process variable definitions:\n\n{ex}");
            }
        }

        /// <summary>
        /// Overwrites the process variable definitions
        /// </summary>
        public static void SaveProcessVariables(string filename = "ProcessVariables.csv")
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename + ".tmp"))
                using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    List<ProcVarRecord> records = new List<ProcVarRecord>();
                    foreach(var ProcVar in AllProcessVariables)
                    {
                        records.Add(ProcVarRecord.FromProcVar(ProcVar));
                    }
                    csv.WriteRecords(records);
                }
                File.Delete(filename);
                File.Move(filename + ".tmp", filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save process variable definitions:\n\n{ex}");
            }
        }
    }

    class ProcVarRecord
    {
        public string ID { get; set; }
        public string ShortDesc { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
        public double minScale { get; set; }
        public double maxScale { get; set; }
        public double minAlarm { get; set; }
        public double maxAlarm { get; set; }
        public double minIdeal { get; set; }
        public double maxIdeal { get; set; }
        public double minTrend { get; set; }
        public double maxTrend { get; set; }

        public void ApplyTo(ProcessVariable ProcVar)
        {
            if (ProcVar is null)
                return;
            ProcVar.ID = ID;
            ProcVar.ShortDesc = ShortDesc;
            ProcVar.Units = Units;
            ProcVar.Description = Description;
            ProcVar.minScale = minScale;
            ProcVar.maxScale = maxScale;
            ProcVar.minAlarm = minAlarm;
            ProcVar.maxAlarm = maxAlarm;
            ProcVar.minIdeal = minIdeal;
            ProcVar.maxIdeal = maxIdeal;
            ProcVar.minTrend = minTrend;
            ProcVar.maxTrend = maxTrend;            
        }

        public static ProcVarRecord FromProcVar(ProcessVariable ProcVar)
        {
            return new ProcVarRecord()
            {
                ID = ProcVar.ID,
                ShortDesc = ProcVar.ShortDesc,
                Units = ProcVar.Units,
                Description = ProcVar.Description,
                minScale = ProcVar.minScale,
                maxScale = ProcVar.maxScale,
                minAlarm = ProcVar.minAlarm,
                maxAlarm = ProcVar.maxAlarm,
                minIdeal = ProcVar.minIdeal,
                maxIdeal = ProcVar.maxIdeal,
                minTrend = ProcVar.minTrend,
                maxTrend = ProcVar.maxTrend,
            };
        }
    }
}
