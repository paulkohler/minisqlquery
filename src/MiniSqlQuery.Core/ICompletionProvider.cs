using System;

namespace MiniSqlQuery.Core
{
    public interface ICompletionProvider
    {
        bool Enabled { get; set; }
        bool KeyEventHandlerFired(char ch);
    }

    public class NullCompletionProvider : ICompletionProvider
    {
        private readonly bool _enabled;

        public NullCompletionProvider()
        {
            _enabled = false;
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { }
        }

        public bool KeyEventHandlerFired(char ch)
        {
            return false;
        }
    }
}