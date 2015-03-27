#region

using System;

#endregion

namespace CodeDraft.API
{
    public class ActionResult
    {
        public bool Success { get; private set; }

        public object Data { get; private set; }

        public Exception Error { get; private set; }

        public TimeSpan Duration { get; private set; }

        public ActionResult(bool success, object data, Exception error, TimeSpan duration)
        {
            this.Success = success;
            this.Data = data;
            this.Error = error;
            this.Duration = duration;
        }
    }
}