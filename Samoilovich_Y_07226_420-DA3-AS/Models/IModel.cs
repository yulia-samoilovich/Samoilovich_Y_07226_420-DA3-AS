using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samoilovich_Y_07226_420_DA3_AS.Models
{
    public interface IModel<TModel> where TModel : IModel<TModel>, new()
    {
        TModel Insert();
        TModel GetById();
        TModel Update();
        TModel Delete();
    }
}
