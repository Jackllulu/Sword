using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwordCore;
using SwordData;

namespace SwordClient
{
    public static class AssetManager
    {
        public static Asset AccountAsset 
        { 
            get=>Singleton<Asset>.Instance; 
        }
    }

    public class Asset
    {
        public long ID { get; set; } = 0;
        public int Coins { get; set; } = 0;
        public int Dimends { get; set; } = 0;
        public int Level { get; set; } = 1;

    }
}
