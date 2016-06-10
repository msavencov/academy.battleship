using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;

namespace Academy.BattleShip.Service.Exceptions
{
    public class EntityValidationException : Exception
    {
        public DbValidationError[] Errors { get; private set; }
        public EntityValidationException(DbEntityValidationException exception) : this("See Errors property for details.")
        {
            Errors = exception.EntityValidationErrors.SelectMany(t => t.ValidationErrors).ToArray();
        }

        public EntityValidationException(string message) : base(message)
        {
        }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}