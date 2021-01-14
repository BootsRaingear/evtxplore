using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtxplore
{
    public class EvtxTab
    {
        public string Header { get; set; }
        public ObservableCollection<EvtxTabData> Data { get; } = new ObservableCollection<EvtxTabData>();

    }

    public class EvtxTabData
    {
        
    }
}
