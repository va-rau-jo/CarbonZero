using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Resource
    {
        private string name;
        private int upgradeLevel;

        public Resource(string name, int upgradeLevel)
        {
            this.name = name;
            this.upgradeLevel = upgradeLevel;
        }
    }
}
