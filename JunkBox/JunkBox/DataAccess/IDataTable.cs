using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    interface IDataTable<TSelect, KSelect, TInsert, KInsert, TUpdate, KUpdate, TDelete, KDelete>
        where TSelect: IDataResultModel
        where TInsert: IDataResultModel
        where TUpdate: IDataResultModel
        where TDelete: IDataResultModel
        where KSelect: IDataParameterModel
        where KInsert: IDataParameterModel
        where KUpdate: IDataParameterModel
        where KDelete: IDataParameterModel
    {

        TSelect SelectRecord(KSelect parameters);

        TInsert InsertRecord(KInsert parameters);

        TUpdate UpdateRecord(KUpdate parameters);

        TDelete DeleteRecord(KDelete parameters);
            
    }
}
