using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace Revitalize.Framework.Objects
{
    public class ObjectManager : IObjectManager
    {
        public virtual Item getItem(string Id, int StackSize = 1)
        {
            throw new NotImplementedException();
        }
    }
}
