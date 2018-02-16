using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
    public interface IDefaultMetaDataRetriever
    {
		MetaCollection GetDefaultMetaData();
    }
}
