using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Diagnostics.Eventing.Reader;
using System.Windows;

namespace evtxplore
{
    [Serializable]
    public class Project
    {
        string ProjNum;
        string Details;
        string ProjDir;
        string DBFile;
        Dictionary<string, Evidence> Evidence;

        public Project(string projNum, string projDir, string details)
        {
            ProjNum = projNum;
            ProjDir = projDir.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            Details = details;

            Evidence = new Dictionary<string, Evidence>();

            // create sqlite db
            DBFile = "evtxpc.db";
            
            using (var connection = new SqliteConnection("Data Source=" + ProjDir + DBFile))
            {
                try
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            CREATE TABLE Evidence (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                name TEXT NOT NULL,
                                filepath TEXT NOT NULL                                
                            )
                        ";
                    command.ExecuteNonQuery();


                }
                catch (SqliteException)
                {
                    // log exception
                }
            }
            
        }

        public bool AddEvidence(String evPath)
        {
            String[] evtxFiles = System.IO.Directory.GetFiles(evPath, "*.evtx");

            foreach (string evtxFile in evtxFiles)
            {
                string evtxName = Path.GetFileNameWithoutExtension(evtxFile).Replace(" ", "").Replace("%4", "").Replace("-","");                

                using (var connection = new SqliteConnection("Data Source=" + ProjDir + DBFile))
                {
                    try
                    {
                        var command = connection.CreateCommand();
                        command.CommandText =
                            @"
                                CREATE TABLE " + evtxName + @" (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    type TEXT NOT NULL,
                                    date DATETIME NOT NULL,
                                    eventid INTEGER NOT NULL,
                                    source TEXT NOT NULL,
                                    category TEXT NOT NULL,
                                    user TEXT NOT NULL,
                                    computer TEXT NOT NULL,
                                    description TEXT);
                                ";

                        connection.Open();
                        command.ExecuteNonQuery();
                        command.CommandText =
                            @"
                                INSERT INTO Evidence (
                                    name, 
                                    filepath) 
                                VALUES ('" +
                                    evtxName + "', '" +
                                    evtxFile +
                                "');";

                        command.ExecuteNonQuery();

                        using (var reader = new EventLogReader(evtxFile, PathType.FilePath))
                        {
                            EventRecord record;                            
                            while ((record = reader.ReadEvent()) != null)
                            {
                                using (record)
                                {                                    
                                    MessageBox.Show(record.ToXml());                                    
                                }
                            }
                        }
                        connection.Close();
                    }
                    catch (SqliteException)
                    {

                    }
                }

            }
            return true;
        }

    }
}
