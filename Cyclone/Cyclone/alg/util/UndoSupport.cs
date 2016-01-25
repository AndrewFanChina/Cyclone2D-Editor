using System;
using System.Collections;
using System.Diagnostics;

namespace Cyclone.alg.util
{
    /// <summary>
    /// IUndoable is an interface for undo/redo support that is
    /// implemented by the UndoCommand class.
    /// </summary>
    public interface IUndoable
    {
        void Undo();
        void Redo();
    }

    /// <summary>
    /// UndoCommand is an abstract class that represents an undoable
    /// or redoable operation or command. It provides virtual Undo()
    /// and Redo() methods which your derived command classes can
    /// override in order to implement actual undo/redo functionality.
    /// In a derived command class, you also have the option of
    /// not overriding the Undo() and Redo() methods. Instead, you
    /// can treat the derived command class like a data class and
    /// simply provide extra fields, properties, or methods that an
    /// external class (one that implements IUndoHandler) can use to
    /// perform the actual undo/redo functionality.
    /// </summary>
    public abstract class UndoCommand : IUndoable
    {
        /// <summary>
        /// GetText() should return a short description of the
        /// user operation associated with this command. For example,
        /// a graphics line drawing operation might have the
        /// text, "Draw Line". This method can be used to update
        /// the Text property of an undo menu item. For example,
        /// instead of just displaying "&Undo", the Text property
        /// of an undo menu item can be augmented to "&Undo Draw Line".
        /// </summary>
        /// <returns>Short description of the command.</returns>
        public virtual string GetText()
        {
            return "";
        }

        /// <summary>
        /// Perform undo of this command.
        /// </summary>
        public virtual void Undo()
        {
            // Empty implementation.
        }

        /// <summary>
        /// Perform redo of this command.
        /// </summary>
        public virtual void Redo()
        {
            // Empty implementation.
        }
    }

    /// <summary>
    /// Optional interface that your application classes can implement
    /// in order to perform the actual undo/redo functionality.
    /// It's optional because you can choose to implement undo/redo
    /// functionality solely within the derived command classes.
    /// </summary>
    public interface IUndoHandler
    {
        void Undo(UndoCommand cmd);
        void Redo(UndoCommand cmd);
        void refreshHistory(ArrayList history);
    }
    /// <summary>
    /// UndoInfo is a nested, private class that is used as the
    /// data type in the undo list and redo stack. It just stores
    /// a reference to a command, and optionally, an undo handler.
    /// </summary>
    public class UndoInfo
    {
        public UndoCommand m_undoCommand;
        public IUndoHandler m_undoHandler;

        public UndoInfo()
        {
            m_undoCommand = null;
            m_undoHandler = null;
        }
    }
    /// <summary>
    /// UndoManager is a concrete class that maintains the undo list
    /// and redo stack data structures. It also provides methods that
    /// tell you whether there is something to undo or redo. The class
    /// is designed to be used directly in undo/redo menu item handlers,
    /// and undo/redo menu item state update functions.
    /// </summary>
    public class UndoManager
    {


        private int m_maxUndoLevel;
        private ArrayList m_undoList;
        private Stack m_redoStack;

        /// <summary>
        /// Constructor which initializes the manager with up to 8 levels
        /// of undo/redo.
        /// </summary>
        public UndoManager()
        {
            m_maxUndoLevel = 8;
            m_undoList = new ArrayList();
            m_redoStack = new Stack();
        }
        public UndoManager(int m_maxUndoLevelT)
        {
            m_maxUndoLevel = m_maxUndoLevelT;
            m_undoList = new ArrayList();
            m_redoStack = new Stack();
        }
        /// <summary>
        /// Property for the maximum undo level.
        /// </summary>
        public int MaxUndoLevel
        {
            get
            {
                return m_maxUndoLevel;
            }
            set
            {
                Debug.Assert(value >= 0);

                // To keep things simple, if you change the undo level,
                // we clear all outstanding undo/redo commands.
                if (value != m_maxUndoLevel)
                {
                    ClearUndoRedo();
                    m_maxUndoLevel = value;
                }
            }
        }

        /// <summary>
        /// Register a new undo command. Use this method after your
        /// application has performed an operation/command that is
        /// undoable.
        /// </summary>
        /// <param name="cmd">New command to add to the manager.</param>
        public void AddUndoCommand(UndoCommand cmd)
        {
            Debug.Assert(cmd != null);
            Debug.Assert(m_undoList.Count <= m_maxUndoLevel);

            if (m_maxUndoLevel == 0)
                return;

            UndoInfo info = null;
            if (m_undoList.Count == m_maxUndoLevel)
            {
                // Remove the oldest entry from the undo list to make room.
                info = (UndoInfo)m_undoList[0];
                m_undoList.RemoveAt(0);
            }

            // Insert the new undoable command into the undo list.
            if (info == null)
                info = new UndoInfo();
            info.m_undoCommand = cmd;
            info.m_undoHandler = null;
            m_undoList.Add(info);

            // Clear the redo stack.
            ClearRedo();
        }

        /// <summary>
        /// Register a new undo command along with an undo handler. The
        /// undo handler is used to perform the actual undo or redo
        /// operation later when requested.
        /// </summary>
        /// <param name="cmd">New command to add to the manager.</param>
        /// <param name="undoHandler">Undo handler to perform the actual undo/redo operation.</param>
        public void AddUndoCommand(UndoCommand cmd, IUndoHandler undoHandler)
        {
            AddUndoCommand(cmd);

            if (m_undoList.Count > 0)
            {
                UndoInfo info = (UndoInfo)m_undoList[m_undoList.Count - 1];
                Debug.Assert(info != null);
                info.m_undoHandler = undoHandler;
                undoHandler.refreshHistory(m_undoList);
            }
        }

        /// <summary>
        /// Clear the internal undo/redo data structures. Use this method
        /// when your application performs an operation that cannot be undone.
        /// For example, when the user "saves" or "commits" all the changes in
        /// the application.
        /// </summary>
        public void ClearUndoRedo()
        {
            ClearUndo();
            ClearRedo();
        }

        /// <summary>
        /// Check if there is something to undo. Use this method to decide
        /// whether your application's "Undo" menu item should be enabled
        /// or disabled.
        /// </summary>
        /// <returns>Returns true if there is something to undo, false otherwise.</returns>
        public bool CanUndo()
        {
            return m_undoList.Count > 0;
        }

        /// <summary>
        /// Check if there is something to redo. Use this method to decide
        /// whether your application's "Redo" menu item should be enabled
        /// or disabled.
        /// </summary>
        /// <returns>Returns true if there is something to redo, false otherwise.</returns>
        public bool CanRedo()
        {
            return m_redoStack.Count > 0;
        }

        /// <summary>
        /// Perform the undo operation. If an undo handler is specified, it
        /// will be used to perform the actual operation. Otherwise, the command
        /// instance is asked to perform the undo.
        /// </summary>
        public void Undo()
        {
            if (!CanUndo())
                return;

            // Remove newest entry from the undo list.
            UndoInfo info = (UndoInfo)m_undoList[m_undoList.Count - 1];
            m_undoList.RemoveAt(m_undoList.Count - 1);

            // Perform the undo.
            Debug.Assert(info.m_undoCommand != null);
            if (info.m_undoHandler != null)
            {
                info.m_undoHandler.Undo(info.m_undoCommand);
                info.m_undoHandler.refreshHistory(m_undoList);
            }
            else
            {
                info.m_undoCommand.Undo();
            }

            // Now the command is available for redo. Push it onto
            // the redo stack.
            m_redoStack.Push(info);
        }

        /// <summary>
        /// Perform the redo operation. If an undo handler is specified, it
        /// will be used to perform the actual operation. Otherwise, the command
        /// instance is asked to perform the redo.
        /// </summary>
        public void Redo()
        {
            if (!CanRedo())
                return;

            // Remove newest entry from the redo stack.
            UndoInfo info = (UndoInfo)m_redoStack.Pop();

            // Perform the redo.
            Debug.Assert(info.m_undoCommand != null);
            if (info.m_undoHandler != null)
            {
                info.m_undoHandler.Redo(info.m_undoCommand);
                info.m_undoHandler.refreshHistory(m_undoList);
            }
            else
            {
                info.m_undoCommand.Redo();
            }
            // Now the command is available for undo again. Put it back
            // into the undo list.
            m_undoList.Add(info);
        }

        /// <summary>
        /// Get the text value of the next undo command. Use this method
        /// to update the Text property of your "Undo" menu item if
        /// desired. For example, the text value for a command might be
        /// "Draw Circle". This allows you to change your menu item Text
        /// property to "&Undo Draw Circle".
        /// </summary>
        /// <returns>Text value of the next undo command.</returns>
        public string GetUndoText()
        {
            UndoCommand cmd = GetNextUndoCommand();
            if (cmd == null)
                return "";
            return cmd.GetText();
        }

        /// <summary>
        /// Get the text value of the next redo command. Use this method
        /// to update the Text property of your "Redo" menu item if desired.
        /// For example, the text value for a command might be "Draw Line".
        /// This allows you to change your menu item text to "&Redo Draw Line".
        /// </summary>
        /// <returns>Text value of the next redo command.</returns>
        public string GetRedoText()
        {
            UndoCommand cmd = GetNextRedoCommand();
            if (cmd == null)
                return "";
            return cmd.GetText();
        }

        /// <summary>
        /// Get the next (or newest) undo command. This is like a "Peek"
        /// method. It does not remove the command from the undo list.
        /// </summary>
        /// <returns>The next undo command.</returns>
        public UndoCommand GetNextUndoCommand()
        {
            if (m_undoList.Count == 0)
                return null;
            UndoInfo info = (UndoInfo)m_undoList[m_undoList.Count - 1];
            return info.m_undoCommand;
        }

        /// <summary>
        /// Get the next redo command. This is like a "Peek"
        /// method. It does not remove the command from the redo stack.
        /// </summary>
        /// <returns>The next redo command.</returns>
        public UndoCommand GetNextRedoCommand()
        {
            if (m_redoStack.Count == 0)
                return null;
            UndoInfo info = (UndoInfo)m_redoStack.Peek();
            return info.m_undoCommand;
        }

        /// <summary>
        /// Retrieve all of the undo commands. Useful for debugging,
        /// to analyze the contents of the undo list.
        /// </summary>
        /// <returns>Array of commands for undo.</returns>
        public UndoCommand[] GetUndoCommands()
        {
            if (m_undoList.Count == 0)
                return null;

            UndoCommand[] cmdList = new UndoCommand[m_undoList.Count];
            object[] objList = m_undoList.ToArray();
            for (int i = 0; i < objList.Length; i++)
            {
                UndoInfo info = (UndoInfo)objList[i];
                cmdList[i] = info.m_undoCommand;
            }

            return cmdList;
        }

        /// <summary>
        /// Retrieve all of the redo commands. Useful for debugging,
        /// to analyze the contents of the redo stack.
        /// </summary>
        /// <returns>Array of commands for redo.</returns>
        public UndoCommand[] GetRedoCommands()
        {
            if (m_redoStack.Count == 0)
                return null;

            UndoCommand[] cmdList = new UndoCommand[m_redoStack.Count];
            object[] objList = m_redoStack.ToArray();
            for (int i = 0; i < objList.Length; i++)
            {
                UndoInfo info = (UndoInfo)objList[i];
                cmdList[i] = info.m_undoCommand;
            }

            return cmdList;
        }

        /// <summary>
        /// Clear the contents of the undo list.
        /// </summary>
        private void ClearUndo()
        {
            while (m_undoList.Count > 0)
            {
                UndoInfo info = (UndoInfo)m_undoList[m_undoList.Count - 1];
                m_undoList.RemoveAt(m_undoList.Count - 1);
                info.m_undoCommand = null;
                info.m_undoHandler = null;
            }
        }

        /// <summary>
        /// Clear the contents of the redo stack.
        /// </summary>
        private void ClearRedo()
        {
            while (m_redoStack.Count > 0)
            {
                UndoInfo info = (UndoInfo)m_redoStack.Pop();
                info.m_undoCommand = null;
                info.m_undoHandler = null;
            }
        }
    }
}
