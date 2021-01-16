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
        const int EVTX_HEADER_FIRSTCHUNK_I = 8;
        const int EVTX_HEADER_LASTCHUNK_I = 16;
        const int EVTX_HEADER_SIZE_I = 32;
        const int EVTX_HEADER_CHUNKCOUNT_I = 42;
        
        EvtxStream Stream;

        long FirstChunkNum;
        long LastChunkNum;
        int HeaderSize;
        short ChunkCount;

        List<EvtxChunk> Chunks;

        public EvtxFile(string fname)
        {
            if (!File.Exists(fname))
                return;

    
            Chunks = new List<EvtxChunk>();

            Stream = new EvtxStream();
            Stream.Open(fname);

            if (Stream != null)
            {

                byte[] bHeader = new byte[4096];
                Stream.Read(bHeader, 4096);
                FirstChunkNum = BitConverter.ToInt64(bHeader, EVTX_HEADER_FIRSTCHUNK_I);
                LastChunkNum = BitConverter.ToInt64(bHeader, EVTX_HEADER_LASTCHUNK_I);
                HeaderSize = BitConverter.ToInt32(bHeader, EVTX_HEADER_SIZE_I);
                ChunkCount = BitConverter.ToInt16(bHeader, EVTX_HEADER_CHUNKCOUNT_I);

                byte[] bChunk = new byte[65536];
                int count = 0;
                while (Stream.Read(bChunk, 65536) == 65536)
                {
                    count++;
                    EvtxChunk newChunk = new EvtxChunk();
                    newChunk.Parse(bChunk);
                }

            }

        }
    }
}
