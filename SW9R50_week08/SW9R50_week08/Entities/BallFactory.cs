using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW9R50_week08.Entities
{
    public class BallFactory
    {
        public Toy CreateNew()
        {
            return new Toy();
        }
    }
}
