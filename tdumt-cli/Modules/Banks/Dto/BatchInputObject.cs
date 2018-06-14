using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Djey.TduModdingTools.CLI
{
	[JsonObject("batchInput")]
	public class BatchInputObject
	{
		[JsonProperty("items")]
		private IList<BatchItem> _batchItems;

		[JsonObject("batchItem")]
		public struct BatchItem {
			[JsonProperty("iPath")]
			string _internalPath;

			[JsonProperty("eFile")]
			string _externalFile;

			public string ExternalFile {
				get {
					return _externalFile;
				}
			}

			public string InternalPath {
				get {
					return _internalPath;
				}
			}
		}

		public IList<BatchItem> Items {
			get {
				return _batchItems;
			}
		}
	}
}
