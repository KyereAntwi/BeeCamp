using FluentResults;

namespace BeeCamp.Shared.Utilities.Errors;

public class ValidationErrors(string message) : Error(message);