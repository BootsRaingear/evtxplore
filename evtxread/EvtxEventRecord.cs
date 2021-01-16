using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtxread
{
    class EvtxEventRecord
    {
        enum TokenType : byte
        {
            BinXmlTokenEOF = 0x00,
            BinXmlTokenOpenStartElementTag = 0x01,
            BinXmlTokenCloseStartElementTag = 0x02,
            BinXmlTokenCloseEmptyElementTag = 0x03,
            BinXmlTokenEndElementTag = 0x04,
            BinXmlTokenValue = 0x05,
            BinXmlTokenAttribute = 0x06,
            BinXmlTokenCDATASection = 0x07,
            BinXmlTokenCharRef = 0x08,
            BinXmlTokenEntityRef = 0x09,
            BinXmlTokenPITarget = 0x0a,
            BinXmlTokenPIData = 0x0b,
            BinXmlTokenTemplateInstance = 0x0c,
            BinXmlTokenNormalSubstitution = 0x0d,
            BinXmlTokenOptionalSubstitution = 0x0e,
            BinXmlFragmentHeaderToken = 0x0f
        }

        enum ValueType : byte
        {
            NullType = 0x00,        //NULL or empty
            StringType = 0x01,      //Stored as UTF-16 little-endian without an end-of-string character
            AnsiStringType = 0x02,  //ASCII string  | Stored using a codepage without an end-of-string character
            Int8Type = 0x03,
            UInt8Type = 0x04,
            Int16Type = 0x05,
            UInt16Type = 0x06,
            Int32Type = 0x07,
            UInt32Type = 0x08,
            Int64Type = 0x09,
            UInt64Type = 0x0a,
            Real32Type = 0x0b,
            Real64Type = 0x0c,
            BoolType = 0x0d,
            BinaryType = 0x0e,      //Boolean - An 32-bit integer that MUST be 0x00 or 0x01 (mapping to true or false, respectively).
            GuidType = 0x0f,        //GUID - Stored in little-endian
            SizeTType = 0x10,       //Size Type - Either 32 or 64-bits. This value type should be pair up with a HexInt32Type or HexInt64Type
            FileTimeType = 0x11,    //FILETIME - Stored in little-endian
            SysTimeType = 0x12,     //System time (128-bit) - Stored in little-endian
            SidType = 0x13,
            HexInt32Type = 0x14,
            HexInt64Type = 0x15,
            EvtHandle = 0x20,       //Unknown
            BinXmlType = 0x21,      //Binary XML fragment
            EvtXml = 0x23,          //Unknown

        }

        const int ER_SIZE_I = 4;
        const int ER_ID_I = 8;
        const int ER_TIME_I = 16;
        const int ER_BINXMLSTART = 28;

        long Id;
        /// <summary>
        /// Time of event in FILETIME format
        /// </summary>
        long Time;
        int Size;

        List<EvtxFragment> Fragments;

        public EvtxEventRecord()
        {
            Fragments = new List<EvtxFragment>();
            // binary xml event record
        }

        public bool Parse(byte[] eventRecord, int recordStart)
        {
            Size = BitConverter.ToInt32(eventRecord, ER_SIZE_I);
            Id = BitConverter.ToInt64(eventRecord, ER_ID_I);
            Time = BitConverter.ToInt64(eventRecord, ER_TIME_I);

           

            if (eventRecord[ER_BINXMLSTART] != Convert.ToByte(TokenType.BinXmlTokenOpenStartElementTag))
                return true;

            return true;
        }

    }
}
