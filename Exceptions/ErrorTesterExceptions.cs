
namespace error_tester.Exceptions
{
    public class ErrorTesterExceptions : System.ApplicationException {
        public ErrorTesterExceptions () { }
        public ErrorTesterExceptions (string message) : base (message) { }
        public ErrorTesterExceptions (string message, System.Exception inner) : base (message, inner) { }

    }

    public class IgnoreApplicationException : ErrorTesterExceptions {
        public IgnoreApplicationException () { }
        public IgnoreApplicationException (string message) : base (message) { }
        public IgnoreApplicationException (string message, System.Exception inner) : base (message, inner) { }
    }
}