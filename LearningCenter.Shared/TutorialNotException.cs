using System;
namespace _3_Shared;


public class TutorialNotException : Exception
{
    public TutorialNotException()
    {
    }

    public TutorialNotException(string message)
        : base(message)
    {
    }

    public TutorialNotException(string message, Exception inner)
        : base(message, inner)
    {
    }
}