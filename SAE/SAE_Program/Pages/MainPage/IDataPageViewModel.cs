using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE_DB;

namespace SAE_Program.Pages
{
    public interface IDataPageViewModel<T>
    {
        public bool IsEmpty { get; }
        public void UpdateData(T dataPresentor);
        public void SetEmptyData();
    }
}
