using System.Collections.Generic;

namespace CrossWordPuzzle.Core
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public List<string> MessageList { get; private set; }

        public OperationResult()
        {
            MessageList = new List<string>();
            Success = true;
        }

        // Adds messages to the MessageList string list which is later used for internal notifications.
        public void AddMessage(string message)
        {
            MessageList.Add(message);
        }

    }

}
