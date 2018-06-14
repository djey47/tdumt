using System;

namespace TDUModdingLibrary.fileformats.database.util
{
    /// <summary>
    /// Represents an identifier for an item in resource database
    /// </summary>
    public class ResourceIdentifier : ICloneable
    {
        #region Properties
        /// <summary>
        /// Main value allowing to identify the entry
        /// </summary>
        public string Id
        {
            get { return _Id; }
            set { _Id = value.Clone() as string; }
        }
        private string _Id;

        /// <summary>
        /// Topic where this entry is situated
        /// </summary>
        public DB.Topic EnclosingTopic
        {
            get { return _EnclosingTopic; }
        }
        private readonly DB.Topic _EnclosingTopic = DB.Topic.None;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="topic"></param>
        public ResourceIdentifier(string identifier, DB.Topic topic)
        {
            _Id = identifier.Clone() as string;
            _EnclosingTopic = topic;
        }

        #region Public methods
        /// <summary>
        /// Determines if specified DatabaseIdentifier is equal to current DatabaseIdentifier. 
        /// </summary>
        /// <param name="obj">DatabaseIdentifier to compare with current one.</param>
        /// <returns>true if specified object is equal to current one; else false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ResourceIdentifier otherId = obj as ResourceIdentifier;

            if (otherId == null)
                return false;

            if (otherId._Id.Equals(_Id))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Hash function for this particular type
        /// </summary>
        /// <returns>Hash code of current object</returns>
        public override int GetHashCode()
        {
            if (_Id == null)
                return _EnclosingTopic.GetHashCode();
            else
                return _Id.GetHashCode();
        }
        #endregion

        #region ICloneable Membres
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            ResourceIdentifier clone = new ResourceIdentifier(_Id, _EnclosingTopic);

            return clone;
        }
        #endregion
    }
}