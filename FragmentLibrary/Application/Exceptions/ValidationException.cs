using System;
using System.Collections;
using System.Collections.Generic;

namespace FragmentLibrary.Application.Exceptions
{
    public class InvalidFieldMessage
    {
        public string FieldName { get; set; }

        public string Message { get; set; }
    }

    public class ValidationException : Exception
    {
        public IEnumerable<InvalidFieldMessage> InvalidFields { get; }

        public ValidationException(IEnumerable<InvalidFieldMessage> invalidFields) 
            : base("Validation Error")
        {
            InvalidFields = invalidFields;
        }
    }
}
