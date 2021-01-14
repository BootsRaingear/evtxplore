using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace evtxread
{
    public class EvtxFile
    {
        EvtxStream Stream;
        EvtxHeader Header;

        public EvtxFile(string fname)
        {
            if (!File.Exists(fname))
                return;

            Stream = new EvtxStream();
            Stream.Open(fname);

            if (Stream != null)
            {
                
            }

        }

        public void ReadHeader()
        {
            Header = new EvtxHeader();

        }
    }
}
