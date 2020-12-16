namespace ByteDev.Validation.Fluent
{
    /// <summary>
    /// Represents a validation error.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Validation.Fluent.ValidationError" /> class.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        public ValidationError(string code, string message)
            : this(code, message, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Validation.Fluent.ValidationError" /> class.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="propertyName">Name of property that relates to the validation error.</param>
        public ValidationError(string code, string message, string propertyName)
        {
            Code = code;
            Message = message;
            PropertyName = propertyName;
        }

        /// <summary>
        /// Error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Name of property that relates to the validation error.
        /// </summary>
        public string PropertyName { get; }

        internal bool IsPropertyNameSet => !string.IsNullOrEmpty(PropertyName);
    }
}