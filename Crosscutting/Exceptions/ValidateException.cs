﻿namespace Crosscutting.Exceptions;

public class ValidateException : Exception
{
    public ValidateException(string message) : base(message) { }
}