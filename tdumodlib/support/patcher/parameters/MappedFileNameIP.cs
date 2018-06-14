using System;
using System.Collections.Generic;
using System.Text;
using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    class MappedFileNameIP : FileNameIP 
    {
        public override string Name
        {
            get { return ParameterName.mappedFileName.ToString(); }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get
            {
                 // All shared (unused files in TDU folders)
                return new MappableFilesVP();
            }
        }
    }
}