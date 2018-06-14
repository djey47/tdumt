using System.Collections.Generic;
using Newtonsoft.Json;

namespace Djey.TduModdingTools.CLI.Modules.Banks.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject("bankInfoOutput")]
    class BankInfoOutputObject
    {
        [JsonProperty("fileSize")]
        private long _fileSize;

        [JsonProperty("year")]
        private int _year;

        [JsonProperty("packedFiles")]
        private readonly List<PackedFileInfoOutputObject> _packedFiles;

        public BankInfoOutputObject(int fileSize, int year)
        {
            _fileSize = fileSize;
            _year = year;
            _packedFiles = new List<PackedFileInfoOutputObject>();
        }

		[JsonIgnore]
        public List<PackedFileInfoOutputObject> PackedFiles
        {
            get { return _packedFiles; }
        }
    }
}