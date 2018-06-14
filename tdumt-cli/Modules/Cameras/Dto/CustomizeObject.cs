using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace Djey.TduModdingTools.CLI
{
	[JsonObject("customizeCam")]	
	public class CustomizeObject
	{
		[JsonProperty("views")]
		private IList<View> _views;

		[JsonObject("camView")]
		public struct View {
			[JsonProperty("type")]
			[JsonConverter(typeof(StringEnumConverter))]
			TDUModdingLibrary.fileformats.binaries.Cameras.ViewType _view;

			[JsonIgnore]
			int _rootCameraId;

			[JsonProperty("cameraId")]
			int _cameraId;

			[JsonProperty("viewId")]
			int _viewId;

			/// <summary>
			/// Only for output
			/// </summary>
			/// <value><c>true</c> if this camera is customized; otherwise, <c>false</c>.</value>
			[JsonProperty("customized")]
			public Boolean IsCustomized {
				get {
					return _cameraId != 0
					&& _viewId != 0
					&& (_cameraId != _rootCameraId || _viewId != (int)_view);
				}
			}

			[JsonIgnore]
			public TDUModdingLibrary.fileformats.binaries.Cameras.ViewType ViewKind {
				get {
					return _view;
				}
				set {
					_view = value;
				}
			}

			[JsonIgnore]
			public int CameraId {
				get {
					return _cameraId;
				}
				set {
					_cameraId = value;
				}
			}

			[JsonIgnore]
			public int RootCameraId {
				get {
					return _rootCameraId;
				}
				set {
					_rootCameraId = value;
				}
			}

			[JsonIgnore]
			public int ViewId {
				get {
					return _viewId;
				}
				set {
					_viewId = value;
				}
			}
		}

		[JsonIgnore]
		public IList<View> Views {
			get {
				return _views;
			}
			set {
				_views = value;
			}
		}
	}
}

