using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Cyclone.mod;
using System.Collections;

namespace Cyclone.alg.type
{
    public interface SerializeAble
    {
        void ReadObject(Stream s);
        void WriteObject(Stream s);
        void ExportObject(Stream fs_bin);
    }
}
