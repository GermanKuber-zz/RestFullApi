using System;

namespace Community.Core.Results
{
    public class ActionResult<T> where T : class
    {
        public T Entity { get; private set; }
        public ActionStatus Status { get; private set; }

        public Exception Exception { get; private set; }


        public ActionResult(T entity, ActionStatus status)
        {
            Entity = entity;
            Status = status;
        }

        public ActionResult(T entity, ActionStatus status, Exception exception) : this(entity, status)
        {
            Exception = exception;
        }

    }
}