using FluentResults;

namespace BeeCamp.Shared.Utilities.Errors;

public class RecordNotFoundErrors(string message) : Error(message);