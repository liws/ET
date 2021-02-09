using ETModel;
using System;
using System.Collections.Generic;

namespace ETHotfix
{
    [Config((int)(AppType.ClientH ))]
    public partial class BundleConfigCategory : ACategory<BundleConfig>
    {
    }

    public class BundleConfig : IConfig
    {
        public long Id { get; set; }
        public Dictionary<string, string> Relation;
    }
}