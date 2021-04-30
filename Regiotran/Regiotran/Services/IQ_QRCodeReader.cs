using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Regiotran.Services
{
    interface IQ_QRCodeReader
    {
        Task<string> ScanAsync();
    }
}

