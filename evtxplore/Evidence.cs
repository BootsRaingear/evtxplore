using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace evtxplore
{
    public class Evidence
    {
        Project Project;
        string EvDir;
        List<string> EvFiles;

        public Evidence(Project project, string evdir)
        {
            Project = project;
            EvDir = evdir.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

            EvFiles = Directory.GetFiles(EvDir, "*.evxt").ToList();
        }
    }
}

/*
string evtxFile;
evtxFile = openFileDialog.FileName;
// confirm file exists 
using (var reader = new EventLogReader(evtxFile, PathType.FilePath))
{
    EventRecord record;
    while ((record = reader.ReadEvent()) != null)
    {
        using (record)
        {
            // parse records
        }
    }
}
*/