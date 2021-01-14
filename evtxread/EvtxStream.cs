using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace evtxread
{
    public class EvtxStream
    {
        Stream File;

        public EvtxStream()
        {
            File = null;
        }

        public int Read(byte[] buf, int bufSize)
        {
            return File.Read(buf, 0, bufSize);
        }

        public long Seek(long offset, SeekOrigin whence)
        {
            return File.Seek(offset, whence);
        }

        public long GetSize()
        {
            return File.Length;
        }

        public bool Open(string filename)
        {
            Close();
            File = new FileStream(filename, FileMode.Open, FileAccess.Read);
            return !File.Equals(null);
        }

        public void Close()
        {
            if (File != null)
            {
                File.Close();
                File = null;
            }
        }
    }

    
}
