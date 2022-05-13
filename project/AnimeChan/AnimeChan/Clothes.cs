using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeChanNS
{

    enum Clothes : int
    {
        None = 0,
        Cap = 1 << 0,
        T_Shirt = 1 << 1,
        Blouse = 1 << 2,
        Jacket = 1 << 3,
        Underpants = 1 << 4,
        Trousers = 1 << 5,
        Socks = 1 << 6,
        Boots = 1 << 7,
        Lingerie = 1 << 8,

        SummerСlothes = Lingerie | Socks | Boots | T_Shirt | Trousers,
        HomeClothes = Lingerie | Socks,
    }
}
