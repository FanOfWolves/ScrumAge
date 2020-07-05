using System;

namespace ScrumageEngine.Exceptions {

    /// <summary>
    /// Thrown by InputHandler for incorrect pawn movement.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [System.Serializable]
    public class MovePawnException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="MovePawnException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MovePawnException(String message = "Something went wrong while moving pawn.") : base(message) { }
    }
}
