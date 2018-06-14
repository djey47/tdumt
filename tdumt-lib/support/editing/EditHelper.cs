using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Timers;
using DjeFramework1.Common.GUI.MVC;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.editing
{
    /// <summary>
    /// Static class providing methods to manage editing of tdu files
    /// </summary>
    // EVO_71: externalization
    public class EditHelper : IModelObserver
    {
        #region Constants
        /// <summary>
        /// Error code 1: invalid packed file
        /// </summary>
        public const string ERROR_CODE_INVALID_PACKED_FILE = "1";

        /// <summary>
        /// Error code 2: task already exists
        /// </summary>
        public const string ERROR_CODE_TASK_EXISTS = "2";

        /// <summary>
        /// Error code 3: extracting packed file failed
        /// </summary>
        public const string ERROR_CODE_EXTRACT_FAILED = "3";

        /// <summary>
        /// Error code 4: temporary folder could not be created
        /// </summary>
        public const string ERROR_CODE_TEMP_FOLDER = "4";
        #endregion

        #region Structures
        /// <summary>
        /// Represents edit of a particular packed file
        /// </summary>
        public struct Task
        {
            /// <summary>
            /// Date de début d'édition
            /// </summary>
            public DateTime startDate;

            /// <summary>
            /// Fichier BNK concerné
            /// </summary>
            public BNK parentBNK;

            /// <summary>
            /// Fichier empaqueté en cours d'édition
            /// </summary>
            public string editedPackedFilePath;

            /// <summary>
            /// Chemin complet du fichier extrait pour l'édition
            /// </summary>
            public string extractedFile;

            /// <summary>
            /// Full path of file being watched for changes (=extractedFile by default).
            /// Can be different for some cases, e.g. 2DB editing (dds used instead)
            /// </summary>
            public string trackedFile;

            /// <summary>
            /// Indicates if current task can be used or not
            /// </summary>
            public bool isValid;

            /// <summary>
            /// Indicates if tracked file has changed since last editing/applying
            /// </summary>
            public bool trackedFileHasChanged;

            /// <summary>
            /// Date&Time where extracted file was last written
            /// </summary>
            public DateTime trackedLastFileWriteTime;

            /// <summary>
            /// Indicates if current task should be reported or not in GUI
            /// </summary>
            public bool isFurtive;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Singleton accessor
        /// </summary>
        public static EditHelper Instance
        {
            get
            {
               if (_Instance == null)
                   _Instance = new EditHelper();

                return _Instance;
            }
        }
        private static EditHelper _Instance = null;

        /// <summary>
        /// Liste des tâches d'édition
        /// </summary>
        public Collection<Task> Tasks
        {
            get { return _Tasks; }
        }
        private readonly Collection<Task> _Tasks = new Collection<Task>();
        #endregion

        #region Members
        /// <summary>
        /// Timer watching task collection for files changes
        /// </summary>
        private readonly Timer taskWatcher;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditHelper()
        {
            // Timer initialization
            taskWatcher = new Timer();
            taskWatcher.Elapsed += _TaskWatcher_Tick;
            taskWatcher.Enabled = true;
            taskWatcher.Start();

        }

        #region Events
        private void _TaskWatcher_Tick(object sender, ElapsedEventArgs e)
        {
            // Occurs when watching delay has elapsed
            bool hasChanged = false;

            // Watching for all tasks
            for (int i = 0; i < Tasks.Count; i++ )
            {
                Task currentTask = Tasks[i];
                FileInfo fi = new FileInfo(currentTask.trackedFile);

                // ANO_48
                if (fi.Exists)
                {
                    DateTime modTime = fi.LastWriteTime;

                    if (currentTask.trackedLastFileWriteTime == DateTime.MinValue)
                    {
                        _SetExtractedFileLastWriteTime(i);
                        hasChanged = true;
                    } 
                    else if (modTime.CompareTo(currentTask.trackedLastFileWriteTime) > 0)
                    {
                        // File has been modified
                        _SetExtractedFileChanged(i, true);
                        _SetExtractedFileLastWriteTime(i);
                        hasChanged = true;
                    }
                }
            }

            if (hasChanged)
                // At least one has changed > signals to listeners
                NotifyAll();
        }
        #endregion

        #region IModelObserver Members
        /// <summary>
        /// Liste d'abonnés à notifier
        /// </summary>
        public Collection<IChangesListener> AllListeners
        {
            get { return allListeners; }
        }
        private readonly Collection<IChangesListener> allListeners = new Collection<IChangesListener>();

        /// <summary>
        /// Abonne un listener (VUE) aux données (MODEL)
        /// </summary>
        /// <param name="aListener"></param>
        public void AttachToObserver(IChangesListener aListener)
        {
            if (!allListeners.Contains(aListener))
            {
                allListeners.Add(aListener);

                // At first time, force listener update
                aListener.NotifyModelChanged();
            }
        }

        /// <summary>
        /// Désabonne un listener aux données
        /// </summary>
        /// <param name="aListener"></param>
        public void DetachFromObserver(IChangesListener aListener)
        {
            allListeners.Remove(aListener);
        }

        /// <summary>
        /// Notifie tous les abonnés (VUE) à cet objet qu'un changement est apparu dans le modèle
        /// </summary>
        public void NotifyAll()
        {
            foreach (IChangesListener listener in allListeners)
                listener.NotifyModelChanged();
        }

        /// <summary>
        /// Notifie tous les abonnés (VUE) à cet objet qu'un changement est apparu dans le modèle
        /// </summary>
        /// <param name="changeDescription">Description du changement effectué</param>
        /// <param name="parameters">liste de paramètres utiles à l'abonné</param>
        public void NotifyAll(string changeDescription, params object[] parameters)
        {
            foreach (IChangesListener listener in allListeners)
                listener.NotifyModelChanged(changeDescription, parameters);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Determines if a same task already exists (same BNK - same packed file)
        /// </summary>
        /// <param name="editTask">concerned edit task</param>
        /// <returns>true if same task exists, else false</returns>
        private bool _SameTaskExists(Task editTask)
        {
            return (_FindTask(editTask) != -1);
        }

        /// <summary>
        /// Returns index of specified task (a task with the same BNK and packed files)
        /// </summary>
        /// <param name="editTask">Task to search</param>
        /// <returns>Index of task, or -1 if it does not exist</returns>
        private int _FindTask(Task editTask)
        {
            // Task browsing
            for(int i = 0 ; i < _Tasks.Count ; i++)
            {
                Task anotherTask = _Tasks[i];

                if (anotherTask.parentBNK != null && editTask.parentBNK != null)
                {
                    if ( anotherTask.parentBNK.FileName.Equals(editTask.parentBNK.FileName, StringComparison.CurrentCultureIgnoreCase)
                        && anotherTask.editedPackedFilePath.Equals(editTask.editedPackedFilePath, StringComparison.CurrentCultureIgnoreCase))
                            return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Prepares a packed file for editing
        /// </summary>
        /// <param name="task">Current task</param>
        /// <returns>Extracted file name</returns>
        private string _PrepareFile(Task task)
        {
            return PrepareFile(task.parentBNK, task.editedPackedFilePath);
        }

        /// <summary>
        /// Set the changed flag to true for specified task
        /// </summary>
        /// <param name="taskIndex">Task index to which extracted file belongs</param>
        /// <param name="hasChanged">true to indicate it has changed, false else</param>
        private void _SetExtractedFileChanged(int taskIndex, bool hasChanged)
        {
            Task currentTask = Tasks[taskIndex];

            currentTask.trackedFileHasChanged = hasChanged;
            Tasks[taskIndex] = currentTask;
        }

        /// <summary>
        /// Updates extracted file last write time for sepecified task
        /// </summary>
        /// <param name="taskIndex">Task index to which extracted file belongs</param>
        private void _SetExtractedFileLastWriteTime(int taskIndex)
        {
            Task currentTask = Tasks[taskIndex];
            FileInfo fi = new FileInfo(currentTask.trackedFile);

            if (fi.Exists)
                currentTask.trackedLastFileWriteTime = fi.LastWriteTime;
            else
                currentTask.trackedLastFileWriteTime = DateTime.Now;

            Tasks[taskIndex] = currentTask;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates a new task and returns a copy instance
        /// </summary>
        /// <param name="parentBNK">Parent BNK file</param>
        /// <param name="packedFilePath">Path of packed file under edit</param>
        /// <param name="isFurtive">true to prevent this task to be displayed in GUI, else false</param>
        /// <returns>Added task instance</returns>
        public Task AddTask(BNK parentBNK, string packedFilePath, bool isFurtive)
        {
            Task editTask = new Task();

            if (parentBNK == null)
                return editTask;

            // New task
            editTask.parentBNK = parentBNK;
            editTask.editedPackedFilePath = packedFilePath;
            editTask.startDate = DateTime.Now;

            if (_SameTaskExists(editTask))
                throw new Exception(ERROR_CODE_TASK_EXISTS);

            // Preparing edit...
            editTask.isFurtive = isFurtive;
            editTask.extractedFile = _PrepareFile(editTask);

            if (editTask.extractedFile == null)
                throw new Exception(ERROR_CODE_EXTRACT_FAILED);

            // BUG_48: Preparing tracking...
            editTask.trackedFile = TduFile.GetTrackedFileName(editTask.extractedFile);

            // Original write time
            if (editTask.extractedFile.Equals(editTask.trackedFile))
            {
                FileInfo fi = new FileInfo(editTask.extractedFile);

                editTask.trackedLastFileWriteTime = fi.LastWriteTime;
            }
            else
                // If tracked file name is different from extracted one, this data is set after first tracking
                // Because at the moment tracked file may not be ready
                editTask.trackedLastFileWriteTime = DateTime.MinValue;

            editTask.isValid = true;

            // Change in task list
            _Tasks.Add(editTask);
            NotifyAll();

            return editTask;
        }

        /// <summary>
        /// Prepares a packed file for viewing/editing. can be used as stand-alone to get files without creating any task.
        /// </summary>
        /// <param name="bnk">BNK file</param>
        /// <param name="packedFilePath">Path of packed file to prepare</param>
        /// <returns>Full path of extracted file</returns>
        public string PrepareFile(BNK bnk, string packedFilePath)
        {
            string returnPath = null;

            if (bnk == null || string.IsNullOrEmpty(packedFilePath))
                return returnPath;

            // Working folder
            string workFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);

            if (workFolder == null)
                throw new Exception(ERROR_CODE_TEMP_FOLDER);

            // Extracting packed file into working folder
            string packedFileName = bnk.GetPackedFileName(packedFilePath);

            returnPath = string.Format(@"{0}\{1}",
                workFolder,
                packedFileName);
            bnk.ExtractPackedFile(packedFilePath, returnPath, false);

            // Removing attributes on file
            File.SetAttributes(returnPath, FileAttributes.Normal);

            return returnPath;
        }

        /// <summary>
        /// Removes all tasks. To be used in extreme cases only !
        /// In most cases, you've just to remove tasks current module has created, not the whole.
        /// </summary>
        public void ClearTasks()
        {
            Tasks.Clear();
            NotifyAll();
        }

        /// <summary>
        /// Deletes specified task
        /// </summary>
        /// <param name="task">Task to remove</param>
        public void RemoveTask(Task task)
        {
            // Searching a task about same extracted file
            Task taskToRemove = GetTask(task.extractedFile);

            if (Tasks.Contains(taskToRemove))
            {
                Tasks.Remove(taskToRemove);
                NotifyAll();
            }
        }

        /// <summary>
        /// Applies changes concerning specified task
        /// </summary>
        /// <param name="task">Task giving info on edited file</param>
        public void ApplyChanges(Task task)
        {
            // Replacing in BNK file
            task.parentBNK.ReplacePackedFile(task.editedPackedFilePath, task.extractedFile);

            // Updating task
            int taskIndex = _FindTask(task);

            if (taskIndex != -1)
            {
                _SetExtractedFileChanged(taskIndex, false);
                _SetExtractedFileLastWriteTime(taskIndex);
                NotifyAll();
            }
        }

        /// <summary>
        /// Returns task using specified extracted file
        /// </summary>
        /// <param name="extractedFileName">Name of extracted file for editing</param>
        /// <returns></returns>
        public Task GetTask(string extractedFileName)
        {
            Task returnTask = new Task();

            foreach (Task anotherTask in Tasks)
            {
                if (anotherTask.extractedFile != null 
                    && anotherTask.extractedFile.Equals(extractedFileName))
                {
                    returnTask = anotherTask;
                    break;
                }
            }

            return returnTask;
        }
        #endregion
    }
}