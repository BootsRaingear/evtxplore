using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtxread
{
    class EvtxHeader
    {
        const int EVTX_HEADER_SIGNATURE = 8;
        const int EVTX_HEADER_FIRSTCHUNK = 8;
        const int EVTX_HEADER_LASTCHUNK = 8;
        const int EVTX_HEADER_NEXTRECORDID = 4;
        const int EVTX_HEADER_SIZE = 4;
        const int EVTX_HEADER_MINORVER = 2;
        const int EVTX_HEADER_MAJORVER = 2;
        const int EVTX_HEADER_HBLOCKSIZE = 2;
        const int EVTX_HEADER_CHUNKCOUNT = 2;
        const int EVTX_HEADER_UNUSED = 76;
        const int EVTX_HEADER_FILEFLAG = 4;
        const int EVTX_HEADER_CHECKSUM = 4;
        const int EVTX_HEADER_UNUSED2 = 3968;

        byte[] Signature;
        byte[] FirstChunkNum;
        byte[] LastChunkNum;
        byte[] NextRecordIdentifier;
        byte[] HeaderSize;
        byte[] MinorV;
        byte[] MajorV;
        byte[] HeaderBlockSize;
        byte[] ChunkCount;
        byte[] Unused;
        byte[] FileFlag;
        byte[] Checksum;
        byte[] Unused2;

        public EvtxHeader()
        {
            Signature = new byte[EVTX_HEADER_SIGNATURE];
            FirstChunkNum = new byte[EVTX_HEADER_FIRSTCHUNK];
            LastChunkNum = new byte[EVTX_HEADER_LASTCHUNK];
            NextRecordIdentifier = new byte[EVTX_HEADER_NEXTRECORDID];
            HeaderSize = new byte[EVTX_HEADER_SIZE];
            MinorV = new byte[EVTX_HEADER_MINORVER];
            MajorV = new byte[EVTX_HEADER_MAJORVER];
            HeaderBlockSize = new byte[EVTX_HEADER_HBLOCKSIZE];
            ChunkCount = new byte[EVTX_HEADER_CHUNKCOUNT];
            Unused = new byte[EVTX_HEADER_UNUSED];
            FileFlag = new byte[EVTX_HEADER_FILEFLAG];
            Checksum = new byte[EVTX_HEADER_CHECKSUM];
        }        

        public bool Read(EvtxStream stream)
        {
            if (stream == null)
            {
                return false;
            }
            int read = 0;

            read += stream.Read(Signature, EVTX_HEADER_SIGNATURE);
            read += stream.Read(FirstChunkNum, EVTX_HEADER_FIRSTCHUNK);
            read += stream.Read(LastChunkNum, EVTX_HEADER_LASTCHUNK);
            read += stream.Read(NextRecordIdentifier, EVTX_HEADER_NEXTRECORDID);
            read += stream.Read(HeaderSize, EVTX_HEADER_SIZE);
            read += stream.Read(MinorV, EVTX_HEADER_MINORVER);
            read += stream.Read(MajorV, EVTX_HEADER_MAJORVER);
            read += stream.Read(HeaderBlockSize, EVTX_HEADER_HBLOCKSIZE);
            read += stream.Read(ChunkCount, EVTX_HEADER_CHUNKCOUNT);
            read += stream.Read(Unused, EVTX_HEADER_UNUSED);
            read += stream.Read(FileFlag, EVTX_HEADER_FILEFLAG);
            read += stream.Read(Checksum, EVTX_HEADER_CHECKSUM);
            read += stream.Read(Unused2, EVTX_HEADER_UNUSED2);

            return (read == 4096);
        }
    }
}
