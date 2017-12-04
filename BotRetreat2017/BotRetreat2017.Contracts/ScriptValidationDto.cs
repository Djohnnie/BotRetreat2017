using System;
using System.Collections.Generic;

namespace BotRetreat2017.Contracts
{
    public class ScriptValidationDto
    {
        public String Script { get; set; }

        public Int64 CompilationTimeInMilliseconds { get; set; }

        public Int64 RunTimeInMilliseconds { get; set; }

        public List<ScriptValidationMessageDto> Messages { get; set; }
    }
}