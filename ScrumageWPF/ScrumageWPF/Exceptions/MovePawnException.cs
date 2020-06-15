using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Exceptions {

    [System.Serializable]
    public class MovePawnException : Exception {
        public MovePawnException(String message = "Something went wrong while moving pawn.") : base(message) { }
    }
}
