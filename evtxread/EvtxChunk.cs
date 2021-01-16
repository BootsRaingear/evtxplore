using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtxread
{
    /// <summary>
    /// .evtx chunk, contains event records 64KB
    /// </summary>
    public class EvtxChunk
    {        
        const int CHUNKH_SIGNATURE = 8;
        const int CHUNKH_FIRSTRECORDNUMBER = 8;
        const int CHUNKH_LASTRECORDNUMBER = 8;
        const int CHUNKH_FIRSTRECORDID = 8;
        const int CHUNKH_LASTRECORDID = 8;
        const int CHUNKH_HEADERSIZE = 4;
        const int CHUNKH_LASTEVENTDATAOFFSET = 4;
        const int CHUNKH_FREESPACEOFFSET = 4;
        const int CHUNKH_EVENTRECORDSCHECKSUM = 4;
        const int CHUNKH_UNKNOWN = 64;
        const int CHUNKH_FLAGS = 4;
        const int CHUNKH_CHECKSUM = 4;

        const int CHUNK_SIZE = 512;

        const int CHUNKH_FIRSTRECORDNUMBER_I = 8;
        const int CHUNKH_LASTRECORDNUMBER_I = 16;

        const int ER_SIZE_I = 4;

        public long FirstRecordNumber;
        public long LastRecordNumber;

        List<EvtxEventRecord> EventRecords;
        public EvtxChunk()
        {
            EventRecords = new List<EvtxEventRecord>();

        }

        public bool Parse(byte[] chunk)
        {
            if (chunk.Length != 65536)
                return false;

            FirstRecordNumber = BitConverter.ToInt64(chunk, CHUNKH_FIRSTRECORDNUMBER_I);
            LastRecordNumber = BitConverter.ToInt64(chunk, CHUNKH_LASTRECORDNUMBER_I);


            int currentRecordStart = CHUNK_SIZE;
            long currentRecordNumber = FirstRecordNumber;

            while (currentRecordNumber < LastRecordNumber)
            {

                int recordSize = BitConverter.ToInt32(chunk, currentRecordStart + ER_SIZE_I);
                byte[] bEventRecord = new byte[recordSize];
                Array.Copy(chunk, currentRecordStart, bEventRecord, 0, recordSize);

                EvtxEventRecord eventRecord = new EvtxEventRecord();
                eventRecord.Parse(bEventRecord, currentRecordStart);

                currentRecordStart += recordSize;
                currentRecordNumber++;
            }


            return true;
        }

    }
}
