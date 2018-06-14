using Newtonsoft.Json;

namespace Djey.TduModdingTools.CLI.Modules.Banks.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject("packedFileInfoOutput")]
    class PackedFileInfoOutputObject
    {
		[JsonProperty("shortName")]
		private string _shortName;

		[JsonProperty("name")]
        private string _name;

        [JsonProperty("fileSize")]
		private long _fileSize;

		[JsonProperty("type")]
		private string _typeDescription;

		public PackedFileInfoOutputObject (string shortName, string name, long fileSize, string typeDescription)
		{
			_shortName = shortName;
			_name = name;
			_fileSize = fileSize;
			_typeDescription = typeDescription;
		}
    }
}