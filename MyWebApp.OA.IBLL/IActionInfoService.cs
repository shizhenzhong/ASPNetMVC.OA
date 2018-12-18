using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.OA.IBLL
{
    public partial interface IActionInfoService
    {
        bool DeleteEntities(List<int> list);
    }
}
