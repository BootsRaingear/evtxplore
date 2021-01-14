using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtxread
{
    public class EvtxChunk
    {
        const int EVTX_CHUNKH_SIGNATURE = 8;
        const int EVTX_CHUNKH_FIRSTRECORDNUMBER = 8;
        const int EVTX_CHUNKH_LASTRECORDNUMBER = 8;
        const int EVTX_CHUNKH_FIRSTRECORDID = 8;
        const int EVTX_CHUNKH_LASTRECORDID = 8;
        const int EVTX_CHUNKH_HEADERSIZE = 4;
        const int EVTX_CHUNKH_LASTEVENTDATAOFFSET = 4;
        const int EVTX_CHUNKH_FREESPACEOFFSET = 4;
        const int EVTX_CHUNKH_EVENTRECORDSCHECKSUM = 4;
        const int EVTX_CHUNKH_UNKNOWN = 64;
        const int EVTX_CHUNKH_FLAGS = 4;
        const int EVTX_CHUNKH_CHECKSUM = 4;
    }
}
